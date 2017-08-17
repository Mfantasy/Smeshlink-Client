using Smeshlink海绵城市Client.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.Device
{
    class MX9110: MX
    {
        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList speeds = x.GetXmlNodeList(start, end, ss, "speed");
            XmlNodeList temps = x.GetXmlNodeList(start, end, ss, "temp");
            XmlNodeList flows = x.GetXmlNodeList(start, end, ss, "flow");
            XmlNodeList levels = x.GetXmlNodeList(start, end, ss, "level");
            if (speeds == null)
                return null;
            for (int i = 0; i < speeds.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement speed = xdoc.CreateElement("流速");
                XmlElement temp = xdoc.CreateElement("温度");
                XmlElement flow = xdoc.CreateElement("流量");
                XmlElement level = xdoc.CreateElement("液位");
                time.InnerText = DateTime.Parse(speeds.Item(i).Attributes["at"].Value).ToString();
                speed.InnerText = double.Parse(speeds.Item(i).InnerText).ToString("0.000") + " m/s";
                temp.InnerText = double.Parse(temps.Item(i).InnerText).ToString("0.000") + " ℃";
                flow.InnerText = double.Parse(flows.Item(i).InnerText).ToString("0.000") + " m/s";
                level.InnerText = double.Parse(levels.Item(i).InnerText).ToString("0.000") + " m";

                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(speed); feed.AppendChild(temp); feed.AppendChild(flow);
                feed.AppendChild(level); feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);

            return xdoc;
        }

        public override void Post(DateTime start, DateTime end, Sensor ss)
        {
            throw new NotImplementedException();
        }

        public XmlDocument XDoc { get; set; }
    }
}
