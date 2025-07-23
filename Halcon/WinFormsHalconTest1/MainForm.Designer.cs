namespace WinFormsHalconTest1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            hsControl = new HalconDotNet.HSmartWindowControl();
            groupBox2 = new GroupBox();
            BtnResult = new Button();
            BtnCreatMetrologyModel = new Button();
            BtnDrawRectangle = new Button();
            BtbEllipse = new Button();
            BtnDrawCircle = new Button();
            BtnDrawLine = new Button();
            BtnLoadImage = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(hsControl);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(649, 485);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "图像展示";
            // 
            // hsControl
            // 
            hsControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            hsControl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            hsControl.Dock = DockStyle.Fill;
            hsControl.HDoubleClickToFitContent = true;
            hsControl.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            hsControl.HImagePart = new Rectangle(-81, 21, 802, 436);
            hsControl.HKeepAspectRatio = true;
            hsControl.HMoveContent = true;
            hsControl.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            hsControl.Location = new Point(3, 19);
            hsControl.Margin = new Padding(0);
            hsControl.Name = "hsControl";
            hsControl.Size = new Size(643, 463);
            hsControl.TabIndex = 0;
            hsControl.WindowSize = new Size(643, 463);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(BtnResult);
            groupBox2.Controls.Add(BtnCreatMetrologyModel);
            groupBox2.Controls.Add(BtnDrawRectangle);
            groupBox2.Controls.Add(BtbEllipse);
            groupBox2.Controls.Add(BtnDrawCircle);
            groupBox2.Controls.Add(BtnDrawLine);
            groupBox2.Controls.Add(BtnLoadImage);
            groupBox2.Location = new Point(667, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(180, 485);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作";
            // 
            // BtnResult
            // 
            BtnResult.BackColor = Color.Lime;
            BtnResult.Location = new Point(19, 238);
            BtnResult.Name = "BtnResult";
            BtnResult.Size = new Size(142, 30);
            BtnResult.TabIndex = 0;
            BtnResult.Text = "执行测量并获取结果";
            BtnResult.UseVisualStyleBackColor = false;
            BtnResult.Click += BtnResult_Click;
            // 
            // BtnCreatMetrologyModel
            // 
            BtnCreatMetrologyModel.BackColor = Color.Lime;
            BtnCreatMetrologyModel.Location = new Point(19, 202);
            BtnCreatMetrologyModel.Name = "BtnCreatMetrologyModel";
            BtnCreatMetrologyModel.Size = new Size(142, 30);
            BtnCreatMetrologyModel.TabIndex = 0;
            BtnCreatMetrologyModel.Text = "创建测量模型参数";
            BtnCreatMetrologyModel.UseVisualStyleBackColor = false;
            BtnCreatMetrologyModel.Click += BtnCreatMetrologyModel_Click;
            // 
            // BtnDrawRectangle
            // 
            BtnDrawRectangle.BackColor = Color.Lime;
            BtnDrawRectangle.Location = new Point(19, 166);
            BtnDrawRectangle.Name = "BtnDrawRectangle";
            BtnDrawRectangle.Size = new Size(142, 30);
            BtnDrawRectangle.TabIndex = 0;
            BtnDrawRectangle.Text = "绘制矩形测量";
            BtnDrawRectangle.UseVisualStyleBackColor = false;
            BtnDrawRectangle.Click += BtnDrawRectangle_Click;
            // 
            // BtbEllipse
            // 
            BtbEllipse.BackColor = Color.Lime;
            BtbEllipse.Location = new Point(19, 130);
            BtbEllipse.Name = "BtbEllipse";
            BtbEllipse.Size = new Size(142, 30);
            BtbEllipse.TabIndex = 0;
            BtbEllipse.Text = "绘制椭圆测量";
            BtbEllipse.UseVisualStyleBackColor = false;
            BtbEllipse.Click += BtbEllipse_Click;
            // 
            // BtnDrawCircle
            // 
            BtnDrawCircle.BackColor = Color.Lime;
            BtnDrawCircle.Location = new Point(19, 94);
            BtnDrawCircle.Name = "BtnDrawCircle";
            BtnDrawCircle.Size = new Size(142, 30);
            BtnDrawCircle.TabIndex = 0;
            BtnDrawCircle.Text = "绘制圆形测量";
            BtnDrawCircle.UseVisualStyleBackColor = false;
            BtnDrawCircle.Click += BtnDrawCircle_Click;
            // 
            // BtnDrawLine
            // 
            BtnDrawLine.BackColor = Color.Lime;
            BtnDrawLine.Location = new Point(19, 58);
            BtnDrawLine.Name = "BtnDrawLine";
            BtnDrawLine.Size = new Size(142, 30);
            BtnDrawLine.TabIndex = 0;
            BtnDrawLine.Text = "绘制直线测量";
            BtnDrawLine.UseVisualStyleBackColor = false;
            BtnDrawLine.Click += BtnDrawLine_Click;
            // 
            // BtnLoadImage
            // 
            BtnLoadImage.BackColor = Color.Lime;
            BtnLoadImage.Location = new Point(19, 22);
            BtnLoadImage.Name = "BtnLoadImage";
            BtnLoadImage.Size = new Size(142, 30);
            BtnLoadImage.TabIndex = 0;
            BtnLoadImage.Text = "加载测量图像";
            BtnLoadImage.UseVisualStyleBackColor = false;
            BtnLoadImage.Click += BtnLoadImage_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 509);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button BtnResult;
        private Button BtnCreatMetrologyModel;
        private Button BtnDrawRectangle;
        private Button BtbEllipse;
        private Button BtnDrawCircle;
        private Button BtnDrawLine;
        private Button BtnLoadImage;
        private HalconDotNet.HSmartWindowControl hsControl;
    }
}
