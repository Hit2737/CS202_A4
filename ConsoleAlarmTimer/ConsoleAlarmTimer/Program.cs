using System;
using System.Threading;

namespace AlarmClockConsoleApp
{
    // Subscriber class containing the event handler
    class Subscriber_class
    {
        public static void Ring()
        {
            Console.WriteLine("ALARM!!!!!!!!!!!!!");
            Thread.Sleep(1000);
        }
    }

    // Publisher class raising the event
    class Publisher_class
    {
        public event Alarm RaiseEvent;        // Event declaration
        public delegate void Alarm();         // Delegate definition

        public void SetTime(string sTime)
        {
            string currTime = DateTime.Now.ToString("HH:mm:ss"); // match format exactly
            while (currTime != sTime)
            {
                Console.WriteLine("Tick-Tock...");
                Thread.Sleep(1000);
                currTime = DateTime.Now.ToString("HH:mm:ss");
            }

            RaiseEvent(); // raise the event
        }

        static void Main(string[] args)
        {
            Publisher_class p = new Publisher_class();
            p.RaiseEvent += new Alarm(Subscriber_class.Ring);

            Console.WriteLine("Enter Time for Alarm (HH:mm:ss) =>");
            string t = Console.ReadLine();

            // ✅ Input format check
            if (TimeSpan.TryParseExact(t, "hh\\:mm\\:ss", null, out _))
            {
                p.SetTime(t);
            }
            else
            {
                Console.WriteLine("❌ Invalid format! Please enter time in HH:mm:ss format.");
            }
        }
    }
}
