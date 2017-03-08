using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXS1201:MX
    {
        private string humid;

        public string Humid
        {
            get { return humid; }
            set { humid = value; }
        }
        private string humtemp;

        public string Humtemp
        {
            get { return humtemp; }
            set { humtemp = value; }
        }


        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList humids = x.GetXmlNodeList(start, end, ss, "humid");
            XmlNodeList humtemps = x.GetXmlNodeList(start, end, ss, "humtemp");
            if (humids == null)
                return null;
            for (int i = 0; i < humids.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement humid = xdoc.CreateElement("湿度");
                XmlElement humtemp = xdoc.CreateElement("温度");

                time.InnerText = DateTime.Parse(humids.Item(i).Attributes["at"].Value).ToString();
                humid.InnerText = Sub(humids.Item(i).InnerText) + " %";
                humtemp.InnerText = Sub(humtemps.Item(i).InnerText) + " ℃";
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(humid);feed.AppendChild(humtemp); feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);            
            return xdoc;
        }
        public override void Post(DateTime start, DateTime end, Sensor ss)
        { }
    }
}
