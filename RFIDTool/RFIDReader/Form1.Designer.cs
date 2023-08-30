using System.Drawing;
using System.Windows.Forms;
using System;

namespace RFIDReader
{
    partial class Form1
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
            this.Btb_Connect = new Button();
            this.txtRfidNo = new TextBox();
            this.label3 = new Label();
            base.SuspendLayout();
            this.Btb_Connect.Font = new Font("楷体", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 134);
            this.Btb_Connect.Location = new Point(322, 42);
            this.Btb_Connect.Name = "Btb_Connect";
            this.Btb_Connect.Size = new Size(114, 37);
            this.Btb_Connect.TabIndex = 0;
            this.Btb_Connect.Text = "连接";
            this.Btb_Connect.UseVisualStyleBackColor = true;
            this.Btb_Connect.Click += new EventHandler(this.Btb_Connect_Click);
            this.txtRfidNo.Font = new Font("宋体", 15f);
            this.txtRfidNo.Location = new Point(131, 47);
            this.txtRfidNo.Name = "txtRfidNo";
            this.txtRfidNo.Size = new Size(124, 30);
            this.txtRfidNo.TabIndex = 54;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 15f);
            this.label3.Location = new Point(47, 51);
            this.label3.Name = "label3";
            this.label3.Size = new Size(69, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "RFID号";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(498, 130);
            base.Controls.Add(this.txtRfidNo);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.Btb_Connect);
            base.Name = "Form1";
            this.Text = "RFID读卡器";
            base.FormClosed += new FormClosedEventHandler(this.Form1_FormClosed);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Btb_Connect;
        private System.Windows.Forms.TextBox txtRfidNo;
        private Label label3;
    }
}

