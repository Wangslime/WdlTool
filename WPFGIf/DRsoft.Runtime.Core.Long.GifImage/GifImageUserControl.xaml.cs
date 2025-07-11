using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using UserControl = System.Windows.Controls.UserControl;

namespace DRsoft.Runtime.Core.Long.GifImage;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class GifImageControl : UserControl
{
    [Bindable(true)]
    [Category("Appearance")]
    public string Source
    {
        get { return GetValue(SourceProperty).ToString(); }
        set { SetValue(SourceProperty, value); }
    }
    public readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(GifImageControl));

    [Bindable(true)]
    [Category("Appearance")]
    public float PlaybackRate
    {
        get { return (float)GetValue(PlaybackRateProperty); }
        set { SetValue(PlaybackRateProperty, value); }
    }
    public readonly DependencyProperty PlaybackRateProperty = DependencyProperty.Register("PlaybackRate", typeof(float), typeof(GifImageControl));

    WriteableBitmap _bitmap = null;
    Image gifImage = null;
    int width = -99;
    int height = -99;
    bool IsLoaded = false;
    bool IsSizeChanged = false;
    FrameDimension frameDimension = null;
    List<int> delays = new List<int>();
    int frameCount = 0;

    public GifImageControl()
    {
        InitializeComponent();

        this.IsVisibleChanged += GifImageControl_IsVisibleChanged;
        this.Loaded += GifImageControl_Loaded;
        this.SizeChanged += GifImageControl_SizeChanged;
    }

    private void GifImageControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        IsSizeChanged = false;
        width = (int)e.NewSize.Width;
        height = (int)e.NewSize.Height;
        IsSizeChanged = true;
    }

    private void GifImageControl_Loaded(object sender, RoutedEventArgs e)
    {
        string gifPath = Source;
        float playbackPate = 1;
        if (PlaybackRate != 0)
        {
            playbackPate = PlaybackRate;
        }
        Task.Run(() =>
        {
            try
            {
                gifImage = Image.FromFile(gifPath);
                frameDimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
                frameCount = gifImage.GetFrameCount(frameDimension);
                for (int i = 0; i < frameCount; i++)
                {
                    foreach (PropertyItem prop in gifImage.PropertyItems)
                    {
                        if (prop.Id == 0x5100) // 帧延迟标识符
                        {
                            // 解析4字节数据（小端序）
                            int delay = BitConverter.ToInt32(prop.Value, i * 4) * 10;
                            int delay1 = (int)(delay / playbackPate);
                            if (delay1 < 1)
                            {
                                delay1 = 1;
                            }
                            delays.Add(delay1); // 单位：毫秒（1/100秒 * 10 = 毫秒）
                            break;
                        }
                    }
                }
                IsLoaded = true;
            }
            catch (Exception)
            {
            }
        });
    }

    bool IsVisible = false;
    private void GifImageControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IsVisible = (bool)e.NewValue;
        if (IsVisible)
        {
            Task.Factory.StartNew(() => RefershUiTask(), TaskCreationOptions.LongRunning);
        }
    }

    private async Task RefershUiTask()
    {
        Stopwatch stopwatch = new Stopwatch();
        while (IsVisible)
        {
            try
            {
                if (IsLoaded && IsSizeChanged && gifImage != null)
                {
                    for (int i = 0; i < frameCount; i++)
                    {
                        if (IsVisible)
                        {
                            if (IsLoaded && IsSizeChanged)
                            {
                                stopwatch.Start();
                                gifImage.SelectActiveFrame(frameDimension, i);
                                Bitmap frame = new Bitmap(gifImage);
                                DrawShapes(frame);
                                frame?.Dispose();
                                frame = null;
                                stopwatch.Stop();
                                int delay = delays[i] - (int)stopwatch.ElapsedMilliseconds;
                                if (delay >= 0)
                                {
                                    await Task.Delay(delay);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                await Task.Delay(1000);
            }
        }
    }


    Bitmap bitmap = null;
    Graphics g = null;
    public void DrawShapes(Bitmap drawBitmap)
    {
        try
        {
            if (width <= 0 || height <= 0)
            {
                return;
            }
            if (bitmap == null || _bitmap == null || g == null || bitmap.Width != width || bitmap.Height != height)
            {
                Dispatcher.Invoke(()=>
                {
                    _bitmap = new WriteableBitmap(width, height, 0, 0, System.Windows.Media.PixelFormats.Bgra32, null);
                    cameraImg.Source = _bitmap;
                    // 初始化为透明背景
                    _bitmap.Lock();
                    unsafe
                    {
                        byte* pPixels = (byte*)_bitmap.BackBuffer;
                        for (int i = 0; i < width * height * 4; i += 4)
                        {
                            pPixels[i + 3] = 0; // Alpha 通道设为 0（完全透明）
                        }
                    }
                    _bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                    _bitmap.Unlock();
                });
                bitmap?.Dispose();
                bitmap = null;
                bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                g?.Dispose();
                g = null;
                g = Graphics.FromImage(bitmap);
            }
            g.Clear(System.Drawing.Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(drawBitmap, 0, 0, width, height);
            Dispatcher.Invoke(() =>
            {
                try
                {
                    _bitmap.Lock();
                    if (IsSizeChanged)
                    {
                        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        _bitmap.WritePixels(new Int32Rect(0, 0, width, height), bitmapData.Scan0, width * height * 4, width * 4);
                        bitmap.UnlockBits(bitmapData);
                    }
                    _bitmap.Unlock();
                }
                catch (Exception)
                {
                }
            });
            drawBitmap?.Dispose();
            drawBitmap = null;
        }
        catch (Exception)
        {
        }
    }
}