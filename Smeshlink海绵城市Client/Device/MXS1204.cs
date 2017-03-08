using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXS1204 : MX
    {
        private string temperature;

        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private string pressure;

        public String Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }
        private string height;

        public String Height
        {
            get { return height; }
            set { height = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList temperatures = x.GetXmlNodeList(start, end, ss, "temperature");
            XmlNodeList pressures = x.GetXmlNodeList(start,end,ss,"pressure");
            XmlNodeList heights = x.GetXmlNodeList(start,end,ss,"height");
            if (temperatures == null)
                return null;
            for (int i = 0; i < temperatures.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement temperature = xdoc.CreateElement("空气温度");
                XmlElement pressure = xdoc.CreateElement("大气压");
                XmlElement height = xdoc.CreateElement("海拔高度");

                time.InnerText = DateTime.Parse(temperatures.Item(i).Attributes["at"].Value).ToString();
                temperature.InnerText = Sub(temperatures.Item(i).InnerText) + " ℃";
                pressure.InnerText = pressures.Item(i).InnerText + " Pa";
                height.InnerText = heights.Item(i).InnerText + " m";

                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(temperature); feed.AppendChild(pressure); feed.AppendChild(height); feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);
            
            return xdoc;
        }
        public override void Post(DateTime start, DateTime end, Sensor ss)
        {
        }
    }
}
