using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXS1402:MX
    {
    
        private string soilTemperature;

        public string SoilTemperature
        {
            get { return soilTemperature; }
            set { soilTemperature = value; }
        }
        private string soilHumidity;

        public string SoilHumidity
        {
            get { return soilHumidity; }
            set { soilHumidity = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList soilTemperatures = x.GetXmlNodeList(start, end, ss, "soilTemperature");
            XmlNodeList soilhumids = x.GetXmlNodeList(start, end, ss, "soilhumid");
            if (soilTemperatures == null)
                return null;
            for (int i = 0; i < soilTemperatures.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement soilTemperature = xdoc.CreateElement("土壤温度");
                XmlElement soilhumid = xdoc.CreateElement("土壤水分");

                time.InnerText = DateTime.Parse(soilTemperatures.Item(i).Attributes["at"].Value).ToString();
                soilTemperature.InnerText = Sub(soilTemperatures.Item(i).InnerText) + " ℃";
                soilhumid.InnerText = Sub(soilhumids.Item(i).InnerText) + "%";
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(soilTemperature);feed.AppendChild(soilhumid); feed.AppendChild(time);
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
