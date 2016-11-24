using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.CFT
{
    public static class Constants
    {
    }

    public enum Services
    {
        Consult,
        EvalSupp,
        LeaveOth,
        MakeUp,
        Mathernity,
        Operate,
        Sell,
        Sick_Leave,
        SickChild,
        Study,
        Travel,
        Unpaid,
        Vacation
    }

    public static class ServiceSuffixes
    {
        public const string ServiceSuffix_bro = "(bro)";
        public const string ServiceSuffix__r_ = "(_r_)";
        public const string ServiceSuffix__ro = "(_ro)";
        public const string ServiceSuffix____ = "(___)";
    }


    //to brac z bazy danych 
    //public static class Contracts
    //{
    //    public const string Bench = "CFT: Bench.F..Net ";
    //    public const string EuroT1 = "(_r_)";
    //    public const string Tower_NPSCompRate = "(_ro)";
    //    public const string SCM_PepsiCola = "(___)";
    //    public const string JDA_Strategix= "(___)";

    //}
}
