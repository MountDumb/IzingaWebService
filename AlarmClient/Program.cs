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

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            _asc = new AlarmService.AlarmServiceClient();
            DoStuff();
            

            Console.ReadKey();
        }

        private void DoStuff()
        {
            _asc.PostAlarm(new AlarmService.Alarm() { Time = DateTime.Now, Number = "66666666", Content = "Testdata" });
            foreach (var item in _asc.GetAlarms())
            {
                Console.WriteLine(item.Content);
            }
            
        }




    }
}
