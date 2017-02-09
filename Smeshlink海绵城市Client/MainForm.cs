using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Smeshlink海绵城市Client.DLL;
using Smeshlink海绵城市Client.Device;

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

        private void LoadConfig()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("config");
                XmlNodeList sensors = xml.FirstChild.SelectNodes("sensor");
                comboBoxChooseWeatherStation.Items.Clear();
                List<Sensor> listSensors = new List<Sensor>();
                foreach (XmlNode item in sensors)
                {
                    Sensor s = new Sensor();
                    s.Name = item.SelectSingleNode("name").InnerText;
                    s.Node = item.SelectSingleNode("node").InnerText;
                    s.Gateway = item.SelectSingleNode("gateway").InnerText;
                    s.Model = item.SelectSingleNode("model").InnerText;
                    listSensors.Add(s);
                }
                comboBoxChooseWeatherStation.Items.AddRange(listSensors.ToArray());
            }
            catch (Exception ex) { MessageBox.Show("配置文件载入异常" + ex.Message);
              }
        }

        private DataSet GetMX(Sensor ss, DateTime start, DateTime end)
        {
            DataSet ds = new DataSet();
            XmlDocument xdoc = new XmlDocument();
            MX mx = null;
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
                ExcelLibrary.DataSetHelper.CreateWorkbook(labelChooseDirectory.Text + "\\" +comboBoxChooseWeatherStation.Text+dateTimePickerRetrieveBegin.Value.ToString("MMdd")+"至"+dateTimePickerRetrieveEnd.Value.ToString("MMdd") +"表.xls", dsWhole);
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

        DataSet dsWhole;Boolean IsWeatherStation;
    

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
    }
}
