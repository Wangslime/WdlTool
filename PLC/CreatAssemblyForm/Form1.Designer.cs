namespace CreatAssemblyForm
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
            this.TxtAssemblyName = new System.Windows.Forms.TextBox();
            this.BtnLoadExcel = new System.Windows.Forms.Button();
            this.BtnSaveDllPath = new System.Windows.Forms.Button();
            this.TxtSaveDllPath = new System.Windows.Forms.TextBox();
            this.BtnSetAssemblyName = new System.Windows.Forms.Button();
            this.TxtLoadExcel = new System.Windows.Forms.TextBox();
            this.BtnCreatDll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtAssemblyName
            // 
            this.TxtAssemblyName.Location = new System.Drawing.Point(33, 92);
            this.TxtAssemblyName.Name = "TxtAssemblyName";
            this.TxtAssemblyName.Size = new System.Drawing.Size(346, 21);
            this.TxtAssemblyName.TabIndex = 0;
            // 
            // BtnLoadExcel
            // 
            this.BtnLoadExcel.Location = new System.Drawing.Point(396, 30);
            this.BtnLoadExcel.Name = "BtnLoadExcel";
            this.BtnLoadExcel.Size = new System.Drawing.Size(139, 21);
            this.BtnLoadExcel.TabIndex = 1;
            this.BtnLoadExcel.Text = "加载Excel文件路径";
            this.BtnLoadExcel.UseVisualStyleBackColor = true;
            this.BtnLoadExcel.Click += new System.EventHandler(this.BtnLoadExcel_Click);
            // 
            // BtnSaveDllPath
            // 
            this.BtnSaveDllPath.Location = new System.Drawing.Point(396, 154);
            this.BtnSaveDllPath.Name = "BtnSaveDllPath";
            this.BtnSaveDllPath.Size = new System.Drawing.Size(139, 21);
            this.BtnSaveDllPath.TabIndex = 3;
            this.BtnSaveDllPath.Text = "保存程序集dll路径";
            this.BtnSaveDllPath.UseVisualStyleBackColor = true;
            this.BtnSaveDllPath.Click += new System.EventHandler(this.BtnSaveDllPath_Click);
            // 
            // TxtSaveDllPath
            // 
            this.TxtSaveDllPath.Location = new System.Drawing.Point(33, 154);
            this.TxtSaveDllPath.Name = "TxtSaveDllPath";
            this.TxtSaveDllPath.Size = new System.Drawing.Size(346, 21);
            this.TxtSaveDllPath.TabIndex = 2;
            // 
            // BtnSetAssemblyName
            // 
            this.BtnSetAssemblyName.Location = new System.Drawing.Point(396, 91);
            this.BtnSetAssemblyName.Name = "BtnSetAssemblyName";
            this.BtnSetAssemblyName.Size = new System.Drawing.Size(139, 21);
            this.BtnSetAssemblyName.TabIndex = 5;
            this.BtnSetAssemblyName.Text = "设置程序集文件名称";
            this.BtnSetAssemblyName.UseVisualStyleBackColor = true;
            this.BtnSetAssemblyName.Click += new System.EventHandler(this.BtnSetAssemblyName_Click);
            // 
            // TxtLoadExcel
            // 
            this.TxtLoadExcel.Location = new System.Drawing.Point(33, 30);
            this.TxtLoadExcel.Name = "TxtLoadExcel";
            this.TxtLoadExcel.Size = new System.Drawing.Size(346, 21);
            this.TxtLoadExcel.TabIndex = 4;
            // 
            // BtnCreatDll
            // 
            this.BtnCreatDll.Location = new System.Drawing.Point(396, 201);
            this.BtnCreatDll.Name = "BtnCreatDll";
            this.BtnCreatDll.Size = new System.Drawing.Size(139, 21);
            this.BtnCreatDll.TabIndex = 6;
            this.BtnCreatDll.Text = "生成程序集";
            this.BtnCreatDll.UseVisualStyleBackColor = true;
            this.BtnCreatDll.Click += new System.EventHandler(this.BtnCreatDll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 234);
            this.Controls.Add(this.BtnCreatDll);
            this.Controls.Add(this.BtnSetAssemblyName);
            this.Controls.Add(this.TxtLoadExcel);
            this.Controls.Add(this.BtnSaveDllPath);
            this.Controls.Add(this.TxtSaveDllPath);
            this.Controls.Add(this.BtnLoadExcel);
            this.Controls.Add(this.TxtAssemblyName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtAssemblyName;
        private System.Windows.Forms.Button BtnLoadExcel;
        private System.Windows.Forms.Button BtnSaveDllPath;
        private System.Windows.Forms.TextBox TxtSaveDllPath;
        private System.Windows.Forms.Button BtnSetAssemblyName;
        private System.Windows.Forms.TextBox TxtLoadExcel;
        private System.Windows.Forms.Button BtnCreatDll;
    }
}

