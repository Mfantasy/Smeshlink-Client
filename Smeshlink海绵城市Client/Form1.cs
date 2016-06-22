using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


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
        
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
                labelChooseDirectory.Text = fbd.SelectedPath;
        }

        DataSet dsWhole;
        private void button2_Click(object sender, EventArgs e)
        {
            if (labelChooseDirectory.Text == "报表存储目录")
                button1_Click(null, null);
            ExcelLibrary.DataSetHelper.CreateWorkbook(labelChooseDirectory.Text + "\\我是报表.xls",dsWhole);
        }
        public void Initial()
        {
            dateTimePickerRetrieveBegin.MaxDate = DateTime.Now;
            dateTimePickerRetrieveEnd.MaxDate = DateTime.Now;
            comboBoxChooseWeatherStation.SelectedIndex = 0;
            comboBoxCondition.SelectedIndex = 2;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {      
            string weatherStation = comboBoxChooseWeatherStation.Text; //"yyyy/MM/dd hh:mm"
            string timeBegin = dateTimePickerRetrieveBegin.Value.ToString();
            string timeEnd = dateTimePickerRetrieveEnd.Value.ToString();
            string condition = comboBoxCondition.Text;
             DataSet ds = GetData(weatherStation,timeBegin,timeEnd,condition);
             dsWhole = ds;       
             dataGridViewRetrieve.DataSource = ds.Tables[0];
            panelRetrieveShowData.Visible = true;
        }
        private DataSet GetData(string weatherStation,string timeBegin,string timeEnd,string condition)
        {
            TcpClient tt = new TcpClient();
            DataSet dd = new DataSet();
            tt.Connect("119.10.1.156", 9000);
            //tt.Connect("127.0.0.1",6666);
            NetworkStream stream = tt.GetStream();
            //int currentSize = 0; int readSize = 0;
            //string weatherStation = "云谷10号楼微气象监测-";
            //string timeBegin = "2016/5/02 00:00-"; string timeEnd = "2016/5/07 00:00-"; string condition = "每个小时一条数据";
            string send = weatherStation +"-"+ timeBegin+"-" + timeEnd+"-" + condition;
            //byte[] totalBytes = new byte[1024];
            byte[] bytes = Encoding.UTF8.GetBytes(send);
            stream.Write(bytes, 0, bytes.Length);
            //while (stream.DataAvailable || currentSize < 1024 * 16)
            //{          
            //    readSize = stream.Read(totalBytes, currentSize, totalBytes.Length - currentSize);
            //    if (readSize > 0)
            //    {
            //        currentSize += readSize;
            //    }
            //    else
            //        break;
            //}
            //string x = Encoding.UTF8.GetString(totalBytes, 0, totalBytes.Length);
            //XmlDocument xd = new XmlDocument();
            //xd.LoadXml(x);

            dd.ReadXml(stream);
            return dd;
        }
    }
}
