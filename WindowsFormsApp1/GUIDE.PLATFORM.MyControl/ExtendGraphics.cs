using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIDE.PLATFORM.MyControl
{
    public static class ExtendGraphics
    {
        private static Graphics TempGraphics;

        /// <summary>
        /// 提供一个Graphics，常用于需要计算文字大小时
        /// </summary>
        /// <returns>大小</returns>
        public static Graphics Graphics()
        {
            if (TempGraphics == null)
            {
                Bitmap bmp = new Bitmap(1, 1);
                TempGraphics = bmp.Graphics();
            }
            return TempGraphics;
        }

        /// <summary>
        /// 计算文字大小
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <returns>大小</returns>
        public static SizeF MeasureString(this string text, Font font)
        {
            return Graphics().MeasureString(text, font);
        }

        public static Graphics Graphics(this Image image)
        {
            return System.Drawing.Graphics.FromImage(image);
        }

        public static Font GetFitFont(this Graphics g, string text, Rectangle rect, Font font)
        {
            float fontSizeLow = 1;
            float fontSizeHigh = rect.Height;
            float fontSizeMid;
            // 循环计算字体大小
            do
            {
                fontSizeMid = (fontSizeLow + fontSizeHigh) / 2;
                if (fontSizeMid > 1)
                {
                    Font font1 = new Font(font.FontFamily, fontSizeMid, font.Style);
                    SizeF size = g.MeasureString(text, font1);
                    if (size.Height <= rect.Height - rect.Height / 8 && size.Width <= rect.Width - rect.Width / 8)
                    {
                        fontSizeLow = fontSizeMid + 0.5f;
                    }
                    else
                    {
                        fontSizeHigh = fontSizeMid - 0.5f;
                    }
                    font1.Dispose();
                }
            }
            while (fontSizeLow <= fontSizeHigh);
            return new Font(font.FontFamily, fontSizeMid, font.Style);
        }

        public static Font GetFitFont(this Graphics g, string text, RectangleF rect, Font font)
        {
            float fontSizeLow = 1;
            float fontSizeHigh = rect.Height;
            float fontSizeMid;
            // 循环计算字体大小
            do
            {
                fontSizeMid = (fontSizeLow + fontSizeHigh) / 2;
                Font font1 = new Font(font.FontFamily, fontSizeMid, font.Style);
                SizeF size = g.MeasureString(text, font1);
                if (size.Height <= rect.Height - rect.Height / 8 && size.Width <= rect.Width - rect.Width / 8)
                {
                    fontSizeLow = fontSizeMid + 0.5f;
                }
                else
                {
                    fontSizeHigh = fontSizeMid - 0.5f;
                }
                font1.Dispose();
            }
            while (fontSizeLow <= fontSizeHigh);
            return new Font(font.FontFamily, fontSizeMid, font.Style);
        }
    }
}
