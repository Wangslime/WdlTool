using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfControlLibrary1
{
    /// <summary>
    /// MianUiControl.xaml 的交互逻辑
    /// </summary>
    public partial class MianUiControl : UserControl
    {
        public CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// 对内刷新存储比较
        /// </summary>
        private ControlUiData CurControlUiData = new ControlUiData();

        /// <summary>
        /// 对外提供赋值
        /// </summary>
        public ControlUiData ControlUiData = new ControlUiData();
        private bool IsRefersh = true;


        #region 动子参数
        private static float ActuatorStartPos = 1000;
        private static float ActuatorEndPos = 10000;
        private static float ActuatorControlStart = 0;
        private static float ActuatorControlEbd = 566;

        private static float ActuatorRatio = (ActuatorControlEbd- ActuatorControlStart) / (ActuatorEndPos - ActuatorStartPos);
        private static float ActuatorDiff = ActuatorStartPos * ActuatorRatio;

        private double ActuatorMoveY = 22;
        #endregion
        private double PrintingMoveY = 10;
        private double LiftingFeedArmMoveX = 200;


        TimeSpan duration = TimeSpan.FromSeconds(0.3);

        List<Image> NgImageList = new List<Image>();
        public MianUiControl()
        {
            InitializeComponent();

            NgImageList.Add(halfSliceIn1Ng);
            NgImageList.Add(halfSliceIn2Ng);
            NgImageList.Add(halfSliceIn3Ng);
            NgImageList.Add(halfSliceIn4Ng);

            Task.Factory.StartNew(async () => await RefershUiTask(), TaskCreationOptions.LongRunning);
        }

        public void RefershUi()
        {
            ControlUiData ??= new ControlUiData();
            CurControlUiData ??= new ControlUiData();
            if (!ControlUiData.Equals(CurControlUiData))
            {
                IsRefersh = true;
            }
        }

        private async Task RefershUiTask()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (IsRefersh)
                    {
                        IsRefersh = false;

                        #region 16块半片的显示
                        CurControlUiData.infeedBelt1 = await RefershVisibility(CurControlUiData.infeedBelt1, ControlUiData.infeedBelt1, halfSliceIn1);
                        CurControlUiData.infeedBelt2 = await RefershVisibility(CurControlUiData.infeedBelt2, ControlUiData.infeedBelt2, halfSliceIn2);
                        CurControlUiData.infeedBelt3 = await RefershVisibility(CurControlUiData.infeedBelt3, ControlUiData.infeedBelt3, halfSliceIn3);
                        CurControlUiData.infeedBelt4 = await RefershVisibility(CurControlUiData.infeedBelt4, ControlUiData.infeedBelt4, halfSliceIn4);
                        CurControlUiData.infeedBelt5 = await RefershVisibility(CurControlUiData.infeedBelt5, ControlUiData.infeedBelt5, halfSliceIn5);
                        CurControlUiData.infeedBelt6 = await RefershVisibility(CurControlUiData.infeedBelt6, ControlUiData.infeedBelt6, halfSliceIn6);
                        CurControlUiData.infeedBelt7 = await RefershVisibility(CurControlUiData.infeedBelt7, ControlUiData.infeedBelt7, halfSliceIn7);
                        CurControlUiData.infeedBelt8 = await RefershVisibility(CurControlUiData.infeedBelt8, ControlUiData.infeedBelt8, halfSliceIn8);

                        CurControlUiData.outfeedBelt1 = await RefershVisibility(CurControlUiData.outfeedBelt1, ControlUiData.outfeedBelt1, halfSliceOut1);
                        CurControlUiData.outfeedBelt2 = await RefershVisibility(CurControlUiData.outfeedBelt2, ControlUiData.outfeedBelt2, halfSliceOut2);
                        CurControlUiData.outfeedBelt3 = await RefershVisibility(CurControlUiData.outfeedBelt3, ControlUiData.outfeedBelt3, halfSliceOut3);
                        CurControlUiData.outfeedBelt4 = await RefershVisibility(CurControlUiData.outfeedBelt4, ControlUiData.outfeedBelt4, halfSliceOut4);
                        CurControlUiData.outfeedBelt5 = await RefershVisibility(CurControlUiData.outfeedBelt5, ControlUiData.outfeedBelt5, halfSliceOut5);
                        CurControlUiData.outfeedBelt6 = await RefershVisibility(CurControlUiData.outfeedBelt6, ControlUiData.outfeedBelt6, halfSliceOut6);
                        CurControlUiData.outfeedBelt7 = await RefershVisibility(CurControlUiData.outfeedBelt7, ControlUiData.outfeedBelt7, halfSliceOut7);
                        CurControlUiData.outfeedBelt8 = await RefershVisibility(CurControlUiData.outfeedBelt8, ControlUiData.outfeedBelt8, halfSliceOut8);
                        #endregion

                        #region 抬料臂
                        CurControlUiData.liftingFeedArm1.existFeed = await RefershVisibility(CurControlUiData.liftingFeedArm1.existFeed, ControlUiData.liftingFeedArm1.existFeed, liftingFeedArm1halfSlice);
                        if (CurControlUiData.liftingFeedArm1.posLeft != ControlUiData.liftingFeedArm1.posLeft)
                        {
                            await UiInvock(() =>
                            {
                                DoubleAnimation animation = new DoubleAnimation();
                                animation.Duration = duration;
                                TranslateTransform? translateTransform = liftingFeedArm1.RenderTransform as TranslateTransform;
                                if (ControlUiData.liftingFeedArm1.posLeft)
                                {
                                    animation.To = 0;
                                }
                                else
                                {
                                    animation.To = LiftingFeedArmMoveX;
                                }
                                if (translateTransform != null)
                                {
                                    translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
                                }
                            });
                            CurControlUiData.liftingFeedArm1.posLeft = ControlUiData.liftingFeedArm1.posLeft;
                        }

                        CurControlUiData.liftingFeedArm2.existFeed = await RefershVisibility(CurControlUiData.liftingFeedArm2.existFeed, ControlUiData.liftingFeedArm2.existFeed, liftingFeedArm2halfSlice);
                        if (CurControlUiData.liftingFeedArm2.posLeft != ControlUiData.liftingFeedArm2.posLeft)
                        {
                            await UiInvock(() =>
                            {
                                DoubleAnimation animation = new DoubleAnimation();
                                animation.Duration = duration;
                                TranslateTransform? translateTransform = liftingFeedArm2.RenderTransform as TranslateTransform;
                                if (ControlUiData.liftingFeedArm2.posLeft)
                                {
                                    animation.To = 0;
                                }
                                else
                                {
                                    animation.To = LiftingFeedArmMoveX;
                                }
                                if (translateTransform != null)
                                {
                                    translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
                                }
                            });
                            CurControlUiData.liftingFeedArm2.posLeft = ControlUiData.liftingFeedArm2.posLeft;
                        }
                        #endregion

                        #region 4动子

                        CurControlUiData.trayActuator1.jackFeed = await RefershActuatorY(CurControlUiData.trayActuator1.jackFeed, ControlUiData.trayActuator1.jackFeed, trayActuator1, actuator1Slice);
                        CurControlUiData.trayActuator1.xpos = await RefershActuatorX(CurControlUiData.trayActuator1.xpos, ControlUiData.trayActuator1.xpos, actuator1);

                        CurControlUiData.trayActuator2.jackFeed = await RefershActuatorY(CurControlUiData.trayActuator2.jackFeed, ControlUiData.trayActuator2.jackFeed, trayActuator2, actuator2Slice);
                        CurControlUiData.trayActuator2.xpos = await RefershActuatorX(CurControlUiData.trayActuator2.xpos, ControlUiData.trayActuator2.xpos, actuator2);

                        CurControlUiData.trayActuator3.jackFeed = await RefershActuatorY(CurControlUiData.trayActuator3.jackFeed, ControlUiData.trayActuator3.jackFeed, trayActuator3, actuator3Slice);
                        CurControlUiData.trayActuator3.xpos = await RefershActuatorX(CurControlUiData.trayActuator3.xpos, ControlUiData.trayActuator3.xpos, actuator3);

                        CurControlUiData.trayActuator4.jackFeed = await RefershActuatorY(CurControlUiData.trayActuator4.jackFeed, ControlUiData.trayActuator4.jackFeed, trayActuator4, actuator4Slice);
                        CurControlUiData.trayActuator4.xpos = await RefershActuatorX(CurControlUiData.trayActuator4.xpos, ControlUiData.trayActuator4.xpos, actuator4);

                        #endregion

                        #region 印刷头
                        if (CurControlUiData.printing1Working != ControlUiData.printing1Working)
                        {
                            await UiInvock(() =>
                            {
                                DoubleAnimation animation = new DoubleAnimation();
                                animation.Duration = duration;
                                TranslateTransform? translateTransform = printing1.RenderTransform as TranslateTransform;
                                if (ControlUiData.printing1Working)
                                {
                                    animation.To = PrintingMoveY;
                                }
                                else
                                {
                                    animation.To = 0;
                                }
                                if (translateTransform != null)
                                {
                                    translateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
                                }
                            });
                            CurControlUiData.printing1Working = ControlUiData.printing1Working;
                        }

                        if (CurControlUiData.printing2Working != ControlUiData.printing2Working)
                        {
                            await UiInvock(() =>
                            {
                                DoubleAnimation animation = new DoubleAnimation();
                                animation.Duration = duration;
                                TranslateTransform? translateTransform = printing2.RenderTransform as TranslateTransform;
                                if (ControlUiData.printing2Working)
                                {
                                    animation.To = PrintingMoveY;
                                }
                                else
                                {
                                    animation.To = 0;
                                }
                                if (translateTransform != null)
                                {
                                    translateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
                                }
                            });
                            CurControlUiData.printing2Working = ControlUiData.printing2Working;
                        }
                        #endregion

                        #region Ng半片显示
                        if (CurControlUiData.NgHalfSliceNum != ControlUiData.NgHalfSliceNum)
                        {
                            await UiInvock(()=>
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (ControlUiData.NgHalfSliceNum > i)
                                    {
                                        if (NgImageList[i].Visibility != Visibility.Visible)
                                        {
                                            NgImageList[i].Visibility = Visibility.Visible;
                                        }
                                    }
                                    else
                                    {
                                        if (NgImageList[i].Visibility != Visibility.Hidden)
                                        {
                                            NgImageList[i].Visibility = Visibility.Hidden;
                                        }
                                    }
                                }
                            });
                            CurControlUiData.NgHalfSliceNum = ControlUiData.NgHalfSliceNum;
                        }
                        #endregion

                        #region 相机
                        if (CurControlUiData.cameraLocalization1 != ControlUiData.cameraLocalization1)
                        {
                            camera1.cameraStatus = ControlUiData.cameraLocalization1;
                            CurControlUiData.cameraLocalization1 = ControlUiData.cameraLocalization1;
                        }
                        if (CurControlUiData.cameraLocalization2 != ControlUiData.cameraLocalization2)
                        {
                            camera2.cameraStatus = ControlUiData.cameraLocalization2;
                            CurControlUiData.cameraLocalization2 = ControlUiData.cameraLocalization2;
                        }
                        if (CurControlUiData.cameraDetect1 != ControlUiData.cameraDetect1)
                        {
                            camera3.cameraStatus = ControlUiData.cameraDetect1;
                            CurControlUiData.cameraDetect1 = ControlUiData.cameraDetect1;
                        }
                        if (CurControlUiData.cameraDetect2 != ControlUiData.cameraDetect2)
                        {
                            camera4.cameraStatus = ControlUiData.cameraDetect2;
                            CurControlUiData.cameraDetect2 = ControlUiData.cameraDetect2;
                        }
                        if (CurControlUiData.cameraAoiOutfeed != ControlUiData.cameraAoiOutfeed)
                        {
                            camera5.cameraStatus = ControlUiData.cameraAoiOutfeed;
                            CurControlUiData.cameraAoiOutfeed = ControlUiData.cameraAoiOutfeed;
                        }
                        #endregion
                    }
                    await Task.Delay(30);
                }
                catch (Exception ex)
                {
                    await Task.Delay(1000);
                }
            }
        }


        private async Task<bool> RefershVisibility(bool target, bool source, FrameworkElement framework)
        {
            if (target != source)
            {
                await UiInvock(() =>
                {
                    if (source)
                    {
                        framework.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        framework.Visibility = Visibility.Hidden;
                    }

                });
                target = source;
            }
            return source;
        }


        public async Task<bool> RefershActuatorY(bool target, bool source, FrameworkElement trayActuator, FrameworkElement actuatorSlice)
        {
            if (target != source)
            {
                await UiInvock(() =>
                {
                    DoubleAnimation animation = new DoubleAnimation();
                    animation.Duration = duration;
                    TranslateTransform? translateTransform = trayActuator.RenderTransform as TranslateTransform;
                    if (source)
                    {
                        actuatorSlice.Visibility = Visibility.Visible;
                        animation.To = 0;
                    }
                    else
                    {
                        actuatorSlice.Visibility = Visibility.Hidden;
                        animation.To = ActuatorMoveY;
                    }
                    translateTransform?.BeginAnimation(TranslateTransform.YProperty, animation);
                });
                target = source;
            }
            return source;
        }

        public async Task<double> RefershActuatorX(double target, double source, FrameworkElement actuator)
        {
            if (target != source)
            {
                double xpos = source;
                double targetX = xpos * ActuatorRatio - ActuatorDiff;
                await UiInvock(() =>
                {
                    DoubleAnimation animation = new DoubleAnimation();
                    animation.To = targetX;
                    animation.Duration = duration;
                    TranslateTransform? translateTransform = actuator.RenderTransform as TranslateTransform;
                    translateTransform?.BeginAnimation(TranslateTransform.XProperty, animation);
                });
                target = source;
            }
            return source;
        }


        private async Task UiInvock(Action func)
        {
            await this.Dispatcher.BeginInvoke(func);
        }
    }
}
