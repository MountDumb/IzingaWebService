using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IzingaWebService
{

    [ServiceContract]
    public interface IAlarmService
    {
        [OperationContract]
        List<Alarm> GetAlarms();

        [OperationContract]
        void PostAlarm(Alarm a);


    }


}
