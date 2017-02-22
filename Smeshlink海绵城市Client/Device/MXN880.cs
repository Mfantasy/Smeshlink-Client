using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MXN880 : MX
    {
       
        private string battVol;

        public String BattVol
        {
            get { return battVol; }
            set { battVol = value; }
        }


        private string chargeVol;

        public String ChargeVol
        {
            get { return chargeVol; }
            set { chargeVol = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList chargeVols = x.GetXmlNodeList(start, end, ss, "chargeVol");
            XmlNodeList battVols = x.GetXmlNodeList(start,end,ss,"battVol");
            if (chargeVols == null)
                return null;

            for (int i = 0; i < chargeVols.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement chargeVol = xdoc.CreateElement("充电电压");
                XmlElement battVol = xdoc.CreateElement("电池电压");

                time.InnerText = DateTime.Parse(chargeVols.Item(i).Attributes["at"].Value).ToString();
                chargeVol.InnerText = chargeVols.Item(i).InnerText + " mv";
                battVol.InnerText = battVols.Item(i).InnerText + " mv";

                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(chargeVol);feed.AppendChild(battVol); feed.AppendChild(time);
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
