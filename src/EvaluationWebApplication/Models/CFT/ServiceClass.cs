using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.CFT
{
    public class ServiceClass
    {
        public Services ServiceType { get; set; }
        public bool IsBillable { get; set; }
        public bool IsCountsForRequired { get; set; }
        public bool IsCountForOT { get; set; }
        public string ServiceSuffix { get; private set; }

        public ServiceClass(Services serviceType)
        {
            ServiceType = serviceType;
            IsBillable = false;
            IsCountsForRequired = false;
            IsCountForOT = false;
            SetSuffix();
        }
        protected void SetSuffix()
        {
            StringBuilder suffixString = new StringBuilder("(___)");
            //ServiceSuffix = "(___)";
            if (ServiceType == Services.Consult)
            {
                suffixString[1] = 'b';
                IsBillable = true;
            }

            if (ServiceType == Services.Consult || ServiceType == Services.EvalSupp || ServiceType == Services.LeaveOth || ServiceType == Services.MakeUp || ServiceType == Services.Mathernity
                || ServiceType == Services.Operate || ServiceType == Services.Sell || ServiceType == Services.Sick_Leave || ServiceType == Services.SickChild || ServiceType == Services.Unpaid
                || ServiceType == Services.Vacation)
            {
                suffixString[2] = 'r';
                IsCountsForRequired = true;
            }
            if (ServiceType == Services.Consult || ServiceType == Services.Operate || ServiceType == Services.Vacation)
            {
                suffixString[3] = 'o';
                IsCountForOT = true;
            }
            ServiceSuffix = suffixString.ToString();
        }
    }
}
