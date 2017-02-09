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
        

        public static String Sub(string str)
        {
            if (str.Length > 6)
                str = str.Substring(0, 5);
            return str;
        }
    }
}
