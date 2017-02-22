using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Smeshlink海绵城市Client
{
    public class PostS
    {
        public readonly static DateTime UnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static Double ToUnixTimestamp(DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
                date = date.ToUniversalTime();
            return (date - UnixTime).TotalMilliseconds;
        }
        //static bool isPostBefore = bool.Parse(ConfigurationManager.AppSettings["Post到原服务器"]);
        //static bool isPostAfter = bool.Parse(ConfigurationManager.AppSettings["Post新服务器"]);
        public static void PostToSW(string deviceId, int index, string data,DateTime dtime)
        {
            string postData = GetJson(deviceId, index.ToString(), data,dtime);           
                RequestPost(postUrl, postData);
           
        }

        public static string GetJson(string deviceId, string index, string data,DateTime dtime)
        {
            string time = ((long)ToUnixTimestamp(dtime)).ToString();
            JObject jobj = new JObject();
            jobj.Add("deviceId", deviceId);
            jobj.Add("attributeIndex", index);
            jobj.Add("attributeData", data);
            jobj.Add("ingressTime", time);
            jobj.Add("deviceTime", time);
            jobj.Add("source", "");
            return jobj.ToString();
        }
        static string postUrl = "http://hm-iot.chinacloudapp.cn:80/api/deviceEvents";
        static string postUrl2 = "http://124.89.55.165:80/api/deviceEvents";
        private static void RequestPost(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic ZXRhZG1pbkBzaXRlOmFiY2QxMjM=");
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据                
                response = request.GetResponse() as HttpWebResponse;
                ////直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                ////返回结果网页（html）代码
                string content = sr.ReadToEnd();
                ////string err = string.Empty;
                ////HttpContext.Current.Response.Write(content);
                //Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
