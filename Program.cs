using System;
using System.Media;
using Phidget22;

namespace HelloPhidgets
{
    class Program
    {
        static void Main(string[] args)
        {
            var greenButton = new DigitalInput();
            greenButton.IsHubPortDevice = true;
            greenButton.HubPort = 1;

            var greenLed = new DigitalOutput();
            greenLed.IsHubPortDevice = true;
            greenLed.HubPort = 3;

            var redButton = new DigitalInput();
            redButton.IsHubPortDevice = true;
            redButton.HubPort = 0;

            var redLed = new DigitalOutput();
            redLed.IsHubPortDevice = true;
            redLed.HubPort = 2;

            var sonar = new DistanceSensor();
            sonar.HubPort = 4;

            sonar.Open(1000);
            redButton.Open(1000);
            redLed.Open(1000);
            greenButton.Open(1000);
            greenLed.Open(1000);

            greenButton.StateChange += (o, e) =>
            {
                greenLed.State = e.State;
            };

            redButton.StateChange += (o, e) =>
            {
                redLed.State = e.State;

                if (e.State)
                {
                    var sound = new SoundPlayer(@"C:\windows\media\tada.wav");
                    sound.Play();

                    var random = new Random();

                    Console.WriteLine("The next random number is {0}!", random.Next(0, 10));
                }
            };

            sonar.DistanceChange += (o, e) =>
            {
                if(e.Distance > 100)
                {
                    greenLed.State = true;
                    redLed.State = false;
                }
                else
                {
                    greenLed.State = false;
                    redLed.State = true;
                }
            };

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            redButton.Close();
            redLed.Close();
            greenButton.Close();
            greenLed.Close();

        }
    }
}
