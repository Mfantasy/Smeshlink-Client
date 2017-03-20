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





        string rec = "";
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
                string value = waterLevels.Item(i).InnerText;
                if (int.Parse(value) < 100)
                {
                    value = rec;
                }
                else
                {
                    rec = value;
                }
                //waterLevel.InnerText = waterLevels.Item(i).InnerText + " mm";
                waterLevel.InnerText = value + " mm";
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(waterLevel); feed.AppendChild(time);
                root.AppendChild(feed);
            }
            xdoc.AppendChild(root);            
            return xdoc;
        }

        public override void Post(DateTime start, DateTime end, Sensor ss)
        {
            SID = ss.SiteWhereId;
            XML x = new XML();
            try
            {
                XmlNodeList waterLevels = x.GetXmlNodeList(start, end, ss, "waterLevel");
                current = "液位";
                BeginPost(waterLevels, 1);
               
            }
            catch (Exception ex)
            {
                error += ex.Message + "\r\n";
                //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        void BeginPost(XmlNodeList list, int index)
        {
            if (list == null)
                return;
            foreach (XmlNode item in list)
            {
                string value = Sub(item.InnerText);
                DateTime time = DateTime.Parse(item.Attributes["at"].Value);
                timeStr = time.ToString();
                try
                {
                    PostS.PostToSW(SID, index, value, time);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);                    
                    error += ex.Message + "\r\n";
                }
            }
        }
        string timeStr = "";
        string current = "";
        string error = "";

        public override string GetErrorState()
        {
            return error;
        }

        public override string GetWorkState()
        {
            return current + "\t" + timeStr;
        }
    }
}
