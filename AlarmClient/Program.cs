using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AlarmClient
{
    class Program
    {
        private AlarmService.AlarmServiceClient _asc;
        private AlarmService.Alarm[] _alarms;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            _asc = new AlarmService.AlarmServiceClient();


            bool running = true;
            while (running)
            {
                Console.WriteLine("Receive: r \r\nSend: s \r\nExit: x");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "r":
                        Thread ga = new Thread(() => GetAlarms());
                        ga.IsBackground = true;
                        ga.Start();
                        break;
                    case "s":
                        Console.WriteLine("Please enter a message");
                        string message = Console.ReadLine();
                        Thread sa = new Thread(() => SendAlarm(message));
                        sa.IsBackground = true;
                        sa.Start();
                        break;
                    case "x":
                        running = false;
                        break;
                    default:
                        break;
                }
            }

        }

        private void GetAlarms()
        {
            Console.Clear();
            _alarms = _asc.GetAlarms();
            Console.WriteLine("Current Alarms:");
            foreach (var item in _asc.GetAlarms())
            {
                Console.WriteLine($"TIME: {item.Time} NUMBER: {item.Number} CONTENT: { item.Content}");
            }

            while (true)
            {
                AlarmService.Alarm[] newAlarms = _asc.GetAlarms();

                for (int i = _alarms.Count(); i < newAlarms.Count(); i++)
                {
                    Console.WriteLine($"TIME: {newAlarms[i].Time} NUMBER: {newAlarms[i].Number} CONTENT: { newAlarms[i].Content}");
                }
                _alarms = newAlarms;
                Thread.Sleep(5000);
            }

        }

        private void SendAlarm(string message)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Creating Alarm!");
                _asc.PostAlarm(new AlarmService.Alarm() { Time = DateTime.Now, Number = "66666666", Content = message });
                Thread.Sleep(5000);
            }

        }




    }
}
