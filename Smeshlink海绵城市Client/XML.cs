using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smeshlink海绵城市Client.DLL
{
    class XML
    {
        private string apiUrl;

        public string ApiUrl
        {
            get { return apiUrl; }
            set { apiUrl = value; }
        }

        public XmlNodeList GetXmlNodeList(DateTime start,DateTime end,Sensor ss,string dataChoose)
        {
            return GetXml(RetrieveByMistyAPI(start,end,ss.Feed+"/"+dataChoose));
        }

        public XmlNodeList GetXml(Stream s)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(s);
            XmlElement root = xdoc.DocumentElement;
            XmlNode data = root.FirstChild.SelectSingleNode("data");

            if (data == null)
                return null;
            //.SelectNodes("entry")[i].Attributes["at"].Value;
            XmlNodeList xl = data.SelectNodes("entry");
            return xl;     
        }
        public Stream RetrieveByMistyAPI(DateTime start, DateTime end, string gatewayId)
        {
            string timeStart = start.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string timeEnd = end.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string strUrl = String.Format("http://api.smeshlink.com/feeds/{0}.xml?key=df44011f-3daf-4372-8c14-9ad29a63a5cb&start={1}&end={2}", gatewayId, timeStart, timeEnd);
            Stream result = RequestGet(strUrl);
            return result;
        }
        public Stream RequestGet(string strURL)
        {
            HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            //request.Headers.Add("X-ApiKey", "3cba154a-07e0-4bd5-b20d-fdabb12ac3d9");
            HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            //string StrDate = "";
            //string strValue = "";
            //StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            //while ((StrDate = Reader.ReadLine()) != null)
            //{
            //    strValue += StrDate + "\r\n";
            //}
            //return strValue;
            return s;
        }

    }
}
