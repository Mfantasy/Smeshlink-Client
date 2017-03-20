using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Smeshlink海绵城市Client.DLL;
using Smeshlink海绵城市Client.Device;
using System.IO;

public enum GateWayType
{
    MXS5000, MXS1402
}
namespace Smeshlink海绵城市Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadConfig();
            Initial();
        }
        List<Sensor> listSensors;
        private void LoadConfig()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("config.xml");
                XmlNodeList sensors = xml.FirstChild.SelectNodes("sensor");
                comboBoxChooseWeatherStation.Items.Clear();
                listSensors = new List<Sensor>();
                foreach (XmlNode item in sensors)
                {
                    Sensor s = new Sensor();
                    s.Name = item.SelectSingleNode("name").InnerText;
                    s.Node = item.SelectSingleNode("node").InnerText;
                    s.Gateway = item.SelectSingleNode("gateway").InnerText;
                    s.Model = item.SelectSingleNode("model").InnerText;
                    s.SiteWhereId = item.SelectSingleNode("sitewhere").InnerText;
                    listSensors.Add(s);
                }
                comboBoxChooseWeatherStation.Items.AddRange(listSensors.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置文件载入异常" + ex.Message);
            }
        }
        DataTable GetData(string name, int start, int end, int max)
        {
            DataTable table = new DataTable();
            table.Columns.Add("名称");
            table.Columns.Add("液位");
            table.Columns.Add("时间");
            DateTime dtStart = DateTime.Parse("2017-3-11 22:00:00");
            table.Rows.Add(name, start, dtStart);
            DateTime dtRainSt = DateTime.Parse("2017-3-12 5:00:00");
            DateTime dtRainMax = DateTime.Parse("2017-3-12 6:30:00");
            DateTime dtRainEd = DateTime.Parse("2017-3-13 13:00:00");
            DateTime dtEnd = DateTime.Parse("2017-3-14 0:00:00");
            Random rTime = new Random();
            Random rValue = new Random();
            int addSecond = rTime.Next(-10, 11);
            int addValue = rValue.Next(-2, 4);
            DateTime nextTime = dtStart.AddSeconds(60 + addSecond);
            int newValue = start + addValue;
            while (nextTime < dtRainSt)
            {
                addSecond = rTime.Next(-10, 11);
                addValue = rValue.Next(-2, 4);
                table.Rows.Add(name, newValue, nextTime);
                newValue = start + addValue;
                nextTime = nextTime.AddSeconds(60 * 5 + addSecond);
            }
            for (int i = 0; i < 18; i++)
            {
                addSecond = rTime.Next(-10, 11);
                addValue = rValue.Next(-2, 4) + i / 2;
                table.Rows.Add(name, newValue, nextTime);
                newValue += addValue;
                newValue = newValue > max ? max : newValue;
                nextTime = nextTime.AddSeconds(60 * 5 + addSecond);
            }

            while (nextTime < dtRainEd)
            {
                addSecond = rTime.Next(-10, 11);
                addValue = rValue.Next(-2, 4);
                table.Rows.Add(name, newValue, nextTime);
                newValue = max + addValue;
                nextTime = nextTime.AddSeconds(60 * 5 + addSecond);
            }
            bool isEnd = false;
            while (nextTime < dtEnd)
            {
                int ad = 7;
                addSecond = rTime.Next(-10, 11);
                if (newValue > end && !isEnd)
                {
                    addValue = rValue.Next(-4, 1) * ad;
                    ad--;
                    newValue += addValue;
                }
                else
                {
                    isEnd = true;
                    addValue = rValue.Next(-2, 4);
                    newValue = end + addValue;
                }
                table.Rows.Add(name, newValue, nextTime);
                nextTime = nextTime.AddSeconds(60 * 5 + addSecond);
            }
            return table;
        }
        MX mx;
        private DataSet GetMX(Sensor ss, DateTime start, DateTime end)
        {
            DataSet ds = new DataSet();
            XmlDocument xdoc = new XmlDocument();
            //MX mx = null;
            switch (ss.Model.ToUpper())
            {
                case "MXS5000":
                    mx = new MXS5000();
                    break;
                case "MXS1501":
                    mx = new MXS1501();
                    break;
                case "MXS1201":
                    mx = new MXS1201();
                    break;
                case "MXS1402":
                    mx = new MXS1402();
                    break;
                case "MXS1204":
                    mx = new MXS1204();
                    break;
                case "MXN880":
                    mx = new MXN880();
                    break;
                case "MX9000":
                    mx = new MX9000();
                    break;
                case "MX7200":
                    mx = new MX7200();
                    break;
                case "MX6100":
                    mx = new MX6100();
                    break;
                case "MX8100":
                case "MX8000":
                    mx = new MX8100();
                    break;


            }
            xdoc = mx.GetXdoc(start, end, ss);
            if (xdoc == null)
                return null;
            XmlNodeReader xnr = new XmlNodeReader(xdoc);
            ds.ReadXml(xnr);
            return ds;
        }

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
                ExcelLibrary.DataSetHelper.CreateWorkbook(labelChooseDirectory.Text + "\\" + comboBoxChooseWeatherStation.Text + dateTimePickerRetrieveBegin.Value.ToString("MMdd") + "至" + dateTimePickerRetrieveEnd.Value.ToString("MMdd") + "表.xls", dsWhole);
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
            string s1 = "尚业路植生滞留槽单体设施网络液位";
            string s2 = "绿色屋顶10号楼单体设施液位采集";
            //if (comboBoxChooseWeatherStation.Text == s1)
            //{
            //    DataTable table = GetData(s1, 142, 165, 198);
            //    dsWhole.Tables.Add(table);
            //    dataGridViewRetrieve.DataSource = table;
            //    //dataGridViewRetrieve.Visible = true;
            //    panelRetrieveShowData.Visible = true;
            //    return;
            //}
            //if (comboBoxChooseWeatherStation.Text == s2)
            //{
            //    DataTable table = GetData(s2, 125, 160, 189);
            //    dsWhole.Tables.Add(table);
            //    dataGridViewRetrieve.DataSource = table;
            //    //dataGridViewRetrieve.Visible = true;
            //    panelRetrieveShowData.Visible = true;
            //    return;
            //}
            try
            {
                DateTime start = dateTimePickerRetrieveBegin.Value;
                DateTime end = dateTimePickerRetrieveEnd.Value;
                if (start > end)
                {
                    MessageBox.Show("查询开始时间应小于结束时间");
                    return;
                }
                //if ((end - start).Days >= 33)
                //{
                //    MessageBox.Show("不允许查询30天以上的数据");
                //    return;
                //}
                panelRetrieveShowData.Visible = false;
                pictureBox1.Visible = true;
                buttonGenerateReport.Enabled = false;
                button1.Enabled = false;
                comboBoxChooseWeatherStation.Enabled = false;
                comboBoxCondition.Enabled = false;
                Sensor ss = (Sensor)comboBoxChooseWeatherStation.SelectedItem;
                IsWeatherStation = (ss.Model.ToUpper() == "MXS5000");

                string gatewayId = String.Empty;

                OverDelegate overDel = Over;
                ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
                {
                    //string data = RetrieveByMistyAPI(start, end, gatewayId);
                    // XmlDocument xdoc = XmlParser.Xml(data);
                    DataSet ds = GetMX(ss, start, end);
                    if (ds == null)
                    {
                        this.Invoke(new Action(Over));
                        MessageBox.Show("未查询到数据");
                        return;
                    }
                    overDel.Invoke(ds);
                }), overDel);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        delegate void OverDelegate(DataSet ds);
        void Over()
        {
            comboBoxChooseWeatherStation.Enabled = true;
            comboBoxCondition.Enabled = true;
            button1.Enabled = true;
            buttonGenerateReport.Enabled = true;
            pictureBox1.Visible = false;
        }
        void Over(DataSet ds)
        {
            this.Invoke(new Action(() =>
            {
                dsWhole = ds;
                if (IsWeatherStation)
                    ds = ChooseData(comboBoxCondition.Text, dsWhole);
                dataGridViewRetrieve.DataSource = ds.Tables[0];
                panelRetrieveShowData.Visible = true;

                Over();

                // MessageBox.Show("查询成功");
            }));
        }

        DataSet dsWhole; Boolean IsWeatherStation;


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
                    if ((string)ds.Tables[0].Rows[i]["雨量"] == rainfall)
                    {
                        dsNew.Tables[0].Rows[i].Delete();
                        count--;
                        i--;
                    }
                    else
                        rainfall = (string)ds.Tables[0].Rows[i]["雨量"];
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTime start = dateTimePickerRetrieveBegin.Value;
            DateTime end = dateTimePickerRetrieveEnd.Value;
            Sensor ss = (Sensor)comboBoxChooseWeatherStation.SelectedItem;
            GetMx(ss);
            ThreadPool.QueueUserWorkItem((o) => { mx.Post(start, end, ss); MessageBox.Show("PostOK"); });            
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 3 * 1000;
            t.Tick += T_Tick;
            t.Start();            
        }

        private void T_Tick(object sender, EventArgs e)
        {
            label3.Text = currentSensor;
            label1.Text = mx.GetWorkState();
            label2.Text = mx.GetErrorState();
        }
        string currentSensor = "";

        class TimeGroup
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        void BackgroundPost()
        {
            //Thread th = new Thread(BackgroundPost);
            //th.IsBackground = true;
            //th.Start();
            //System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            //t.Interval = 3 * 1000;
            //t.Tick += T_Tick;
            //t.Start();
            List<TimeGroup> timeList = new List<TimeGroup>();
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/6/16 0:00:00"), End = DateTime.Parse("2016/7/1 0:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/7/1 0:00:00"), End = DateTime.Parse("2016/7/5 9:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/7/8 10:00:00"), End = DateTime.Parse("2016/7/8 14:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/7/14 0:00:00"), End = DateTime.Parse("2016/7/26 19:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/7/30 23:00:00"), End = DateTime.Parse("2016/8/1 0:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/8/7 12:00:00"), End = DateTime.Parse("2016/8/9 21:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/8/17 6:00:00"), End = DateTime.Parse("2016/8/19 14:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/8/25 21:00:00"), End = DateTime.Parse("2016/8/28 2:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/8/30 23:00:00"), End = DateTime.Parse("2016/9/1 0:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/9/29 23:00:00"), End = DateTime.Parse("2016/10/1 0:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/10/29 23:00:00"), End = DateTime.Parse("2016/11/1 0:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/11/20 21:00:00"), End = DateTime.Parse("2016/11/22 10:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/11/25 14:00:00"), End = DateTime.Parse("2016/11/26 23:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2016/11/29 23:00:00"), End = DateTime.Parse("2016/12/19 21:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2017/1/16 22:00:00"), End = DateTime.Parse("2017/1/17 11:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2017/1/24 18:00:00"), End = DateTime.Parse("2017/2/7 17:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2017/2/11 22:00:00"), End = DateTime.Parse("2017/2/12 17:00:00") });
            timeList.Add(new TimeGroup() { Start = DateTime.Parse("2017/2/17 20:00:00"), End = DateTime.Parse("2017/3/7 10:00:00") });


            foreach (var time in timeList)
            {
                DateTime start = time.Start;
                DateTime end = time.End;
                foreach (Sensor item in listSensors)
                {
                    GetMx(item);
                    currentSensor = item.Name;                                                                                
                    mx.Post(start, end, item);                                                            
                }
            }
        }

        private void GetMx(Sensor item)
        {
            switch (item.Model.ToUpper())
            {
                case "MXS5000":
                    mx = new MXS5000();
                    break;
                case "MXS1501":
                    mx = new MXS1501();
                    break;
                case "MXS1201":
                    mx = new MXS1201();
                    break;
                case "MXS1402":
                    mx = new MXS1402();
                    break;
                case "MXS1204":
                    mx = new MXS1204();
                    break;
                case "MXN880":
                    mx = new MXN880();
                    break;
                case "MX9000":
                    mx = new MX9000();
                    break;
                case "MX7200":
                    mx = new MX7200();
                    break;
                case "MX6100":
                    mx = new MX6100();
                    break;
                case "MX8100":
                case "MX8000":
                    mx = new MX8100();
                    break;
            }
        }
    }
}
