using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    abstract class MX
    {
        private string title;

        public string Titile
        {
            get { return title; }
            set { title = value; }
        }
        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public abstract XmlDocument GetXdoc(DateTime start,DateTime end,Sensor ss);

        public abstract void Post(DateTime start, DateTime end, Sensor ss);

        public virtual string GetWorkState() { return ""; }
        public virtual string GetErrorState() { return ""; }



        public string SID { get; set; }
        public static String Sub(string str)
        {
            double result = 0;
            bool ok = double.TryParse(str, out result);
            if (ok)
            {
                return result.ToString("0.0");
            }
            else
            {
                return str;
            }           
        }
    }
}
