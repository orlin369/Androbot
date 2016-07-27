/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.


 Author : Vyara Simeonova and Orlin Dimitrov
 Name : Arduino project for Betino.
 Ver: 1.1.0
*/
 
//--------------------- PIN definitions -----------

//--------------------- Sensors -----------
#define RIGHT_EDGE  A0
#define RIGHT_POS   A1
#define CENTER_POS  A2
#define LEFT_POS    A3
#define LEFT_EDGE   A4
#define EMITERS     7

//--------------------- Motor driver -----------
#define LEFT_PWM    11
#define RIGHT_PWM   10
#define LEFT_DIR    9
#define RIGHT_DIR   8

//--------------------- LEDS -----------
#define LED1        7
#define LED2        6
#define LED3        4

//--------------------- BEEP -----------
#define BEEP  3

//--------------------- BUTTON----------
#define BUTTON  2

//////////////////////////////////////////////////////////////////////////
// Setup of the peripherals.
//////////////////////////////////////////////////////////////////////////
void setup()
{
  // Setup the H bridge.
  pinMode(LEFT_PWM,OUTPUT);
  pinMode(RIGHT_PWM,OUTPUT);
  pinMode(LEFT_DIR,OUTPUT);
  pinMode(RIGHT_DIR,OUTPUT);
  analogWrite(LEFT_PWM, 0);
  analogWrite(RIGHT_PWM, 0);
  digitalWrite(LEFT_DIR, LOW);
  digitalWrite(RIGHT_DIR, LOW);

  // Setup the LEDs.
  pinMode(LED1,OUTPUT);
  pinMode(LED2,OUTPUT);
  pinMode(LED3,OUTPUT);

  // Setup the beep buzzer.
  pinMode(BEEP, OUTPUT);
  tone(BEEP, 2400, 500);
}

//////////////////////////////////////////////////////////////////////////
// Main loop of the program.
//////////////////////////////////////////////////////////////////////////
void loop()
{
   Move(0, 0);
   delay(1000);
   Move(120, -120);
   delay(1000);
   Move(0, 0);
   delay(1000);
   Move(-120, 120);
   delay(1000);
   Move(0, 0);
}

//////////////////////////////////////////////////////////////////////////
// This function controll the motors of the robot.
//////////////////////////////////////////////////////////////////////////
void Move(int leftSpeed, int rightSpeed)
{
  if(leftSpeed > 0)
  {
    digitalWrite(LEFT_DIR, HIGH);
    if(leftSpeed > 255)
    {
      leftSpeed = 255;
    }
  }
  else if(leftSpeed < 0)
  {
    digitalWrite(LEFT_DIR, LOW);
    if(leftSpeed < -255)
    {
      leftSpeed = -255;
    }
    leftSpeed *= -1;
  }
  else
  {
    leftSpeed = 0;
  }

  if(rightSpeed > 0)
  {
    digitalWrite(RIGHT_DIR, HIGH);
    if(rightSpeed > 255)
    {
      rightSpeed = 255;
    }
  }
  else if(rightSpeed < 0)
  {
    digitalWrite(RIGHT_DIR, LOW);
    if(rightSpeed < -255)
    {
      rightSpeed = -255;
    }
    rightSpeed *= -1;
  }
  else
  {
    rightSpeed = 0;
  }

  analogWrite(LEFT_PWM, leftSpeed);
  analogWrite(RIGHT_PWM, rightSpeed);
}

