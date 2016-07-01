using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client
{
    class XmlParser
    {
        public static XmlDocument Xml(Stream s,GateWayType gt)
        {
            XmlDocument x = new XmlDocument();
            x.Load(s);
            XmlElement root = x.DocumentElement;
            string name = root.FirstChild.SelectSingleNode("title").InnerText;//云谷10号楼微气象监测
                 
            XmlDocument xdoc = null;
            XmlNodeList xl = null;
            switch (gt)
            {
                case GateWayType.MXS5000:
                    xl = root.FirstChild.SelectSingleNode("children").FirstChild.SelectSingleNode("children").SelectNodes("feed");
                    xdoc = NewXml5000(xl, name);
                    break;
                case GateWayType.MXS1402:
                    xl = root.FirstChild.SelectSingleNode("children").SelectNodes("feed")[1].SelectSingleNode("children").SelectNodes("feed");
                    xdoc = NewXml1402(xl,name);
                    break;
                
            }
            
            return xdoc;
        }
        static XmlDocument NewXml1402(XmlNodeList xl, string name)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("root");
            if (xl[1].SelectSingleNode("data") == null)
                return null;
            int length = xl[1].SelectSingleNode("data").ChildNodes.Count;
            for (int i = 0; i < length; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                string time = xl[1].SelectSingleNode("data").SelectNodes("entry")[i].Attributes["at"].Value;
                string soilHumid = xl[0].SelectSingleNode("data").SelectNodes("entry")[i].InnerText; ;
                string soilTemperature = xl[1].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                soilTemperature = Sub(soilTemperature);soilHumid = Sub(soilHumid);
                XmlElement timex = xdoc.CreateElement("时间"); timex.InnerText = DateTime.Parse(time).ToString();
                XmlElement title = xdoc.CreateElement("监测地点"); title.InnerText = name;
                XmlElement soilHumidx = xdoc.CreateElement("土壤水分"); soilHumidx.InnerText = soilHumid + "%";
                XmlElement soilTemperaturex = xdoc.CreateElement("土壤温度"); soilTemperaturex.InnerText = soilTemperature + "℃";
                feed.AppendChild(title);feed.AppendChild(soilHumidx);feed.AppendChild(soilTemperaturex);feed.AppendChild(timex);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);
            return xdoc;
        }
        static XmlDocument NewXml5000(XmlNodeList xl,string name)
        {
                XmlDocument xdoc = new XmlDocument();
                XmlElement root = xdoc.CreateElement("root");
            if (xl[1].SelectSingleNode("data") == null)
                return null;
                int lenght = xl[1].SelectSingleNode("data").ChildNodes.Count;
                for (int i = 0; i < lenght; i++)
                {
                    XmlElement feed = xdoc.CreateElement("feed");
                    string time = xl[1].SelectSingleNode("data").SelectNodes("entry")[i].Attributes["at"].Value;
                    string rain = xl[1].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                    string windspeed = xl[2].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                    string winddirection = xl[3].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                    string airtemp = xl[4].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                    string airhum = xl[5].SelectSingleNode("data").SelectNodes("entry")[i].InnerText;
                rain = Sub(rain); airhum = Sub(airhum); airtemp = Sub(airtemp);
                    XmlElement timex = xdoc.CreateElement("时间"); timex.InnerText = DateTime.Parse(time).ToString();
                    XmlElement rainx = xdoc.CreateElement("降雨量"); rainx.InnerText = rain+"mm";
                    XmlElement windspeedx = xdoc.CreateElement("风速"); windspeedx.InnerText = windspeed+ "m/s";
                    XmlElement winddirectionx = xdoc.CreateElement("风向"); winddirectionx.InnerText = winddirection+"°";
                    XmlElement airtempx = xdoc.CreateElement("温度"); airtempx.InnerText = airtemp+"℃";
                    XmlElement airhumx = xdoc.CreateElement("湿度"); airhumx.InnerText = airhum+"%";
                    XmlElement title = xdoc.CreateElement("监测地点"); title.InnerText = name;
                    feed.AppendChild(title); feed.AppendChild(rainx); feed.AppendChild(windspeedx); feed.AppendChild(winddirectionx);
                    feed.AppendChild(airtempx); feed.AppendChild(airhumx); feed.AppendChild(timex);
                    root.AppendChild(feed);
                }
                xdoc.AppendChild(root);
                return xdoc;
        }
        static String Sub(string str)
        {
            if (str.Length > 6)
                str = str.Substring(0, 5);
                return str;
        }
        [Obsolete]
        public static XmlDocument Xml(string str)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(str);
            XmlElement root = x.DocumentElement;
            string name = root.FirstChild.SelectSingleNode("title").InnerText;//云谷10号楼微气象监测
                                                                              //得到所有存储数据的feed节点
            XmlNodeList xl = root.FirstChild.SelectSingleNode("children").FirstChild.SelectSingleNode("children").SelectNodes("feed");
            XmlDocument xdoc = NewXml5000(xl, name);
            return xdoc;
        }
    }
}
