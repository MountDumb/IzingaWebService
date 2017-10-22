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
        private object alarmsLock = new object();
        private List<Alarm> _alarms;

        private AlarmService()
        {
            _alarms = new LogReader().TeliaLogReader();
        }

        public List<Alarm> GetAlarms()
        {
            lock (alarmsLock)
            {
                return _alarms;
            }
                
            
        }

        public void PostAlarm(Alarm a)
        {
            lock (alarmsLock)
            {
                _alarms.Add(a);
            }
        }
    }
}
