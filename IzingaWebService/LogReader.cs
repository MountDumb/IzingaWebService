using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IzingaWebService
{
    public class LogReader
    {
        private List<Alarm> teliaMessages;

        public LogReader()
        {
            teliaMessages = new List<Alarm>();

        }

        public List<Alarm> TeliaLogReader()
        {
            //string targetpath = @"C:\Program Files (x86)\Mobilt Bredband\Log\trace_0_kopi.txt";
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Documents\\trace_0.txt";
            string targetFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mobilt Bredband\\Log";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mobilt Bredband\\Log\\trace_0_kopi.txt";

            CheckTargetPath(targetFolder, targetPath);

            File.Copy(currentPath, targetPath);

            foreach (string line in File.ReadAllLines(targetPath))
            {
                if (line.Contains("Number=+4552631220"))
                {
                    Regex regex = new Regex(@"(\d{4}[-]\d{2}[-]\d{2}\s\d{2}[:]\d{2}[:]\d{2})|(\w{3,}[=][a-zA-Z+0-9\s]{0,})");
                    MatchCollection match = regex.Matches(line);

                    DateTime timeOfAlaram = IntepretDateTime(match[0].ToString());
                    string number = IntepretData(match[1].ToString());
                    string content = IntepretData(match[2].ToString());

                    Alarm newAlarm = new Alarm(timeOfAlaram, number, content);

                    if (!teliaMessages.Exists(x => x.Time == timeOfAlaram))
                    {
                        teliaMessages.Add(newAlarm);
                    }
                }
            }

            return teliaMessages;
        }


        //Intended to split up a string into object type: DateTime
        //string format is supposed to be "2017-10-04 10:51:57"
        private DateTime IntepretDateTime(string input)
        {
            string[] date, time, dateAndTime;
            dateAndTime = input.Split(' ');
            date = dateAndTime[0].Split('-');
            time = dateAndTime[1].Split(':');
            return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));

        }

        private string IntepretData(string input)
        {
            string[] DataArray = input.Split('=');
            return DataArray[1];
        }

        private void CheckTargetPath(string targetFolder, string targetPath)
        {
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
        }


    }
}
