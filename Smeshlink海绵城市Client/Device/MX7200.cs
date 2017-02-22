using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MX7200:MX
    {
        private string waterLevel;

        public string WaterLevel
        {
            get { return waterLevel; }
            set { waterLevel = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList waterLevels = x.GetXmlNodeList(start, end, ss, "waterLevel");
            if (waterLevels == null)
                return null;
            for (int i = 0; i < waterLevels.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement waterLevel = xdoc.CreateElement("液位");

                time.InnerText = DateTime.Parse(waterLevels.Item(i).Attributes["at"].Value).ToString();
                waterLevel.InnerText = waterLevels.Item(i).InnerText + " mm";
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(waterLevel); feed.AppendChild(time);
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
