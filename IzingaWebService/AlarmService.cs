using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace IzingaWebService
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AlarmService : IAlarmService
    {
        private List<Alarm> _alarms;
        private static object PostAlarmLock = new object();


        private AlarmService()
        {
            _alarms = new LogReader().TeliaLogReader();
        }

        public List<Alarm> GetAlarms()
        {

            return _alarms;

        }

        public void PostAlarm(Alarm a)
        {
            lock (PostAlarmLock)
            {
                _alarms.Add(a);
            }


        }


    }
}
