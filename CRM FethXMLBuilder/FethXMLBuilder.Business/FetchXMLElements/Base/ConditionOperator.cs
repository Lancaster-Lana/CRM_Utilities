using System;
using System.Collections.Generic;
using System.Text;

namespace FetchXMLBuilder.Business
{
    public class ConditionOperator
    {
        public static string Between = "between";
        public static string In = "in";
        public static string IsNull = "null";
        public static string Eq = "eq";
        public static string Ne = "ne";
        public static string Lt = "lt";
        public static string Gt = "gt";
        public static string Le = "le";
        public static string Ge = "ge";
        public static string Like = "like";
        public static string NotLike = "not-like";
        public static string On = "on ";
        public static string OnOrBefore = "on-or-befor";
        public static string OnOrAfter = "on-or-after ";
        public static string LastXHours = " last-x-hours ";
        public static string NextXHours = "next-x-hours";
        public static string LastXDays = "last-x-days";
        public static string NextXDdays = "next-x-days";
        public static string LastXWeeks = "last-x-weeks";
        public static string NextXWeeks = "next-x-weeks";
        public static string LastXMonths = "last-x-months";
        public static string NextXMonths = "ext-x-months";
        public static string LastXYears = "last-x-years";
        public static string NextXYears = "next-x-years";

    }
}
