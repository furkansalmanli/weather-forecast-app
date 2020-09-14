using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace havaDurumuTahmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string api = "c95b4ae235f882228b400ef827d67e91";
        private const string baglanti2 = "http://api.openweathermap.org/data/2.5/weather?q=Turkey,Kütahya&mode=xml&units=metric&APPID="+api;

        MySqlConnection baglanti = new MySqlConnection("Server=83.150.213.18;Port=3306;Database=havaduru_1;Uid=havaduru_1;Pwd=e{lV-g6z4q.Y;");
        int[] sicaklikDizi = new int[2];
        int[] basincDizi = new int[2];
        int[] nemDizi = new int[2];
        void griddoldur()
        {
            baglanti.Open();
            string sql = "SELECT * FROM  Degerler ";
            MySqlCommand cmd = new MySqlCommand(sql, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ForeColor = Color.Black;
            baglanti.Close();
        }
        void sonveriler()
        {
            baglanti.Open();
            string sql = "select * from Degerler order by id desc limit 0,2";
            MySqlCommand cmd = new MySqlCommand(sql, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label18.Text = dr["id"].ToString();
                label12.Text = dr["sicaklik"].ToString();
                label13.Text = dr["basinc"].ToString();
                label14.Text = dr["nem"].ToString();
                sicaklikDizi[0] = Convert.ToInt32(label12.Text);
                basincDizi[0] = Convert.ToInt32(label13.Text);
                nemDizi[0] = Convert.ToInt32(label14.Text);
            }
            else
            {
                label12.Text = "veri cekilemedi";
            }
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
        void sondanoncekiveriler()
        {
            baglanti.Open();
            int a = Convert.ToInt32(label18.Text) - 1;
            string sql = "select * from Degerler where id='"+a+"'";
            MySqlCommand cmd = new MySqlCommand(sql, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label18.Text = dr["id"].ToString();
                label15.Text = dr["sicaklik"].ToString();
                label16.Text = dr["basinc"].ToString();
                label17.Text = dr["nem"].ToString();
                sicaklikDizi[1] = Convert.ToInt32(label15.Text);
                basincDizi[1] = Convert.ToInt32(label16.Text);
                nemDizi[1] = Convert.ToInt32(label17.Text);
            }
            else
            {
                label12.Text = "veri cekilemedi";
            }
        }
        string sontahmin()
        {
            int ww = Convert.ToInt32(label18.Text)-1;
            baglanti.Close();

            baglanti.Open();
            string sql = "select * from Degerler order by id desc limit 0,2";
            MySqlCommand cmd = new MySqlCommand(sql, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label18.Text = dr["id"].ToString();
                label12.Text = dr["sicaklik"].ToString();
                label13.Text = dr["basinc"].ToString();
                label14.Text = dr["nem"].ToString();
                sicaklikDizi[0] = Convert.ToInt32(label12.Text);
                basincDizi[0] = Convert.ToInt32(label13.Text);
                nemDizi[0] = Convert.ToInt32(label14.Text);
            }
            else
            {
                label12.Text = "veri cekilemedi";
            }
            dataGridView1.DataSource = dt;
            baglanti.Close();
            baglanti.Open();
            int a = Convert.ToInt32(label18.Text) - 1;
            string sql1 = "select * from Degerler where id='" + a + "'";
            MySqlCommand cmd1 = new MySqlCommand(sql, baglanti);
            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            cmd.ExecuteNonQuery();
            MySqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                label18.Text = dr1["id"].ToString();
                label15.Text = dr1["sicaklik"].ToString();
                label16.Text = dr1["basinc"].ToString();
                label17.Text = dr1["nem"].ToString();
                sicaklikDizi[1] = Convert.ToInt32(label15.Text);
                basincDizi[1] = Convert.ToInt32(label16.Text);
                nemDizi[1] = Convert.ToInt32(label17.Text);
            }
            else
            {
                label12.Text = "veri cekilemedi";
            }
            
            if (tahminet()==1)
            {
                return "";
            }
            else
            {
                return sontahmin();
            }
            

        }
         int tahminet()
        {
            int x = 0;
            if ((basincDizi[0] - basincDizi[1] >= 0) && (basincDizi[0] - basincDizi[1] <= 0.6) && (nemDizi[0] >= 60) && (sicaklikDizi[0] <= 20))
            {
                pictureYagmur.Visible = true;
                x = 1;
            }
            else if ((basincDizi[0] - basincDizi[1] <= 0) && (basincDizi[0] - basincDizi[1] <= 0.6) && (nemDizi[0] <= 40) && (sicaklikDizi[0] - sicaklikDizi[1] >= 3))
            {
                pictureKarli.Visible = true;
                x = 1;
            }
            else if ((basincDizi[0] - basincDizi[1] >= 0) && (yönGercek.Text == "South"))
            {
                pictureGünesli2.Visible = true;
                x =1;
            }
            else if(basincDizi[0]-basincDizi[1]>=0.7)
            {
                x = 0;
            }
            else if(basincDizi[0]-basincDizi[1]<=0.6)
            {
                x = 0;
            }
            else if(basincDizi[0]-basincDizi[1]<=-0.7)
            {
                pictureYagmur.Visible = true;
                x = 1;
            }
            else if ((yönGercek.Text=="West") || (yönGercek.Text == "NorthWest") || (yönGercek.Text=="SouthWest") && (basincDizi[0]-basincDizi[1]<=-0.7) && (sicaklikDizi[0]<=3))
             {
                pictureKarlı.Visible = true;
                x = 1;
            }
            else if ((yönGercek.Text == "West") || (yönGercek.Text == "NorthWest") || (yönGercek.Text == "SouthWest") && (basincDizi[0] - basincDizi[1] <= -0.7) && (sicaklikDizi[0] > 3))
            {
                pictureYagmur.Visible = true;
                x = 1;
            }
                if (x == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sonveriler();
            sondanoncekiveriler();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            griddoldur();

            //****************************************************************************************
            XDocument hava = XDocument.Load(baglanti2);
            var sicaklikgercek = hava.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            gercekDeger.Text = sicaklikgercek.ToString()+"'";


            var nemgercek = hava.Descendants("humidity").ElementAt(0).Attribute("value").Value;
            nemGercek.Text = nemgercek.ToString() +"%";

            var basincgercek = hava.Descendants("pressure").ElementAt(0).Attribute("value").Value;
            basincGercek.Text = basincgercek.ToString()+"hPa";

           var rüzgargercek = hava.Descendants("speed").ElementAt(0).Attribute("value").Value;
           rüzgarGercek.Text = rüzgargercek.ToString();

            var yongercek = hava.Descendants("direction").ElementAt(0).Attribute("name").Value;
            yönGercek.Text = yongercek.ToString();

            var yergercek = hava.Descendants("city").ElementAt(0).Attribute("name").Value;
            yerGercek.Text =yergercek.ToString();

            var durum = hava.Descendants("clouds").ElementAt(0).Attribute("name").Value;
            durumGercek.Text = durum.ToString();

            if (durum.Contains("clouds") == true) {
                pictureBox_bulutlu.Visible = true;
            }
            else if (durum.Contains("sun") == true)
            {
                pictureBox_bulutlu.Visible = false;
                picture_günesli.Visible = true;
            }
            else if (durum.Contains("precipitation") == true)
            {
                pictureBox_bulutlu.Visible = false;
                picture_günesli.Visible = false;
                pictureYagmurlu.Visible = true;
            }
            else if (durum.Contains("weather") == true)
            {
                pictureBox_bulutlu.Visible = false;
                picture_günesli.Visible = false;
                pictureYagmurlu.Visible = false;
                pictureKarli.Visible = true;
            }

            //***************************************
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tahminet() == 0)
            {
                sontahmin();
                baglanti.Close();
            }
            else
            {
                tahminet();
                baglanti.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureGünesli2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureKarli_Click(object sender, EventArgs e)
        {

        }
    }
}
