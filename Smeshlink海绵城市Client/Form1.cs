using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;


public enum GateWayType
{
    MXS5000,MXS1402
}
namespace Smeshlink海绵城市Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SqlSelect();
            Initial();
        }
#region 已弃用Tcp连接查询数据
        private void button1_Click_1(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerRetrieveBegin.Value;
            DateTime end = dateTimePickerRetrieveEnd.Value;
            string weatherStation = comboBoxChooseWeatherStation.Text;
            string timeBegin = start.ToString("yyyy/MM/dd HH:mm");
            string timeEnd = end.ToString("yyyy/MM/dd HH:mm");
            string condition = comboBoxCondition.Text;
            DataSet ds = GetData(weatherStation, timeBegin, timeEnd, condition);
            dsWhole = ds;
            dataGridViewRetrieve.DataSource = ds.Tables[0];
            panelRetrieveShowData.Visible = true;
        }
        private DataSet GetData(string weatherStation, string timeBegin, string timeEnd, string condition)
        {
            TcpClient tt = new TcpClient();
            DataSet dd = new DataSet();
            tt.Connect("119.10.1.156", 9000);
            NetworkStream stream = tt.GetStream();
            string send = weatherStation + "-" + timeBegin + "-" + timeEnd + "-" + condition;
            byte[] bytes = Encoding.UTF8.GetBytes(send);
            stream.Write(bytes, 0, bytes.Length);
            dd.ReadXml(stream);
            return dd;
        }
        #endregion

        #region 生成报表
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
                labelChooseDirectory.Text = fbd.SelectedPath;
        }
        //生成报表
        private void button2_Click(object sender, EventArgs e)
        {
            if (dsWhole == null)
            { MessageBox.Show("请先进行查询"); return; }
            try
            {
                if (labelChooseDirectory.Text == "报表存储目录")
                    button1_Click(null, null);
                ExcelLibrary.DataSetHelper.CreateWorkbook(labelChooseDirectory.Text + "\\" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分") + "报表.xls", dsWhole);
                MessageBox.Show("生成报表成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        //运行时初始化控件的值
        public void Initial()
        {
            dateTimePickerRetrieveEnd.Value = DateTime.Now;
            TimeSpan tspan = new TimeSpan(24, 0, 0);
            dateTimePickerRetrieveBegin.Value = DateTime.Now.Subtract(tspan);
            dateTimePickerRetrieveBegin.MaxDate = DateTime.Now;
            dateTimePickerRetrieveEnd.MaxDate = DateTime.Now;

            comboBoxChooseWeatherStation.SelectedIndex = 0;
            comboBoxCondition.SelectedIndex = 0;
        }
        //开始查询
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                DateTime start = dateTimePickerRetrieveBegin.Value;
                DateTime end = dateTimePickerRetrieveEnd.Value;
                if (start > end)
                {
                    MessageBox.Show("查询开始时间应小于结束时间");
                    return;
                }
                if ((end - start).Days >= 30)
                {
                    MessageBox.Show("不允许查询30天以上的数据");
                    return;
                }
                panelRetrieveShowData.Visible = false;
                pictureBox1.Visible = true;
                buttonGenerateReport.Enabled = false;
                button1.Enabled = false;
                comboBoxChooseWeatherStation.Enabled = false;
                string gatewayId = String.Empty;
                int index = comboBoxChooseWeatherStation.SelectedIndex;
                GateWayType gt = 0;
                switch (index)
                {
                    case 0:
                        gatewayId = zyllGateway;
                        break;
                    case 1:
                        gatewayId = xbygGateway;
                        break;
                    case 2:
                        gatewayId = kdhyGateway;
                        break;
                    case 3:
                        gatewayId = tdjy1402;
                        gt = GateWayType.MXS1402;
                        break;
                }
                OverDelegate overDel = Over;
                ThreadPool.QueueUserWorkItem(new WaitCallback((o) => {
                    //string data = RetrieveByMistyAPI(start, end, gatewayId);
                    // XmlDocument xdoc = XmlParser.Xml(data);
                    XmlDocument xdoc = RetrieveByMistyAPI1(start, end, gatewayId,gt);
                    if (xdoc == null)
                    {
                        this.Invoke(new Action(Over));
                        MessageBox.Show("未查询到数据");
                        return;
                    }
                    overDel.Invoke(xdoc);
                }),overDel);
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        delegate void OverDelegate(XmlDocument xdoc);
        void Over()
        {
            comboBoxChooseWeatherStation.Enabled = true;
            button1.Enabled = true;
            buttonGenerateReport.Enabled = true;
            pictureBox1.Visible = false;
        }
        void Over(XmlDocument xdoc)
        {
            this.Invoke(new Action(() =>
            {
                XmlNodeReader xnr = new XmlNodeReader(xdoc);
                DataSet ds = new DataSet();
                ds.ReadXml(xnr);
                if(comboBoxChooseWeatherStation.SelectedIndex < 3)
                    ds = ChooseData(comboBoxCondition.Text, ds);
                dataGridViewRetrieve.DataSource = ds.Tables[0];
                panelRetrieveShowData.Visible = true;
                dsWhole = ds;
                Over();
                MessageBox.Show("查询成功");
            }));
        }

        DataSet dsWhole;
        const string zyllGateway = "81EAA500E524223A/0";
        const string xbygGateway = "81B28F01E524227F/0";
        const string kdhyGateway = "81E85C01E524228E/0";
        const string tdjy1402 = "8182B200E5242275/1";
        public DataSet ChooseData(string condition, DataSet ds)
        {
            DataSet dsNew = ds;
            int count = ds.Tables[0].Rows.Count;
            if (condition == "无")
                return dsNew;
            else if (condition == "有降雨时的数据")
            {
                string rainfall = null;
                //ds.Tables[0].Rows[0]["雨量"];
                for (int i = 0; i < count; i++)
                {
                    if ((string)ds.Tables[0].Rows[i]["降雨量"] == rainfall)
                    {
                        dsNew.Tables[0].Rows[i].Delete();
                        count--;
                        i--;
                    }
                    else
                        rainfall = (string)ds.Tables[0].Rows[i]["降雨量"];
                }
            }
            else if (condition == "每个小时一条数据")
            {
                string time = null;
                for (int i = 0; i < count; i++)
                {
                    int index = ((string)ds.Tables[0].Rows[i]["时间"]).IndexOf(" ");
                    string hour = ((string)ds.Tables[0].Rows[i]["时间"]).Substring(index + 1, 2);
                    if (hour == time)
                    {
                        dsNew.Tables[0].Rows[i].Delete();
                        count--;
                        i--;
                    }
                    else
                        time = hour;
                }
            }
            return dsNew;
        }
        //封装调用mistyAPI的条件 将HttpResponse转为string然后xmlDoc.loadxml(string)
        [Obsolete("建议使用RetrieveByMistyAPI1,封装好了全过程")]  
        public String RetrieveByMistyAPI(DateTime start, DateTime end, string gatewayId)
        {
            string timeStart = start.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string timeEnd = end.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string strUrl = String.Format("http://api.misty.smeshlink.com/feeds/{0}/0.xml?key=df44011f-3daf-4372-8c14-9ad29a63a5cb&start={1}&end={2}", gatewayId, timeStart, timeEnd);
            string result = RequestGet(strUrl);
            return result;
        }
        
        public XmlDocument RetrieveByMistyAPI1(DateTime start, DateTime end, string gatewayId,GateWayType gt)
        {
            string timeStart = start.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string timeEnd = end.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
            string strUrl = String.Format("http://api.misty.smeshlink.com/feeds/{0}.xml?key=df44011f-3daf-4372-8c14-9ad29a63a5cb&start={1}&end={2}", gatewayId, timeStart, timeEnd);
            XmlDocument result = RequestGet1(strUrl,gt);
            return result;
        }
        public XmlDocument RequestGet1(string strURL,GateWayType gt)
        {
            HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            //request.Headers.Add("X-ApiKey", "3cba154a-07e0-4bd5-b20d-fdabb12ac3d9");
            HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            XmlDocument xdoc = XmlParser.Xml(s,gt);
            
            return xdoc;
        }
       
      
        //调用接口Get请求
        public String RequestGet(string strURL)
        {
            HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            //request.Headers.Add("X-ApiKey", "3cba154a-07e0-4bd5-b20d-fdabb12ac3d9");
            HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

    }
}
