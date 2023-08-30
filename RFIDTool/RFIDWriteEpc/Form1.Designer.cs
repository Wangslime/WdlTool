using System.Drawing;
using System.Windows.Forms;
using System;

namespace RFIDWriteEpc
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            bool flag = disposing && this.components != null;
            if (flag)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Btb_Connect = new Button();
            this.txtRfidNo = new TextBox();
            this.label3 = new Label();
            this.BtnWriteIn = new Button();
            base.SuspendLayout();
            this.Btb_Connect.Font = new Font("楷体", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 134);
            this.Btb_Connect.Location = new Point(32, 133);
            this.Btb_Connect.Name = "Btb_Connect";
            this.Btb_Connect.Size = new Size(114, 37);
            this.Btb_Connect.TabIndex = 0;
            this.Btb_Connect.Text = "连接";
            this.Btb_Connect.UseVisualStyleBackColor = true;
            this.Btb_Connect.Click += new EventHandler(this.Btb_Connect_Click);
            this.txtRfidNo.Font = new Font("宋体", 15f);
            this.txtRfidNo.Location = new Point(131, 47);
            this.txtRfidNo.Name = "txtRfidNo";
            this.txtRfidNo.Size = new Size(201, 30);
            this.txtRfidNo.TabIndex = 54;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 15f);
            this.label3.Location = new Point(28, 51);
            this.label3.Name = "label3";
            this.label3.Size = new Size(69, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "RFID号";
            this.BtnWriteIn.Font = new Font("楷体", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 134);
            this.BtnWriteIn.Location = new Point(218, 133);
            this.BtnWriteIn.Name = "BtnWriteIn";
            this.BtnWriteIn.Size = new Size(114, 37);
            this.BtnWriteIn.TabIndex = 55;
            this.BtnWriteIn.Text = "写入";
            this.BtnWriteIn.UseVisualStyleBackColor = true;
            this.BtnWriteIn.Click += new EventHandler(this.BtnWriteIn_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(385, 214);
            base.Controls.Add(this.BtnWriteIn);
            base.Controls.Add(this.txtRfidNo);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.Btb_Connect);
            base.Name = "Form1";
            this.Text = "RFID读卡器";
            base.FormClosed += new FormClosedEventHandler(this.Form1_FormClosed);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        private Button Btb_Connect;
        private TextBox txtRfidNo;
        private Label label3;
        private Button BtnWriteIn;
    }
}

