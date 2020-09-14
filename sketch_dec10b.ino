
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BMP085_U.h>
#include <dht11.h>
#define ag_ismi "KaToKiTa"
#define ag_sifresi "196619661966"
#define IP "83.150.213.18"    //thingspeak.com IP adresi
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(10085);
#define DHT11PIN 2
dht11 DHT11;

void setup()
{
  Serial.begin(9600);
  Serial.begin(115200);
  Serial.println("AT"); 
  delay(3000);
 analogReference(INTERNAL);
  if(Serial.find("OK")){
     Serial.println("AT+CWMODE=1"); 
     delay(2000);
     String baglantiKomutu=String("AT+CWJAP=\"")+ag_ismi+"\",\""+ag_sifresi+"\"";
    Serial.println(baglantiKomutu);
    
     delay(5000);
 }
}
void loop(){
  bmp.begin();
  sensors_event_t event;
    sensor_t sensor;
  bmp.getSensor(&sensor);
  bmp.getEvent(&event);
  DHT11.read(DHT11PIN);
  float sicaklik = DHT11.temperature; 
  float basinc =event.pressure;
  float seaLevelPressure = SENSORS_PRESSURE_SEALEVELHPA;
  float yukseklik =bmp.pressureToAltitude(seaLevelPressure,event.pressure);
  float nem = DHT11.humidity;
 Serial.println(sicaklik);
 Serial.println(basinc);
 Serial.println(yukseklik);
 Serial.println(nem);
 sicaklik_yolla(sicaklik,basinc,yukseklik,nem);
 delay(1800000);
}
void sicaklik_yolla(float sicaklik,float basinc,float yukseklik,float nem){
 Serial.println(String("AT+CIPSTART=\"TCP\",\"") + IP + "\",80");
 delay(1000);
  if(Serial.find("Error")){
   Serial.println("AT+CIPSTART Error");
    return;
  }
 String yollanacakkomut = "GET /index.php?sicaklik=";
 yollanacakkomut += (int(sicaklik));
 yollanacakkomut += "&basinc=";
 yollanacakkomut += (int(basinc));
 yollanacakkomut += "&yukseklik=";
 yollanacakkomut += (int(yukseklik));
 yollanacakkomut += "&nem=";
 yollanacakkomut += (int(nem));
 yollanacakkomut += " HTTP/1.0\r\n\r\n";
 delay(3000);
 Serial.print("AT+CIPSEND=");
 Serial.println(yollanacakkomut.length()+2);
 delay(1000);
 if(Serial.find(">")){
 Serial.print(yollanacakkomut);
 Serial.print("\r\n\r\n");
 }
 else{
 Serial.println("AT+CIPCLOSE");
 }
}
