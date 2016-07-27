
/*
Author : Vyara Simeonova
 Name : First Arduino project
 Ver: 1.0.0
 */
 
//--------------------- PIN definitions -----------

#define RIGHT_EDGE  A0
#define RIGHT_POS   A1
#define CENTER_POS  A2
#define LEFT_POS    A3
#define LEFT_EDGE   A4
#define EMITERS     7

#define LEFT_PWM    11
#define RIGHT_PWM   10
#define LEFT_DIR    9
#define RIGHT_DIR   8
//--------------- LEDS -----------
#define LED1        7
#define LED2        6
#define LED3        4
//------------BEEP--------
#define BEEP  3
//-------BUTTON----------
#define BUTTON  2

void setup() {
  // put your setup code here, to run once:
  pinMode(LEFT_PWM,OUTPUT);
  pinMode(RIGHT_PWM,OUTPUT);
  
  pinMode(LEFT_DIR,OUTPUT);
  pinMode(RIGHT_DIR,OUTPUT);
  
pinMode(LED1,OUTPUT);
  pinMode(LED2,OUTPUT);
  pinMode(LED3,OUTPUT);

  pinMode(BEEP,OUTPUT);
  tone(BEEP,2400,500);

}
void loop() {
  // put your main code here, to run repeatedly:
  
  digitalWrite(LEFT_DIR,HIGH);
   analogWrite(LEFT_PWM,128);
   delay(1000);
   analogWrite(LEFT_PWM,180);
  delay(1000);
  analogWrite(LEFT_PWM,128);                //PWM -задава скорост на мотора (от 0 до 255)
   delay(1000);                             //DIR - пин за посоката на въртене на мотора 
   analogWrite(LEFT_PWM,0);


   digitalWrite(RIGHT_DIR,HIGH);
   analogWrite(RIGHT_PWM,128);
   delay(1000);
   analogWrite(RIGHT_PWM,180);
  delay(1000);
   analogWrite(RIGHT_PWM,128);
   delay(1000);
   analogWrite(RIGHT_PWM,0);

}
