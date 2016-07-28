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
*/

// Standart library.
#include <stdlib.h>
// String and standart functions.
#include <String.h>
// Standart RC servo.
#include <Servo.h>
// Software ComPort port.
#include <SoftwareSerial.h>

//--------------------- PIN definitions -----------

//--------------------- Bluetooth -----------
#define BT_TX 12
#define BT_RX 13
 
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

// ComPort port echo.
boolean ComPortEcho = false;

// Print array.
char PrintArr[256];

// Create servo object to control a servo.
Servo pingServo;

// 
SoftwareSerial SoftSerial(BT_RX, BT_TX); // RX, TX

//#define ComPort Serial
#define ComPort SoftSerial


//////////////////////////////////////////////////////////////////////////
// Prototypes
//////////////////////////////////////////////////////////////////////////
void ReadCommand();
boolean ValidateCommand(String command);
void ParseCommand(String command);
void Move(int leftSpeed, int rightSpeed);

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
  tone(BEEP, 2400, 100);

  // Setup ComPort port.
  ComPort.begin(9600);
  ComPort.println("Hello, I am Carton4o!");
}

//////////////////////////////////////////////////////////////////////////
// Main loop of the program.
//////////////////////////////////////////////////////////////////////////
void loop()
{
  /*
   Move(0, 0);
   delay(1000);
   Move(120, -120);
   delay(1000);
   Move(0, 0);
   delay(1000);
   Move(-120, 120);
   delay(1000);
   Move(0, 0);
   */
  ReadCommand();
}

//////////////////////////////////////////////////////////////////////////  
// Read incomming data from the ComPort buffer.
//////////////////////////////////////////////////////////////////////////  
void ReadCommand()
{  
  static String IncomingCommand = "";
  
  // Fill the command data buffer with command.
  while(ComPort.available())
  {
    // Add new char.
    IncomingCommand += (char)ComPort.read();
    // Wait a while for a a new char.
    delay(5);
  }
  
  // If command if not empty parse it.
  if(IncomingCommand != "")
  {
    boolean isValid = ValidateCommand(IncomingCommand);
    if(isValid)
    {
      ParseCommand(IncomingCommand);
    }
    // Print command for feedback.
    if(ComPortEcho)
    {
      ComPort.print("Cmd; ");
      ComPort.println(IncomingCommand);
    }
  }
  
  // Clear the command data buffer.
  IncomingCommand = ""; 
}

//////////////////////////////////////////////////////////////////////////  
// Validate incomming command from the ComPort buffer.
//////////////////////////////////////////////////////////////////////////  
boolean ValidateCommand(String command)
{
  //
  // ?R+180\n
  // ||||||||
  // ||||||\\- Termin
  // ||\\\\--- Degree value [-180 : +180].
  // ||\------ Direction index .
  // |\------- Motion type - in this case rotation.
  // \-------- Start symbol.
  //
  // ?M+999\n
  // ||||||||
  // ||||||\\- Termin
  // ||\\\\--- Degree value [-999 : +999].
  // ||\------ Direction index .
  // |\------- Motion type - in this case move.
  // \-------- Start symbol.
  //
  // Package lenght 1 + 1 + 1 + 3 + 1 = 7 bytes;
  //
  
  
  boolean state = false;
  // Number value.
  static int numValue;
  numValue = 0;
  // ?M+1800\n
  if(command[0] == '?' && command[7] == '\n')
  {
    if(command[2] == '-' || command[2] == '+')
    {
      if(command[1] == 'M')
      {
        // Convert commands from string to numbers.
        numValue = atoi(command.substring(3, 7).c_str());

        if(numValue <= 9999 && numValue >= -9999)
        {
          // If is valid.
          state = true;
        }
      }
      if(command[1] == 'R')
      {
        if(numValue <= 9999 && numValue >= -9999)
        {
          // If is valid.
          state = true;
        } 
      }
    }
  }
  // ?US180\n
  if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      numValue = atoi(command.substring(3, 6).c_str());       
      if(numValue >= 0 && numValue <= 180)
      {
        // If is valid.
        state = true;
      }
    }
  }
  if(command == "?SENSORS\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?POSITION\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?STOP\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?USA\n")
  {
    // If is valid.
    state = true;
  }
  
  // If is not valid.
  return state;
}

void ParseCommand(String command)
{
  // Number value.
  static int steps;
  static long r;
  static long t;
  static float cmMsec;
  static long microsec;
  
  steps = 0;
  r = 0;
  t = 0;
  cmMsec = 0;
  microsec = 0;
  
  // Convert commands from string to numbers.
  steps = atoi(command.substring(3, 7).c_str());
  
  if(command[1] == 'M')
  {
    if(command[2] == '-')
    {
      steps *= -1;
    }
    else if(command[2] == '+')
    {
      ;
    }
    
    MoveRobot(steps);
  }
  else if(command[1] == 'R')
  {
    if(command[2] == '-')
    {
      steps *= -1;
    }
    else if(command[2] == '+')
    {
      ;
    }
    
    TurnRobot(steps);
  }
  else if(command == "?SENSORS\n")
  {
    // read the analog in value:
    //ReadLRSensors();
    // TODO: Read analogs.
  }
  else if(command == "?POSITION\n")
  {
    r = 0;
    t = 0;
    sprintf(PrintArr, "#POSITION;D:%ld;A:%ld;", t, r);
    ComPort.println(PrintArr);   
  }
  else if(command == "?STOP\n")
  {
    StopMove();
    ComPort.println("#STOP");
  }
  else if(command == "?USA\n")
  {
    // Preventing errors in mesurments.
    pingServo.write(0);
    int pos = pingServo.read();
    pos += 1000;
    delay(pos);
    
    for(int indexPos = 0; indexPos <= 180; indexPos++)
    {
        pingServo.write(indexPos);
        delay(100);
        //microsec = ultraSonic.timing();     
        //cmMsec = ultraSonic.convert(microsec, Ultrasonic::CM);
        //sprintf(PrintArr, "#US;%d:%d", indexPos, cmMsec);
        //ComPort.println(PrintArr);   

        ComPort.print("#US;");
        ComPort.print(indexPos);
        ComPort.print(":");
        ComPort.println(cmMsec);
    }
  }
  // ?US180\n
  else if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      steps = atoi(command.substring(3, 6).c_str());
       
      if(steps >= 0 && steps <= 180)
      {
        //ReadPing(steps);
      }
    }
  }
}

void MoveRobot(int speed)
{
    Move(speed, speed);
}

void TurnRobot(int speed)
{
    Move(speed, -speed);
}

void StopMove()
{
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

void ReadLine()
{
  
  // Data array.
  static int rightEdge = 0;
  static int rightPos = 0;
  static int centerPos = 0;
  static int leftPos = 0;
  static int leftEdge = 0;
  
  // Get data.
  rightEdge = analogRead(RIGHT_EDGE);
  rightPos = analogRead(RIGHT_POS);
  centerPos = analogRead(CENTER_POS);
  leftPos = analogRead(LEFT_POS);
  leftEdge = analogRead(LEFT_EDGE);

  
}

