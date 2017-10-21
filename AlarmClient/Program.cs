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
        private static AlarmService.AlarmServiceClient _asc;

        static void Main(string[] args)
        {
            _asc = new AlarmService.AlarmServiceClient();
            Program p = new Program();
            p.Run();

        }

        private void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Send: s \r\nReceive: r \r\nExit: x");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "s":
                        new Thread(() => SendAlarm()).Start();
                        break;
                    case "r":
                        new Thread(() => GetAlarms()).Start();
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
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Nuværende alarmer:");
                foreach (var item in _asc.GetAlarms())
                {
                    Console.WriteLine(item.Content);
                }
                Thread.Sleep(3000);
            }

        }

        private void SendAlarm()
        {
            while (true)
            {
                _asc.PostAlarm(new AlarmService.Alarm() { Time = DateTime.Now, Number = "66666666", Content = "Testdata" });
                Thread.Sleep(5000);
            }
            
        }




    }
}
