using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace IzingaWebService
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        public DateTime Time { get; private set; }
        [DataMember]
        public string Number { get; private set; }
        [DataMember]
        public string Content { get; private set; }


        public Alarm(DateTime time, string number, string content)
        {
            Time = time;
            Number = number;
            Content = content;
        }

    }
}

