namespace Smeshlink海绵城市Client
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelRetrieve = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCondition = new System.Windows.Forms.Label();
            this.labelChooseWeatherStation = new System.Windows.Forms.Label();
            this.comboBoxCondition = new System.Windows.Forms.ComboBox();
            this.comboBoxChooseWeatherStation = new System.Windows.Forms.ComboBox();
            this.panelRetrieveShowData = new System.Windows.Forms.Panel();
            this.dataGridViewRetrieve = new System.Windows.Forms.DataGridView();
            this.buttonRetrieveReturn = new System.Windows.Forms.Button();
            this.buttonGenerateReport = new System.Windows.Forms.Button();
            this.dateTimePickerRetrieveEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonChooseDirectory = new System.Windows.Forms.Button();
            this.labelChooseDirectory = new System.Windows.Forms.Label();
            this.dateTimePickerRetrieveBegin = new System.Windows.Forms.DateTimePicker();
            this.labelRetrieve = new System.Windows.Forms.Label();
            this.panelRetrieve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelRetrieveShowData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetrieve)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRetrieve
            // 
            this.panelRetrieve.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRetrieve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelRetrieve.BackgroundImage")));
            this.panelRetrieve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelRetrieve.Controls.Add(this.label3);
            this.panelRetrieve.Controls.Add(this.label2);
            this.panelRetrieve.Controls.Add(this.label1);
            this.panelRetrieve.Controls.Add(this.button2);
            this.panelRetrieve.Controls.Add(this.pictureBox1);
            this.panelRetrieve.Controls.Add(this.button1);
            this.panelRetrieve.Controls.Add(this.labelCondition);
            this.panelRetrieve.Controls.Add(this.labelChooseWeatherStation);
            this.panelRetrieve.Controls.Add(this.comboBoxCondition);
            this.panelRetrieve.Controls.Add(this.comboBoxChooseWeatherStation);
            this.panelRetrieve.Controls.Add(this.panelRetrieveShowData);
            this.panelRetrieve.Controls.Add(this.buttonGenerateReport);
            this.panelRetrieve.Controls.Add(this.dateTimePickerRetrieveEnd);
            this.panelRetrieve.Controls.Add(this.buttonChooseDirectory);
            this.panelRetrieve.Controls.Add(this.labelChooseDirectory);
            this.panelRetrieve.Controls.Add(this.dateTimePickerRetrieveBegin);
            this.panelRetrieve.Controls.Add(this.labelRetrieve);
            this.panelRetrieve.Location = new System.Drawing.Point(0, 0);
            this.panelRetrieve.Name = "panelRetrieve";
            this.panelRetrieve.Size = new System.Drawing.Size(906, 637);
            this.panelRetrieve.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(238, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "当前执行传感器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(324, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "错误记录";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(28, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "执行进度";
            this.label1.Visible = false;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Smeshlink海绵城市Client.Properties.Resources.未标题_1;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(326, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 38);
            this.button2.TabIndex = 12;
            this.button2.Text = "Post到ENNO";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Smeshlink海绵城市Client.Properties.Resources._1424377706;
            this.pictureBox1.Location = new System.Drawing.Point(418, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Smeshlink海绵城市Client.Properties.Resources.未标题_1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(486, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 38);
            this.button1.TabIndex = 10;
            this.button1.Text = "开始查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // labelCondition
            // 
            this.labelCondition.AutoSize = true;
            this.labelCondition.BackColor = System.Drawing.Color.Transparent;
            this.labelCondition.ForeColor = System.Drawing.SystemColors.Info;
            this.labelCondition.Location = new System.Drawing.Point(26, 124);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(53, 12);
            this.labelCondition.TabIndex = 7;
            this.labelCondition.Text = "查询条件";
            // 
            // labelChooseWeatherStation
            // 
            this.labelChooseWeatherStation.AutoSize = true;
            this.labelChooseWeatherStation.BackColor = System.Drawing.Color.Transparent;
            this.labelChooseWeatherStation.ForeColor = System.Drawing.SystemColors.Info;
            this.labelChooseWeatherStation.Location = new System.Drawing.Point(26, 41);
            this.labelChooseWeatherStation.Name = "labelChooseWeatherStation";
            this.labelChooseWeatherStation.Size = new System.Drawing.Size(53, 12);
            this.labelChooseWeatherStation.TabIndex = 6;
            this.labelChooseWeatherStation.Text = "选择地点";
            // 
            // comboBoxCondition
            // 
            this.comboBoxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCondition.FormattingEnabled = true;
            this.comboBoxCondition.Items.AddRange(new object[] {
            "无",
            "有降雨时的数据",
            "每个小时一条数据"});
            this.comboBoxCondition.Location = new System.Drawing.Point(28, 101);
            this.comboBoxCondition.Name = "comboBoxCondition";
            this.comboBoxCondition.Size = new System.Drawing.Size(263, 20);
            this.comboBoxCondition.TabIndex = 5;
            // 
            // comboBoxChooseWeatherStation
            // 
            this.comboBoxChooseWeatherStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChooseWeatherStation.FormattingEnabled = true;
            this.comboBoxChooseWeatherStation.Items.AddRange(new object[] {
            "中央绿廊微气象监测",
            "云谷10号楼微气象监测",
            "康定和园微气象监测",
            "同德佳苑东侧雨水花园监测"});
            this.comboBoxChooseWeatherStation.Location = new System.Drawing.Point(28, 59);
            this.comboBoxChooseWeatherStation.Name = "comboBoxChooseWeatherStation";
            this.comboBoxChooseWeatherStation.Size = new System.Drawing.Size(263, 20);
            this.comboBoxChooseWeatherStation.TabIndex = 4;
            // 
            // panelRetrieveShowData
            // 
            this.panelRetrieveShowData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRetrieveShowData.Controls.Add(this.dataGridViewRetrieve);
            this.panelRetrieveShowData.Controls.Add(this.buttonRetrieveReturn);
            this.panelRetrieveShowData.Location = new System.Drawing.Point(0, 169);
            this.panelRetrieveShowData.Name = "panelRetrieveShowData";
            this.panelRetrieveShowData.Size = new System.Drawing.Size(906, 468);
            this.panelRetrieveShowData.TabIndex = 3;
            this.panelRetrieveShowData.Visible = false;
            // 
            // dataGridViewRetrieve
            // 
            this.dataGridViewRetrieve.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRetrieve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRetrieve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRetrieve.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRetrieve.Name = "dataGridViewRetrieve";
            this.dataGridViewRetrieve.RowTemplate.Height = 23;
            this.dataGridViewRetrieve.Size = new System.Drawing.Size(906, 468);
            this.dataGridViewRetrieve.TabIndex = 0;
            // 
            // buttonRetrieveReturn
            // 
            this.buttonRetrieveReturn.Location = new System.Drawing.Point(771, 676);
            this.buttonRetrieveReturn.Name = "buttonRetrieveReturn";
            this.buttonRetrieveReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonRetrieveReturn.TabIndex = 1;
            this.buttonRetrieveReturn.Text = "返回";
            this.buttonRetrieveReturn.UseVisualStyleBackColor = true;
            // 
            // buttonGenerateReport
            // 
            this.buttonGenerateReport.AutoSize = true;
            this.buttonGenerateReport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonGenerateReport.Location = new System.Drawing.Point(787, 55);
            this.buttonGenerateReport.Name = "buttonGenerateReport";
            this.buttonGenerateReport.Size = new System.Drawing.Size(111, 23);
            this.buttonGenerateReport.TabIndex = 3;
            this.buttonGenerateReport.Text = "生成报表";
            this.buttonGenerateReport.UseVisualStyleBackColor = false;
            this.buttonGenerateReport.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePickerRetrieveEnd
            // 
            this.dateTimePickerRetrieveEnd.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveEnd.Location = new System.Drawing.Point(486, 57);
            this.dateTimePickerRetrieveEnd.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.Name = "dateTimePickerRetrieveEnd";
            this.dateTimePickerRetrieveEnd.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerRetrieveEnd.TabIndex = 2;
            this.dateTimePickerRetrieveEnd.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // buttonChooseDirectory
            // 
            this.buttonChooseDirectory.AutoSize = true;
            this.buttonChooseDirectory.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonChooseDirectory.Location = new System.Drawing.Point(660, 56);
            this.buttonChooseDirectory.Name = "buttonChooseDirectory";
            this.buttonChooseDirectory.Size = new System.Drawing.Size(111, 23);
            this.buttonChooseDirectory.TabIndex = 2;
            this.buttonChooseDirectory.Text = "选择报表存放目录";
            this.buttonChooseDirectory.UseVisualStyleBackColor = false;
            this.buttonChooseDirectory.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelChooseDirectory
            // 
            this.labelChooseDirectory.AutoSize = true;
            this.labelChooseDirectory.BackColor = System.Drawing.Color.Transparent;
            this.labelChooseDirectory.ForeColor = System.Drawing.SystemColors.Info;
            this.labelChooseDirectory.Location = new System.Drawing.Point(668, 40);
            this.labelChooseDirectory.Name = "labelChooseDirectory";
            this.labelChooseDirectory.Size = new System.Drawing.Size(77, 12);
            this.labelChooseDirectory.TabIndex = 1;
            this.labelChooseDirectory.Text = "报表存储目录";
            // 
            // dateTimePickerRetrieveBegin
            // 
            this.dateTimePickerRetrieveBegin.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveBegin.Location = new System.Drawing.Point(307, 57);
            this.dateTimePickerRetrieveBegin.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.Name = "dateTimePickerRetrieveBegin";
            this.dateTimePickerRetrieveBegin.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerRetrieveBegin.TabIndex = 1;
            this.dateTimePickerRetrieveBegin.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // labelRetrieve
            // 
            this.labelRetrieve.AutoSize = true;
            this.labelRetrieve.BackColor = System.Drawing.Color.Transparent;
            this.labelRetrieve.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRetrieve.Location = new System.Drawing.Point(26, 16);
            this.labelRetrieve.Name = "labelRetrieve";
            this.labelRetrieve.Size = new System.Drawing.Size(131, 12);
            this.labelRetrieve.TabIndex = 0;
            this.labelRetrieve.Text = "Smeshlink数据查询系统";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 637);
            this.Controls.Add(this.panelRetrieve);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DataRetrieve";
            this.panelRetrieve.ResumeLayout(false);
            this.panelRetrieve.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelRetrieveShowData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetrieve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelChooseDirectory;
        private System.Windows.Forms.Button buttonChooseDirectory;
        private System.Windows.Forms.Button buttonGenerateReport;
        private System.Windows.Forms.Panel panelRetrieve;
        private System.Windows.Forms.ComboBox comboBoxChooseWeatherStation;
        private System.Windows.Forms.Panel panelRetrieveShowData;
        private System.Windows.Forms.Button buttonRetrieveReturn;
        private System.Windows.Forms.DataGridView dataGridViewRetrieve;
        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveBegin;
        private System.Windows.Forms.Label labelRetrieve;
        private System.Windows.Forms.Label labelChooseWeatherStation;
        private System.Windows.Forms.ComboBox comboBoxCondition;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}

