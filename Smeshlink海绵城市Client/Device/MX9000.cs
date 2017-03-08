using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class MX9000:MX
    {
        private string waterLevel;

        public string WaterLevel
        {
            get { return waterLevel; }
            set { waterLevel = value; }
        }

        private string flow;

        public string Flow
        {
            get { return flow; }
            set { flow = value; }
        }
        private string temp;

        public string Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        public override XmlDocument GetXdoc(DateTime start, DateTime end, Sensor ss)
        {
            XML x = new XML();
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("sensor");
            XmlNodeList waterLevels = x.GetXmlNodeList(start, end, ss, "waterLevel");
            XmlNodeList flows = x.GetXmlNodeList(start,end,ss,"flow");
            XmlNodeList temps = x.GetXmlNodeList(start,end,ss,"temp");
            if (waterLevels == null)
                return null;
            for (int i = 0; i < waterLevels.Count; i++)
            {
                XmlElement feed = xdoc.CreateElement("feed");
                XmlElement name = xdoc.CreateElement("名称");
                XmlElement time = xdoc.CreateElement("时间");
                XmlElement waterLevel = xdoc.CreateElement("液位");
                XmlElement flow = xdoc.CreateElement("流量");
                XmlElement temp = xdoc.CreateElement("温度");

                time.InnerText = DateTime.Parse(waterLevels.Item(i).Attributes["at"].Value).ToString();
                waterLevel.InnerText = waterLevels.Item(i).InnerText + " mm";
                flow.InnerText = flows.Item(i).InnerText + " m/s";
                temp.InnerText = Sub(temps.Item(i).InnerText) + " ℃";
                name.InnerText = ss.Name;

                feed.AppendChild(name); feed.AppendChild(waterLevel);feed.AppendChild(flow);feed.AppendChild(temp); feed.AppendChild(time);
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
                XmlNodeList flows = x.GetXmlNodeList(start, end, ss, "flow");
                XmlNodeList temps = x.GetXmlNodeList(start, end, ss, "temp");
                current = "液位";
                BeginPost(waterLevels, 1);
                current = "流量";
                BeginPost(flows, 2);
                current = "温度";
                BeginPost(temps, 3);            
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
