﻿namespace GUIDE.PLATFORM.MyControl
{
    partial class BridgeControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.spreaderControl1 = new GUIDE.PLATFORM.MyControl.SpreaderControl(this.components);
            this.SuspendLayout();
            // 
            // spreaderControl1
            // 
            this.spreaderControl1.Location = new System.Drawing.Point(127, 28);
            this.spreaderControl1.Name = "spreaderControl1";
            this.spreaderControl1.Size = new System.Drawing.Size(69, 64);
            this.spreaderControl1.TabIndex = 0;
            this.spreaderControl1.Text = "spreaderControl1";
            // 
            // BridgeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(25)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.spreaderControl1);
            this.Name = "BridgeControl";
            this.Size = new System.Drawing.Size(498, 412);
            this.ResumeLayout(false);

        }

        #endregion

        private SpreaderControl spreaderControl1;
    }
}
