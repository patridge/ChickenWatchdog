using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
//using SecretLabs.NETMF.Hardware.NetduinoPlus;
using N = SecretLabs.NETMF.Hardware.NetduinoPlus;
//using Netduino.Foundation;
using Netduino.Foundation.Sensors.Atmospheric;
using Netduino.Foundation.Displays;
using Netduino.Foundation.Displays.LCD;

namespace ChickenWatchdog
{
    public class Program
    {
        static ITextDisplay lcd;
        static BME280 sensor;

        public static void Main()
        {
            lcd = new Lcd2004(N.Pins.GPIO_PIN_D5, N.Pins.GPIO_PIN_D7, N.Pins.GPIO_PIN_D8, N.Pins.GPIO_PIN_D9, N.Pins.GPIO_PIN_D10, N.Pins.GPIO_PIN_D11);
            sensor = new BME280(updateInterval: 0);

            string displayMessageLine1Prior = "";
            string displayMessageLine1;
            string displayMessageLine2Prior = "";
            string displayMessageLine2;
            string displayMessageLine3Prior = "";
            string displayMessageLine3;
            //string displayMessageLine4;
            while (true)
            {
                //  Make the sensor take new readings.
                sensor.Update();

                //  Prepare a message for the user and output to the LCD
                displayMessageLine1 = "Temperature: " + sensor.Temperature.ToString("F1") + " C";
                displayMessageLine2 = "Humidity: " + sensor.Humidity.ToString("F1") + " %";
                displayMessageLine3 = "Pressure: " + (sensor.Pressure / 100).ToString("F0") + " hPa";

                if (displayMessageLine1 != displayMessageLine1Prior
                    || displayMessageLine2 != displayMessageLine2Prior
                    || displayMessageLine3 != displayMessageLine3Prior)
                {
                    lcd.Clear();
                    lcd.WriteLine(displayMessageLine1, 0);
                    lcd.WriteLine(displayMessageLine2, 1);
                    lcd.WriteLine(displayMessageLine3, 2);
                }

                displayMessageLine1Prior = displayMessageLine1;
                displayMessageLine2Prior = displayMessageLine2;
                displayMessageLine3Prior = displayMessageLine3;

                Thread.Sleep(2000);
            }
            
            //Debug.Print(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));

            // 5: Display stuff on a multi-line display
            //ITextDisplay lcd = new Lcd2004(N.Pins.GPIO_PIN_D5, N.Pins.GPIO_PIN_D7, N.Pins.GPIO_PIN_D8, N.Pins.GPIO_PIN_D9, N.Pins.GPIO_PIN_D10, N.Pins.GPIO_PIN_D11);
            //lcd.WriteLine("This working?", 0);
            //Thread.Sleep(Timeout.Infinite);

            // 4: Get sensor data from BME280
            ////
            ////  Create a new BME280 object and put the sensor into polling
            ////  mode (update intervale set to 0ms).
            ////
            //BME280 sensor = new BME280(updateInterval: 0);

            //string message;
            //while (true)
            //{
            //    //
            //    //  Make the sensor take new readings.
            //    //
            //    sensor.Update();
            //    //
            //    //  Prepare a message for the user and output to the debug console.
            //    //
            //    message = "Temperature: " + sensor.Temperature.ToString("F1") + " C\n";
            //    message += "Humidity: " + sensor.Humidity.ToString("F1") + " %\n";
            //    message += "Pressure: " + (sensor.Pressure / 100).ToString("F0") + " hPa\n\n";
            //    Debug.Print(message);
            //    //
            //    //  Sleep for 1000ms before repeating the process.
            //    //
            //    Thread.Sleep(1000);
            //}

            // 3: Pulse an LED to make sure I can get Netduino.Foundation to work
            //// create a new LED on pin 11
            //var pwmLed = new Netduino.Foundation.LEDs.PwmLed(N.PWMChannels.PWM_PIN_D11,
            //    Netduino.Foundation.LEDs.TypicalForwardVoltage.White);

            //// pulse the LED
            //pwmLed.StartPulse();

            //// keep the app running
            //Thread.Sleep(Timeout.Infinite);

            // 2: Blink and LED to make sure I can deploy
            //// configure an output port for us to "write" to the LED
            //OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            //// note that if we didn't have the SecretLabs.NETMF.Hardware.Netduino DLL, we could also manually access it this way:
            ////OutputPort led = new OutputPort(Cpu.Pin.GPIO_Pin10, false); 
            //int i = 0;
            //while (true)
            //{
            //    led.Write(true); // turn on the LED 
            //    Thread.Sleep(250); // sleep for 250ms 
            //    led.Write(false); // turn off the LED 
            //    Thread.Sleep(250); // sleep for 250ms 

            //    Debug.Print("Looping" + i);
            //    i++;
            //}

            // 1: Included with File>New
            //Debug.Print(Resources.GetString(Resources.StringResources.String1));
        }
    }
}
