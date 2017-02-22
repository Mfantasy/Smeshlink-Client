using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXS1501:MX
    {
       
        private string light;

        public String Light
        {
            get { return light; }
            set { light = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList lights = x.GetXmlNodeList(start, end, ss,"light");
            if (lights == null)
                return null;
            for (int i = 0; i < lights.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement light = xdoc.CreateElement("太阳光照");

                time.InnerText = DateTime.Parse(lights.Item(i).Attributes["at"].Value).ToString();
                light.InnerText = lights.Item(i).InnerText + " lux";
                name.InnerText = ss.Name;

                feed.AppendChild(name);feed.AppendChild(light);feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);
            this.XDoc = XDoc;
            return xdoc;
        }
        public XmlDocument XDoc { get; set; }
        public override void Post(DateTime start, DateTime end, Sensor ss)
        {
            throw new NotImplementedException();
        }
    }
}
