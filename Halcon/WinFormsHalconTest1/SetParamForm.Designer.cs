namespace WinFormsHalconTest1
{
    partial class SetParamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            CmbMetrologyType = new ComboBox();
            TxtLen1 = new TextBox();
            TxtLen2 = new TextBox();
            TxtSigama = new TextBox();
            txtContrant = new TextBox();
            BtnAddParam = new Button();
            BtnParamClose = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 33);
            label1.Name = "label1";
            label1.Size = new Size(92, 17);
            label1.TabIndex = 0;
            label1.Text = "测量对象名称：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 83);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 0;
            label2.Text = "卡尺厂半径：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 133);
            label3.Name = "label3";
            label3.Size = new Size(80, 17);
            label3.TabIndex = 0;
            label3.Text = "卡尺短半径：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 183);
            label4.Name = "label4";
            label4.Size = new Size(92, 17);
            label4.TabIndex = 0;
            label4.Text = "高斯平滑系数：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 233);
            label5.Name = "label5";
            label5.Size = new Size(92, 17);
            label5.TabIndex = 0;
            label5.Text = "最新边缘振幅：";
            // 
            // CmbMetrologyType
            // 
            CmbMetrologyType.FormattingEnabled = true;
            CmbMetrologyType.Location = new Point(126, 29);
            CmbMetrologyType.Name = "CmbMetrologyType";
            CmbMetrologyType.Size = new Size(121, 25);
            CmbMetrologyType.TabIndex = 1;
            // 
            // TxtLen1
            // 
            TxtLen1.Location = new Point(126, 80);
            TxtLen1.Name = "TxtLen1";
            TxtLen1.Size = new Size(121, 23);
            TxtLen1.TabIndex = 2;
            TxtLen1.Text = "40";
            // 
            // TxtLen2
            // 
            TxtLen2.Location = new Point(126, 129);
            TxtLen2.Name = "TxtLen2";
            TxtLen2.Size = new Size(121, 23);
            TxtLen2.TabIndex = 2;
            TxtLen2.Text = "10";
            // 
            // TxtSigama
            // 
            TxtSigama.Location = new Point(126, 178);
            TxtSigama.Name = "TxtSigama";
            TxtSigama.Size = new Size(121, 23);
            TxtSigama.TabIndex = 2;
            TxtSigama.Text = "15";
            // 
            // txtContrant
            // 
            txtContrant.Location = new Point(126, 227);
            txtContrant.Name = "txtContrant";
            txtContrant.Size = new Size(121, 23);
            txtContrant.TabIndex = 2;
            txtContrant.Text = "30";
            // 
            // BtnAddParam
            // 
            BtnAddParam.Location = new Point(34, 277);
            BtnAddParam.Name = "BtnAddParam";
            BtnAddParam.Size = new Size(75, 23);
            BtnAddParam.TabIndex = 3;
            BtnAddParam.Text = "添加参数";
            BtnAddParam.UseVisualStyleBackColor = true;
            BtnAddParam.Click += BtnAddParam_Click;
            // 
            // BtnParamClose
            // 
            BtnParamClose.Location = new Point(174, 277);
            BtnParamClose.Name = "BtnParamClose";
            BtnParamClose.Size = new Size(75, 23);
            BtnParamClose.TabIndex = 3;
            BtnParamClose.Text = "关闭";
            BtnParamClose.UseVisualStyleBackColor = true;
            BtnParamClose.Click += BtnParamClose_Click;
            // 
            // ParamForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(301, 321);
            Controls.Add(BtnParamClose);
            Controls.Add(BtnAddParam);
            Controls.Add(txtContrant);
            Controls.Add(TxtSigama);
            Controls.Add(TxtLen2);
            Controls.Add(TxtLen1);
            Controls.Add(CmbMetrologyType);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ParamForm";
            Text = "测量模型参数设置";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox CmbMetrologyType;
        private TextBox TxtLen1;
        private TextBox TxtLen2;
        private TextBox TxtSigama;
        private TextBox txtContrant;
        private Button BtnAddParam;
        private Button BtnParamClose;
    }
}