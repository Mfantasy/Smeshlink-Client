using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXS5000 : MX
    {
      
        private string rain;

        public string Rain
        {
            get { return rain; }
            set { rain = value; }
        }
        private string windSpeed;

        public string WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }
        private string windDirection;

        public string WindDirection
        {
            get { return windDirection; }
            set { windDirection = value; }
        }
        private string temperature;

        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private string humidity;

        public string Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public XmlDocument XDoc { get; set; }

        public override XmlDocument GetXdoc(DateTime start,DateTime end,Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList rains = x.GetXmlNodeList(start, end, ss,"rain");
            XmlNodeList windSpeeds = x.GetXmlNodeList(start, end, ss, "windspeed");
            XmlNodeList windDirections = x.GetXmlNodeList(start, end, ss, "winddirection");
            XmlNodeList temperatures = x.GetXmlNodeList(start, end, ss, "airtemp");
            XmlNodeList humiditys = x.GetXmlNodeList(start, end, ss, "airhumid");
            if (rains == null)
                return null;
            for (int i = 0; i < rains.Count; i++)
            {//[i].Attributes["at"].Value
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement rain = xdoc.CreateElement("雨量");
                XmlElement windspeed = xdoc.CreateElement("风速");
                XmlElement winddirection = xdoc.CreateElement("风向");
                XmlElement airtemp = xdoc.CreateElement("空气温度");
                XmlElement airhumid = xdoc.CreateElement("空气湿度");
                
                time.InnerText =DateTime.Parse(rains.Item(i).Attributes["at"].Value).ToString();
                rain.InnerText = Sub(rains.Item(i).InnerText) + " mm";
                windspeed.InnerText = windSpeeds.Item(i).InnerText + " m/s";
                winddirection.InnerText = windDirections.Item(i).InnerText+" °";
                airtemp.InnerText = Sub(temperatures.Item(i).InnerText)+" ℃";
                airhumid.InnerText = Sub(humiditys.Item(i).InnerText)+" %";
                name.InnerText = ss.Name;
                feed.AppendChild(name);feed.AppendChild(rain);feed.AppendChild(windspeed);
                feed.AppendChild(winddirection);feed.AppendChild(airtemp);feed.AppendChild(airhumid);
                feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);
            this.XDoc = xdoc;
            SID = ss.SiteWhereId ;
            return xdoc;
        }

        public override void Post()
        {
            
            //PostS.PostToSW(ss.SiteWhereId, 1, Rain);
            //PostS.PostToSW(ss.SiteWhereId, 2, Temperature);
            //PostS.PostToSW(ss.SiteWhereId, 3, Humidity);
            //PostS.PostToSW(ss.SiteWhereId, 4, WindSpeed);
            //PostS.PostToSW(ss.SiteWhereId, 5, WindDirection);
        }
        void Post1R()
        {
            
            //PostS.PostToSW(SID,1,)
        }
    }
}
