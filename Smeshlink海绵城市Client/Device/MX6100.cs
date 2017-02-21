using Smeshlink海绵城市Client.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.Device
{
    class MX6100 : MX
    {
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList statuss = x.GetXmlNodeList(start, end, ss, "status");
            if (statuss == null)
                return null;
            for (int i = 0; i < statuss.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement status = xdoc.CreateElement("在线状态");

                time.InnerText = DateTime.Parse(statuss.Item(i).Attributes["at"].Value).ToString();
                status.InnerText = statuss.Item(i).InnerText;
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(status); feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);
            this.XDoc = XDoc;
            return xdoc;
        }
        public XmlDocument XDoc { get; set; }
        public override void Post()
        {
            throw new NotImplementedException();
        }
    }
}
