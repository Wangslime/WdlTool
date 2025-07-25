﻿using System.Windows.Controls;

namespace DRsoft.Runtime.Core.XcMarkCard
{
    public class DrMarkAdaptor
    {
        public List<Grid>? PanelList { get; set; } = new List<Grid>();
        public event Action<int, int>? MarkEndStatus;
        public event Action<int, string>? MarkAlarmStatus;
        private Dictionary<int, DrMarkSinglePlugin> markCards = new Dictionary<int, DrMarkSinglePlugin>();
        private List<DrMarkSinglePlugin> ListIMarkCard = new List<DrMarkSinglePlugin>();
        private readonly bool simulate = false;
        private readonly bool isSingle = true;
        private bool isPause = false;

        public bool EnableIMarkAop = false;


        public bool InitiaPanel(int CardNo)
        {
            PanelList.Add(new Grid());
            bool ret = true;
            DrMarkSinglePlugin drMarkPlugin = new DrMarkSinglePlugin();
            drMarkPlugin.CardNo = CardNo;
            if (simulate)
            {
                markCards.Add(CardNo, drMarkPlugin);
                if (isSingle)
                {
                    markCards.Add(CardNo + 1, drMarkPlugin);
                }
                ListIMarkCard.Add(drMarkPlugin);
                return ret;
            }
            ret = drMarkPlugin.InitiaPanel(PanelList[CardNo - 1], CardNo) && ret;
            drMarkPlugin.MarkEndStatus += DrMarkPlugin_MarkEndStatus;
            drMarkPlugin.MarkAlarmStatus += DrMarkPlugin_MarkAlarmStatus;
            markCards.Add(CardNo, drMarkPlugin);
            if (isSingle)
            {
                markCards.Add(CardNo + 1, drMarkPlugin);
            }
            ListIMarkCard.Add(drMarkPlugin);
            return ret;
        }

        public bool InitiaPanel(System.Windows.Controls.Panel panels, int CardNo)
        {
            bool ret = true;
            DrMarkSinglePlugin drMarkPlugin = new DrMarkSinglePlugin();
            drMarkPlugin.CardNo = CardNo;
            if (simulate)
            {
                markCards.Add(CardNo, drMarkPlugin);
                if (isSingle)
                {
                    markCards.Add(CardNo + 1, drMarkPlugin);
                }
                ListIMarkCard.Add(drMarkPlugin);
                return ret;
            }
            ret = drMarkPlugin.InitiaPanel(panels, CardNo) && ret;
            drMarkPlugin.MarkEndStatus += DrMarkPlugin_MarkEndStatus;
            drMarkPlugin.MarkAlarmStatus += DrMarkPlugin_MarkAlarmStatus;
            markCards.Add(CardNo, drMarkPlugin);
            if (isSingle)
            {
                markCards.Add(CardNo + 1, drMarkPlugin);
            }
            ListIMarkCard.Add(drMarkPlugin);
            return ret;
        }

        public bool InitialExt()
        {
            //ThreadExitis("DRMark");
            if (simulate) return true;
            bool ret = true;
            if (isSingle)
            {
                ret = ListIMarkCard[0].InitialExt(1);
            }
            else
            {
                foreach (var item in markCards)
                {
                    ret = item.Value.InitialExt(item.Key) || ret;
                }
            }
            return ret;
        }

        public void ThreadExitis(string threadName)
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (var process in processList)
            {
                if (process.ProcessName.ToLower() == threadName.ToLower())
                {
                    process.Kill(); //结束进程 
                }
            }
        }

        public void ThreadExitis()
        {
            System.Diagnostics.Process[] processList1 = System.Diagnostics.Process.GetProcessesByName("EZDrawPlatform MFC Application");
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcessesByName("DRMark");
            if (processList.Any())
            {
                foreach (var process in processList)
                {
                    process.Kill(); //结束进程 
                }
            }
            processList = null;
            processList = System.Diagnostics.Process.GetProcessesByName("DRMark");
            if (processList.Any())
            {
                foreach (var process in processList)
                {
                    process.Kill(); //结束进程 
                }
            }
        }

        /// <summary>
        /// 清空打标卡相关列表
        /// </summary>
        public void ReInitialClean()
        {
            markCards?.Clear();
            ListIMarkCard?.Clear();
            PanelList?.Clear();
        }

        public void Dispose()
        {
            if (simulate) return;
            if (isSingle)
            {
                ListIMarkCard[0].MarkClosing();
            }
            else
            {
                foreach (var item in ListIMarkCard)
                {
                    item.MarkClosing();
                }
            }
        }

        private void DrMarkPlugin_MarkEndStatus(int cardNo, int markEnd)
        {
            MarkEndStatus?.Invoke(cardNo, markEnd);
        }
        private void DrMarkPlugin_MarkAlarmStatus(int cardNo, string alarmStatus)
        {
            MarkAlarmStatus?.Invoke(cardNo, alarmStatus);
        }

        #region 单个发送

        /// <summary>
        /// 模拟 MarkingMate开机，以及信号初始化。         
        /// 在OCX AP 初始过程中，呼叫本函式，将库做初始化动作及产生新的数据库。 是MMMark_1.ocx中，第一个要呼叫的函数，之后才能正常呼叫MMMark_1.ocx内的其他函数。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns></returns>
        public bool InitialExt(int cardNo)
        {
            if (simulate) return true; if (!Verification(cardNo)) return false;
            return markCards[cardNo].InitialExt(cardNo);
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        public void MarkingFinish(int cardNo)
        {
            if (simulate) return; if (!Verification(cardNo)) return;
            markCards[cardNo].MarkingFinish();
        }

        /// <summary>
        /// 读取ezm文件到目前的数据库
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="fileName"></param>
        /// <returns>
        ///一、MMMark.ocx
        ///0：成功。
        ///1：失败，当前无数据库、输入的文件路径名称有误。
        ///2：失败，重置数据库错误。
        ///3：失败，加载文件错误。
        ///4：失败，回复数据库错误。
        ///5：失败，非阻断式雕刻中或是像素数据库闭锁中（于 V2.7A-34.22、V2.7D-2.84以后版本）。
        ///6：失败，激光仍在雕刻或预览状态中（于 V2.7A-34.27、V2.7D-2.89以后版本），或是正在自动化流程当中 （StartAutomation ） （于 V2.7A-35.14、V2.7D-4.19以后版本）
        /// 
        ///二、MMMark_1.ocx
        ///0 ： 成功，非零值表失败。
        ///10 ： 失败，正在自动化流程当中 （StartAutomation ） （于 V2.7Dx64-2.43以后版本）
        /// 
        ///三、MM3DMark.ocx
        ///0 ： 成功，非零值表失败。
        /// </returns>
        public int LoadFile(int cardNo, string fileName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].LoadFile(fileName);
        }

        /// <summary>
        /// 取得目前 MarkLib以及使用的软件版本
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="pstrVersion"></param>
        /// <returns>
        ///1、单系统：
        ///仅会回传0。
        ///
        ///2、多系统：
        ///0：成功。
        ///1：执行序执行失败或无版本信息。
        ///
        ///3、MM3D：
        ///仅会回传0。
        /// </returns>
        public int GetVersion(int cardNo, ref string pstrVersion)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetVersion(ref pstrVersion);
        }

        /// <summary>
        /// 可移动目标对象
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="objectName">
        ///目标对象的名称，定义如下。
        ///若为图层或组名（该图层或组中的全部对象）
        ///select（已选中的所有对象）、
        ///对象名称（所有相同名称对象）
        /// </param>
        /// <param name="dX">
        /// （绝对模式） 目标位置 X坐标 [公厘]。
        ///（相对模式） X偏移量[公厘]。
        /// </param>
        /// <param name="dY">
        /// （绝对模式） 目标位置 Y坐标 [公厘]。
        ///（相对模式） Y偏移量[公厘]。
        /// </param>
        /// <param name="lRel">
        /// 设定移动模式，定义如下。
        ///0：绝对模式，1：相对模式。
        ///注意事项：如果lRel = 0，请先使用 SetReferencePoint 设定适当的参考点再移动该物件。
        /// </param>
        /// <returns>
        /// 0：成功，非零值则表失败。
        ///10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）
        /// </returns>
        public int MoveObject(int cardNo, string objectName, double dX, double dY, int lRel)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].MoveObject(objectName, dX, dY, lRel);
        }

        /// <summary>
        /// 旋转目标对象
        /// 注意事项 ：请先使用 SetReferencePoint（）设定适当的参考点再旋转该物件。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="objectName">
        ///目标对象的名称，定义如下。
        ///若为图层或组名（该图层或组中的全部对象）
        ///select（已选中的所有对象）、
        ///对象名称（所有相同名称对象）
        /// </param>
        /// <param name="dAngle">
        /// （绝对模式） 旋转角度 [度]。
        ///（相对模式） 旋转角度差量[度]。
        ///如角度设定为正值，则对象往逆时针方向做旋转，角度设定为负值，对象往顺时针方向旋转
        /// </param>
        /// <param name="lRel">
        ///设定移动模式，定义如下。
        ///0：绝对模式，1：相对模式。
        /// </param>
        /// <returns>
        /// 一、MMMark.ocx、MMMark_1.ocx
        ///0：成功，非零值则表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DMark.ocx
        ///0：成功，非零值则表失败。
        /// </returns>
        public int RotateObject(int cardNo, string objectName, double dAngle, int lRel)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].RotateObject(objectName, dAngle, lRel);
        }

        /// <summary>
        /// 执行雕刻时，整体图面会依据所设之数值缩放、位移与旋转，但不会修改到所有对象资料
        /// 请注意，在没有重新设定之前，数据库的位移及旋转角度值不变。
        /// 操作做顺序： 缩放 -->旋转 -->平移
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="dXOffset">X轴偏移量 [公厘]</param>
        /// <param name="dYOffset">Y轴偏移量 [公厘]</param>
        /// <param name="dRotateCenterX">旋转中心 X坐标 [公厘]</param>
        /// <param name="dRotateCenterY">旋转中心 Y坐标 [公厘]</param>
        /// <param name="dAngle">旋转角度 [度]</param>
        /// <param name="dScaleCenterX">缩放中心 X坐标 [公厘]</param>
        /// <param name="dScaleCenterY">缩放中心 Y坐标 [公厘]</param>
        /// <param name="dScaleX">X轴缩放倍率</param>
        /// <param name="dScaleY">Y轴缩放倍率</param>
        /// <example>
        /// </example>
        /// <returns>0：成功，非零值则表失败</returns>
        public int SetMatrixExt(int cardNo, double dXOffset, double dYOffset, double dRotateCenterX, double dRotateCenterY, double dAngle, double dScaleCenterX, double dScaleCenterY, double dScaleX, double dScaleY)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetMatrixExt(dXOffset, dYOffset, dRotateCenterX, dRotateCenterY, dAngle, dScaleCenterX, dScaleCenterY, dScaleX, dScaleY);
        }

        /// <summary>
        /// 执行雕刻时，整体图面会依据所设之角度值旋转及位移，但不会修改到所有对象资料
        /// 在没有重新设定之前，数据库的位移及旋转角度值不变
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="dXOffset">X轴的平移量 [公厘]</param>
        /// <param name="dYOffset">Y轴的平移量 [公厘]</param>
        /// <param name="dAngle">旋转角度 [度]</param>
        /// <returns>0：成功，非零值则表失败</returns>
        public int SetMatrix(int cardNo, double dXOffset, double dYOffset, double dAngle)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetMatrix(dXOffset, dYOffset, dAngle);
        }

        /// <summary>
        /// 执行雕刻功能
        /// 注意事项 ：
        /// lMode = 3、4、5时，呼叫 StopMarking（）中断雕刻或预览状态。
        ///只有在lMode = 4、5时，才会发送 MarkEnd事件。
        ///预览雕刻不支持分图。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iMode">
        /// 设定雕刻模式，定义如下：
        ///1： 阻断式雕刻 （无雕刻对话框） 。
        ///2： 单系统：阻断式雕刻 （有雕刻对话盒），
        ///    多系统：无定义，函数传回异常。
        ///3： 预览雕刻：非阻断式雕刻 （单系统：V2.7A-34.14，多系统：V2.7A-34.45、V2.7D-3.11以上）。 可使用 SetMarkSelect函式设置只预览选取对象）。
        ///4： 非阻断式雕刻（无雕刻对话框） 。
        ///5： 非阻断式雕刻且激光不出光。 （V2.7D-4.3以上）
        ///帮助：
        ///A.阻断式：当函数执行结束时代表雕刻结束。
        ///B.非阻断式：当函式执行结束时不代表雕刻结束。 （另称背景雕刻）
        /// </param>
        /// <returns>
        ///一、MMMark.ocx、MMMark_1.ocx
        ///0：成功。
        ///
        ///1：对应组件未初始化。
        ///
        ///2：阻断式雕刻 （无雕刻对话盒） Startmarking （1）错误。 可能原因：
        ///（1）已开启自动化流程
        ///（2）找不到保护锁
        ///（3）保护锁只有编辑模式
        ///（4）MarkingMate内部流程错误。
        ///
        ///3：阻断式雕刻 （有雕刻对话盒） Startmarking （2）错误。 可能原因：
        ///（1）找不到保护锁。
        ///（2）保护锁只有编辑模式。
        ///（3）MarkingMate内部流程错误。
        ///
        ///4：预览雕刻错误 Startmarking （3）。 可能原因：
        ///（1）找不到保护锁。
        ///（2）保护锁只有编辑模式。
        ///（3）MarkingMate内部流程错误。
        ///
        ///5：背景雕刻 Startmarking （4）错误。 可能原因：
        ///（1）保护锁只有编辑模式。
        ///（2）MarkingMate内部流程错误。
        ///（3）MarkingMate已启动自动化流程。
        ///
        ///6：正在雕刻中 / 正在预览 （正在预览需V2.7A-35.28、V2.7D 4.29、V2.7Dx64 2.17 以上）。 
        ///
        ///7、8：Startmarking （5）失败;
        ///
        ///9：板卡未连接。 （V2.7A-35.27、V2.7D 4.26、V2.7Dx64 2.15 以上）
        ///
        ///10：已启用自动化流程。 （V2.7Dx64 2.30 以上）
        ///
        ///13：失败，启用虚线检查机制功能，有对象使用虚线，C参数等于0。 （仅适用于V2.7 A-35.49、V2.7 D-4.40、V2.7 D_x64-2.38以上版本）
        ///
        ///14：失败，启用虚线检查机制功能，有对象使用虚线，起点偏位参数等于0。 （仅适用于V2.7 A-35.49、V2.7 D-4.40、V2.7 D_x64-2.38以上版本）
        ///
        ///二、MM3DMark.ocx
        ///0 ： 成功，非零值表失败。
        /// </returns>
        public int StartMarking(int cardNo, int iMode)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].StartMarking(iMode);
        }

        /// <summary>
        /// 可以现有的激光条件开启激光
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// 一、MMMark.ocx、MMMark_1.ocx
        ///0：成功，非零值则代表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DMark.ocx
        ///0：成功，非零值则代表失败。
        /// </returns>
        public int LaserOn(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].LaserOn();
        }

        /// <summary>
        /// 可以现有的激光条件关闭激光
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        ///一、MMMark.ocx、MMMark_1.ocx
        ///0：成功。
        ///1：失败，初始化失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DMark.ocx
        ///0：成功，非零值则代表失败。
        /// </returns>
        public int LaserOff(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].LaserOff();
        }

        /// <summary>
        /// 进入自动化流程，此函式仅适用于 V2.7A-25.4版本以上
        /// 请注意使用此函式，对象雕刻次数或是整体线段数不可过多，若过多可能会发生无法雕刻状况。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        ///0：成功。
        ///1：失败，MMMark 未initial; 
        ///2：失败，没有保护锁/保护锁为编辑模式; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///3：失败，已启动自动化流程; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///4：失败，板卡使用RS232通讯不支持自动化流程; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///5：失败，板卡不支持此功能; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///6：失败，图档有开启分图/图层切割/图层XY滑台+电脑视觉定位/进阶飞雕功能; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///7：失败，compile雕刻资料流程出错; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///8：失败，使用多选档模式，未有可以雕刻的数据库; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///9：失败，使用多选档模式，欲雕刻的数据库中有自动文字/自动化组件（运动、圆环、回原点） ; （V2.7A-35.19、V2.7D-4.23、V2.7Dx64-2.12以上）
        ///10：失败，板卡未连线; （V2.7A-35.25、V2.7D-4.25、V2.7Dx64-2.15以上） 
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///12：失败，正在执行RunMarkingMate无法使用。 （仅适用于V2.7 A-35.49、V2.7 D-4.40、V2.7 D_x64-2.36以上版本）
        ///13：失败，启用虚线检查机制功能，有对象使用虚线，C参数等于0。 （仅适用于V2.7 A-35.49、V2.7 D-4.40、V2.7 D_x64-2.38以上版本）
        ///14：失败，启用虚线检查机制功能，有对象使用虚线，起点偏位参数等于0。 （仅适用于V2.7 A-35.49、V2.7 D-4.40、V2.7 D_x64-2.38以上版本）
        ///注： “板卡未连线”错误V2.7A-35.25、V2.7D-4.25、V2.7Dx64-2.15以前会回传“7”
        ///在退出自动化流程之前，呼叫其它雕刻函式，如、即时指令函数或进行任何编辑皆会产生不可预期的错误
        /// </returns>
        public int StartAutomation(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].StartAutomation();
        }

        /// <summary>
        /// 退出自动化流程，此函式仅适用于 V2.7 A-25.4以上
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        ///0：成功，非零值则表失败，
        ///1：MMMark OCX 未初始化或未启动自动化流程。
        ///11：失败，正在雕刻或预览中无法使用。 （仅适用于V2.7 A-35.31、V2.7 D-4.31、V2.7 D_x64-2.20以上版本）
        /// </returns>
        public int StopAutomation(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].StopAutomation();
        }

        /// <summary>
        /// 可设定指定图层是否可输出
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="objectName">指定对象名称</param>
        /// <param name="iCanOutput">设定是否可输出 1：可输出。0：不可输出。</param>
        /// <returns>
        ///一、MMEdit.ocx、MMEdit_1.ocx
        ///0：成功，非零值则代表失败。
        ///10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DEdit.ocx
        ///0：成功，非零值则代表失败。
        /// </returns>
        public int SetLayerOutput(int cardNo, string objectName, int iCanOutput)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetLayerOutput(objectName, iCanOutput);
        }

        /// <summary>
        /// 可设定输出点状态
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lPort">输出点代码，范围是1~16。</param>
        /// <param name="lOn">1：高电位，0：低电位</param>
        /// <returns>
        ///一、MMIO.ocx、MMIO_1.ocx
        ///0：成功，非零值则代表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DIO.ocx
        ///0：成功，非零值则代表失败。
        ///
        ///注、仅对控制卡的 Output接口脚位有作用，其余输出接口则无。
        ///各卡 Output接口如下：
        ///UMC4为 Motion（UMC4-B-Motion-IPG/SPI） 子卡上的 JF2接口。
        ///PMC2/PMC2e/PMC6 为控制卡的 JF8接口。
        /// </returns>
        public int SetOutput(int cardNo, int lPort, int lOn)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetOutput(lPort, lOn);
        }

        /// <summary>
        /// 可依据空跑位移的速度，空跑位移到起始位置
        /// 注意：由 SetJumpSpeed（）指定空跑位移的速度与 SetJumpDelay（）指定空跑位移的延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        ///0：成功，非零值则表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int JumpToStartPos(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].JumpToStartPos();
        }

        /// <summary>
        /// 可输出 JPG 文件
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="filePath">输出 jpg档案的全路径</param>
        /// <param name="iDpi">
        /// 0：使用设备DPI （默认）。
        ///>0：用户自定义DPI
        /// </param>
        /// <returns>
        ///一、MMMark.ocx、MMMark_1.ocx
        ///0：成功。
        ///1：失败，MMMark未初始化/保护锁不支持OCX/保护锁为编辑模式/创建图片失败/存储图片失败。
        ///2：失败，输出对象超出雕刻范围。 （仅适用于V2.7 A-35.48、V2.7 D-4.40、V2.7 D_x64-2.33以上版本）
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DMark.ocx
        ///0：成功，非零值则表失败。
        /// </returns>
        public int ExportJpg(int cardNo, string filePath, int iDpi)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ExportJpg(filePath, iDpi);
        }

        /// <summary>
        /// 系统会将 OCX AP 隐藏，弹出 MarkingMate/ MM3D操作窗口，此函式为「非阻断式」
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="filePath">指定档案的完整路径</param>
        /// <returns>
        ///0：成功。
        ///1：Window窗口控制权无法使用，失败。
        ///2：数据库异常，以下为其可能性：
        ///（a） 对应模块未初始化。
        ///（b） 使用了异常的数据库。
        ///3：参数异常，失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int RunMarkingMate(int cardNo, string filePath)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].RunMarkingMate(filePath);
        }

        /// <summary>
        /// 可即时修改所有图层是否输出、输出偏移值、输出角度。 （V2.7A-35.26、V2.7D-4.27、V2.7Dx64-2.16以上）
        /// 注意事项 ：
        ///1. 在MMEdit_1或MMEdit_x64_1初始化以后，呼叫一次 ModifyLayerTable 并且给参数都给零，当作功能初始化。
        ///2. 呼叫 StartAutomation 之前，要将图档的所有图层都设定为可输出。
        ///3. 第一个参数是图层总数。
        ///4. 参数阵列顺序就是文件中图层的顺序。
        ///5. 仅EMC6可使用，且EMC6的FW要在18以上。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lCnt">图层总数</param>
        /// <param name="iEnable">每个图层是否输出的阵列地址 0表相应图层不输出，1表相应图层要输出</param>
        /// <param name="dOffX">每个图层的偏移值X（绝对位置）[单位：公厘]</param>
        /// <param name="dOffY">每个图层的偏移值Y（绝对位置）[单位：公厘]</param>
        /// <param name="dRcx">每个图层的旋转中心坐标X[单位：公厘]的阵列地址</param>
        /// <param name="dRcy">每个图层的旋转中心坐标Y[单位：公厘]的阵列地址</param>
        /// <param name="dRotate">每个图层的旋转角度（绝对角度） [单位：度]的阵列地址</param>
        /// <returns>
        /// 0：成功。
        ///1： MM错误，没初始化。
        ///2： MM错误，由ML4传入的参数不合理。
        ///3： MM错误，driver不支持。
        ///4： MM错误，将参数下载给driver时， 因数据量过大导致内存不足。
        ///5： ML4错误，由OCX传入的参数不合理。
        ///6： ML4错误，要开share memory把参数传给MM时出错。
        ///10：MM错误，没收到来自ML4的参数。
        /// </returns>
        public int ModifyLayerTable(int cardNo, int lCnt, int[] iEnable, double[] dOffX, double[] dOffY, double[] dRcx, double[] dRcy, double[] dRotate)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ModifyLayerTable(lCnt, iEnable, dOffX, dOffY, dRcx, dRcy, dRotate);
        }

        /// <summary>
        /// 取得目前数据库的图层数
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        ///一、MMEdit.ocx、MMEdit_1.ocx、
        ///目前数据库的图层数。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DEdit.ocx
        ///目前数据库的图层数。
        /// </returns>
        public int GetLayerCount(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLayerCount();
        }

        /// <summary>
        /// 取得指定代码的图层名称
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iIndex">图层代码。 （由1开始。 )</param>
        /// <param name="pszName">传回图层名称 （字符串长度建议大于64字节。 )</param>
        /// <returns>
        /// 一、MMEdit.ocx、MMEdit_1.ocx、
        ///0：成功，非零值则表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        ///
        ///二、MM3DEdit.ocx
        ///0：成功，非零值则表失败。
        /// </returns>
        public int GetLayerName(int cardNo, int iIndex, out string pszName)
        {
            pszName = "";
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLayerName(iIndex,out pszName);
        }

        /// <summary>
        /// 可指定镜头文件
        /// 补充：若要在「多卡」模式下变更镜头文件，请使用 <see cref="ChangeLensMulti"/>
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lpName">镜头文件名称。 （EX：镜头档名称 -123.cor，则参数只要下“123”即可）</param>
        /// <returns>
        /// 0：成功，
        ///1：失败，可能原因：
        ///（1） MMEdit.OCX，没初始化。
        ///（2）找不到指定的镜头文件（.cor）。
        ///2：失败，目前于自动化流程状态下，无法切换K值不同的镜头校正文件。
        ///11：失败，正在雕刻或预览中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int ChangeLens(int cardNo, string lpName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ChangeLens(lpName);
        }

        /// <summary>
        /// 可於多卡模式下指定鏡頭檔，此函式僅適用於V 2.7 A 33.52 以上
        /// 若要在「单卡」模式下变更镜头文件，請使用 OCX函式 <see cref="ChangeLens"/>
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iHead">
        /// 指定 GalvoHead代碼，定義如下。
        ///1：第一個 GalvoHead。
        ///2：第二個 GalvoHead。
        ///3：第三個 GalvoHead。
        ///4：第四個 GalvoHead。
        /// </param>
        /// <param name="lpName">多卡鏡頭檔名稱</param>
        /// <returns>0：成功，非零值代表失敗。</returns>
        public int ChangeLensMulti(int cardNo, int iHead, string lpName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ChangeLensMulti(iHead, lpName);
        }

        /// <summary>
        /// 可取得指定对象中心的X轴坐标
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">指定对象名称</param>
        /// <returns>
        /// 对象中心的 X轴坐标 [公厘]
        /// 11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public double GetCenterX(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetCenterX(strName);
        }

        /// <summary>
        /// 可取得指定对象中心的Y轴坐标
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">指定对象名称</param>
        /// <returns>
        /// 对象中心的 Y轴坐标 [公厘]
        /// 11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public double GetCenterY(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetCenterY(strName);
        }

        /// <summary>
        /// 可取消所有选取对象
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// 0：成功，非零值则表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int SelectClearObjects(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SelectClearObjects();
        }

        /// <summary>
        /// 可设定是否只雕刻选取对象
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iSelect">是否只雕刻选取对象 0：雕刻所有对象， 1：只雕刻选取对象
        /// 注：V2.7A-34.14以上支持设定预览。（0：预览所有对象; 1：只预览选中对象）
        /// </param>
        /// <returns>
        /// 0：成功，2：失败，设定非0/1的值（V2.7A-34.44、V2.7D-3.10以上）。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int SetMarkSelect(int cardNo, int iSelect)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetMarkSelect(iSelect);
        }

        /// <summary>
        /// 可选取特定组下的指定对象（此函数在 V2.7A-27.8以上才可正常使用）
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strParent">群组名称</param>
        /// <param name="strobj">指定对象名称</param>
        /// <returns>
        /// 0：成功，非零值则表失败。
        ///11：失败，正在雕刻、预览或在自动化流程中无法使用。 （仅适用于V2.7 A-35.30、V2.7 D-4.30、V2.7 D_x64-2.19以上版本）
        /// </returns>
        public int SelectAddObjectExt(int cardNo, string strParent, string strobj)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SelectAddObjectExt(strParent, strobj);
        }

        /// <summary>
        /// 可设定预览模式为预览外框还是预览全路径
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iMode">预览模式 0：预览外框，1：预览全路径</param>
        /// <returns>0：成功，非零值则表失败</returns>
        public int SetPreviewMode(int cardNo, int iMode)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetPreviewMode(iMode);
        }

        /// <summary>
        /// 可中断雕刻或预览状态
        /// 注意事项 ：执行 StartMarking（） 为预览模式或非阻断式雕刻模式下，才能使用函数
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>0：成功，非零值则代表失败</returns>
        public int StopMarking(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].StopMarking();
        }

        /// <summary>
        /// 可清除进阶热漂移四点拉伸数值 
        /// 此函式仅适用于 V2.7A-33.46、V2.7D-4.31、V2.7Dx64-2.21版本以上，不适用于2.7DS-5.0含以上版本
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iScanHead">扫描头编号，请固定设为1</param>
        /// <param name="iTable">平台编号，范围1~4</param>
        /// <returns>
        /// 0：成功，非零值则代表失败。
        ///-1：Driver 异常，代码3。
        ///-2：Driver 异常，代码2。 可能原因：未安装PMC2e、PMC6、EMC6控制卡。
        ///-3：lScanHead 参数错误。
        ///-4：lTable 参数错误。
        ///-5：Driver 不支持。 可能原因：选择错误驱动，此函式仅支持PMC6、EMC6，请于安装目录 C：Program Files （x86）MarkingMate 或 C：Program Files （x86）MM3D，开启「DM.exe」，选择对应控制卡。
        ///-6：文件无法写入
        /// </returns>
        public int ThermalDrift_ClearExt(int cardNo, int iScanHead, int iTable)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ThermalDrift_ClearExt(iScanHead, iTable);
        }

        /// <summary>
        /// 可设定进阶热漂移四点拉伸的数值
        /// 此函式仅适用于 V2.7A-33.46、V2.7D-4.31、V2.7Dx64-2.21版本以上，不适用于2.7DS-5.0含以上版本
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lScanHead">扫描头编号，请固定设为1</param>
        /// <param name="lTable">平台编号，范围1~4</param>
        /// <param name="dx1">指定区域的左下角 X（理论坐标）</param>
        /// <param name="dy1">指定区域的左下角 Y（理论坐标）</param>
        /// <param name="dx2">指定区域的右下角 X（理论坐标）</param>
        /// <param name="dy2">指定区域的右下角 Y（理论坐标）</param>
        /// <param name="dx3">指定区域的右上角 X（理论坐标）</param>
        /// <param name="dy3">指定区域的右上角 Y（理论坐标）</param>
        /// <param name="dx4">指定区域的左上角 X（理论坐标）</param>
        /// <param name="dy4">指定区域的左上角 Y（理论坐标）</param>
        /// <param name="dRx1">指定区域的左下角 X（实际坐标）</param>
        /// <param name="dRy1">指定区域的左下角 Y（实际坐标）</param>
        /// <param name="dRx2">指定区域的右下角 X（实际坐标）</param>
        /// <param name="dRy2">指定区域的右下角 Y（实际坐标）</param>
        /// <param name="dRx3">指定区域的右上角 X（实际坐标）</param>
        /// <param name="dRy3">指定区域的右上角 Y（实际坐标）</param>
        /// <param name="dRx4">指定区域的左上角 X（实际坐标）</param>
        /// <param name="dRy4">指定区域的左上角 Y（实际坐标）</param>
        /// <returns>
        /// 0：成功，非零值则代表失败。
        ///-1：Driver 异常，代码3。
        ///-2：Driver 异常，代码2。 可能原因：未安装PMC6、EMC6控制卡。
        ///-3：lScanHead 参数错误。
        ///-4：lTable 参数错误。
        ///-5：Driver 不支持。 可能原因：选择错误驱动，此函式仅支持PMC6、EMC6，请于安装目录 C：Program Files （x86）MarkingMate 或 C：Program Files （x86）MM3D，开启「DM.exe」，选择对应控制卡。
        ///-6：文件无法写入。
        /// </returns>
        public int ThermalDrift_SetStretchDataExt(int cardNo, int lScanHead, int lTable, double dx1, double dy1, double dx2, double dy2, double dx3, double dy3, double dx4, double dy4, double dRx1, double dRy1, double dRx2, double dRy2, double dRx3, double dRy3, double dRx4, double dRy4)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ThermalDrift_SetStretchDataExt(lScanHead, lTable, dx1, dy1, dx2, dy2, dx3, dy3, dx4, dy4, dRx1, dRy1, dRx2, dRy2, dRx3, dRy3, dRx4, dRy4);
        }

        /// <summary>
        /// 可选择进阶热漂移四点拉伸平台，使用进阶热漂移四点拉伸功能，打标前需呼叫此函式，以选择当前热漂移平台。
        ///此函式仅适用于 V2.7A-33.46、V2.7D-4.31、V2.7Dx64-2.21版本以上，不适用于2.7DS-5.0含以上版本，说明如下
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="iScanHead">扫描头编号，请固定设为1</param>
        /// <param name="iTable">平台编号，范围1~4</param>
        /// <returns>
        /// 0：成功，非零值则代表失败。
        ///-2：Driver 异常，代码1。 可能原因：未安装PMC6、EMC6控制卡。
        ///-3：lScanHead 参数错误。
        ///-4：lTable 参数错误。
        ///-5：Driver 不支持。 可能原因：选择错误驱动，此函式仅支持PMC6、EMC6，请于安装目录 C：\Program Files\ （x86）\MarkingMate 或 C：\Program Files （x86）\MM3D，开启「DM.exe」，选择对应控制卡。
        /// </returns>
        public int ThermalDrift_ChangeTableExt(int cardNo, int iScanHead, int iTable)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ThermalDrift_ChangeTableExt(iScanHead, iTable);
        }

        /// <summary>
        /// 可依据位移的速度，空跑位移到指定位置
        /// 注意：由 SetJumpSpeed（）指定空跑位移的速度与 SetJumpDelay（）指定空跑位移的延迟时间。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="dx">目标位置的 X轴坐标[毫米]</param>
        /// <param name="dy">目标位置的 Y轴坐标[毫米]</param>
        /// <returns>0：成功，非零值则代表失败</returns>
        public int JumpTo(int cardNo, double dx, double dy)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].JumpTo(dx, dy);
        }

        /// <summary>
        /// 可设定目标对象的激光功率
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称
        /// 若为
        ///（“”）空字符串（对当次默认值）、
        ///default（对存储为默认值）、
        ///图层或组名（该图层或组中的全部对象）
        ///select（已选中的所有对象）、
        ///对象名称（所有相同名称对象）
        /// </param>
        /// <param name="dPerc">激光功率[%]， 范围是0~100</param>
        /// <returns>
        /// 0：成功，非零值则表失败。
        ///10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）
        /// </returns>
        public int SetPower(int cardNo, string strName, double dPerc)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetPower(strName, dPerc);
        }

        /// <summary>
        /// 可设定目标对象的激光频率
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称
        /// 若为
        ///（“”）空字符串（对当次默认值）、
        ///default（对存储为默认值）、
        ///图层或组名（该图层或组中的全部对象）
        ///select（已选中的所有对象）、
        ///对象名称（所有相同名称对象）
        /// </param>
        /// <param name="dKHz">激光频率 [kHz]，范围是0以上</param>
        /// <returns>
        /// 0：成功。
        ///1：当 strName参数为（“”）空字符串，dKHz为（“”）空字符串或小于0或非使用的激光配置文件范围值，失败。
        ///2：当 strName参数为非（“”）空字串及非 default，dKHz为（“”）空字串或小于0或非使用的激光设定档范围值，失败。
        ///3：当 strName参数为 default，dKHz为（“”）空字串或小于0或非使用的激光设定文件范围值，失败。
        ///10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）
        /// </returns>
        public int SetFrequency(int cardNo, string strName, double dKHz)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetFrequency(strName, dKHz);
        }

        /// <summary>
        /// 设定激光器脉宽
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称
        /// 若为
        ///（“”）空字符串（对当次默认值）、
        ///default（对存储为默认值）、
        ///图层或组名（该图层或组中的全部对象）
        ///select（已选中的所有对象）、
        ///对象名称（所有相同名称对象）
        /// </param>
        /// <param name="dPulseWidth"></param>
        /// <returns></returns>
        public int SetPulseWidth(int cardNo, string strName, double dPulseWidth)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetPulseWidth(strName, dPulseWidth);
        }

        /// <summary>
        /// 可取得本次雕刻所花费的时间
        /// 须注意，此函式预设取得的时间为，软体功能中雕刻时间模式的「路径时间」，亦可于软体中切换为「实际时间」。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>本次雕刻所花费的时间 [毫秒]</returns>
        public int GetMarkTime(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetMarkTime();
        }

        /// <summary>
        /// 输入点状态
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lport">输入点代码，范围是1~16</param>
        /// <returns>输入点状态。 （1代表高电位，0代表低电位）</returns>
        public int GetInput(int cardNo, long lport)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetInput(lport);
        }

        /// <summary>
        /// 取得数个连续的输入点状态，此函式仅适用于 V2.7Dx64-2.40 版本以上
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lFirstPort">第一个输入点代码，代码编号方式可参考IO定义文件</param>
        /// <param name="lPortCnt">需要取得连续输入点的数量，输入范围为 1 ~ 31</param>
        /// <returns>连续的输入点状态，每一个bit表示一个点的状态，第一个bit为第一点状态，第二个bit为第二点状态，以此类推; 若为负值，表示错误</returns>
        public int GetInputExt(int cardNo, int lFirstPort, int lPortCnt)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetInputExt(lFirstPort, lPortCnt);
        }

        /// <summary>
        /// 可取得目标对象的雕刻速度
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>雕刻速度 [公厘/秒]</returns>
        public double GetSpeed(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetSpeed(strName);
        }

        /// <summary>
        /// 可取得目标对象的激光频率
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>激光频率 [kHz]</returns>
        public double GetFrequency(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetFrequency(strName);
        }

        /// <summary>
        /// 可取得目标对象的雷射功率
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>激光功率 [%]</returns>
        public double GetPower(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetPower(strName);
        }

        /// <summary>
        /// 可取得激光的脉宽
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>激光的脉宽 [微秒]</returns>
        public double GetPulseWidth(int cardNo, string strName)
        {
            return markCards[cardNo].GetPulseWidth(strName);
        }

        /// <summary>
        /// 可取得起始点延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>起始点延迟时间[微秒]</returns>
        public int GetLaserOnDelay(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLaserOnDelay(strName);
        }

        /// <summary>
        /// 可取得对象转角延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>转角延迟时间 [微秒]</returns>
        public int GetPolyDelay(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetPolyDelay(strName);
        }

        /// <summary>
        /// 可取得对象的终止点延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>终止点延迟时间 [微秒]</returns>
        public int GetLaserOffDelay(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLaserOffDelay(strName);
        }

        /// <summary>
        /// 取得雕刻延迟的延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>雕刻延迟的延迟时间 [微秒]</returns>
        public int GetMarkDelay(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetMarkDelay(strName);
        }

        /// <summary>
        /// 可取得空跑位移时的速度
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>空跑位移的速度 [公厘/秒]</returns>
        public double GetJumpSpeed(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetJumpSpeed(strName);
        }

        /// <summary>
        /// 可取得空跑位移时的延迟时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称。若为（“”）空字串（对当次预设值），则将取得指定对象的回传值</param>
        /// <returns>空跑位移的延迟时间 [微秒]</returns>
        public int GetJumpDelay(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetJumpDelay(strName);
        }

        /// <summary>
        /// 可进入雕刻状态 （模拟雕刻对话盒开启，以及功率爬升）
        /// 在开始打标之前，会执行提升电流或是I/O信号设定等相关动作
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// 0：成功。
        ///1：数据库被占用中或是 MMMark/ MM3DMark模块未初始化，失败。
        /// </returns>
        public int MarkStandBy(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].MarkStandBy();
        }

        /// <summary>
        /// 可启动热漂移四点拉伸功能，需要在进阶热漂移功能启用和结束时呼叫
        /// 此函式仅适用于 V2.7A-33.46、V2.7D-4.31、V2.7Dx64-2.21版本以上，不适用于2.7DS-5.0含以上版本
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lScanHead">扫描头编号，请固定设为1</param>
        /// <param name="lTable">平台编号，范围1~4</param>
        /// <param name="lEnable">1/0 （启动功能/关闭功能）</param>
        /// <returns>
        /// 0：成功，非零值则代表失败。
        ///-1：Driver异常，代码1。
        ///-2：Driver异常，代码2。
        ///-3：lScanHead 参数错误。
        ///-4：lTable 参数错误。
        ///-5：Driver 不支持
        /// </returns>
        public int ThermalDrift_EnableExt(int cardNo, int lScanHead, int lTable, int lEnable)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ThermalDrift_EnableExt(lScanHead, lTable, lEnable);
        }

        /// <summary>
        /// 可配置目标对象的激光内部频率
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">
        /// （“”）空字符串，对当次默认值。
        ///default，对存储成预设值。
        ///图层或组名，该图层或组内所有对象。
        ///select，已选取的所有对象。
        ///对象名称，所有相同名称对象。
        /// </param>
        /// <param name="lPassIndex">指定要调整哪一次加工的参数，范围是1~5</param>
        /// <param name="dKHz">激光内部频率 [kHz]，范围是0以上</param>
        /// <returns>
        /// 0..成功，非零值表失败。
        ///10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）
        /// </returns>
        public int SetIntFrequencyExt(int cardNo, string strName, int lPassIndex, double dKHz)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetIntFrequencyExt(strName, lPassIndex, dKHz);
        }

        /// <summary>
        /// 可清除目前的数据库中，所有的图形资料。 
        /// 注意：当必须清除此工作数据库中的所有像素时，呼叫此函式清除工作数据库内的像素
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>0：成功，非零值则表失败</returns>
        public int ResetFile(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].ResetFile();
        }

        /// <summary>
        /// 可重绘目前数据库
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>0：成功，非零值则表失败</returns>
        public int Redraw(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].Redraw();
        }

        /// <summary>
        /// 可下载校正表（由DRIVER直接读档并装入配置的校正表中）
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lHead">
        /// 指定下载至哪个头的 1：第一颗雕刻头。2：第二颗雕刻头。
        /// </param>
        /// <param name="lTb">下载至哪一个位置 1~9</param>
        /// <param name="strFilePath">校正档路径 （需为全路径）（*. COR)</param>
        /// <returns>
        /// 0：成功，
        /// 1：使用不支持的Driver，EX：Demo 或是 UMC4、PMC2。 （目前仅支持PMC2e、PMC6）
        /// </returns>
        public int LoadCorrectTable(int cardNo, int lHead, int lTb, string strFilePath)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].LoadCorrectTable(lHead, lTb, strFilePath);
        }

        /// <summary>
        /// 可设定镜头校正参数
        /// 此函式仅适用于 V2.7A-35.34、V2.7D-4.33、V2.7Dx64-2.24 版本以上，说明如下。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lpName">镜头文件名称</param>
        /// <param name="lParamType">
        /// 属性类型，定义如下：
        ///1： 原点偏位X （公厘）
        ///2： 原点偏位Y （公厘）
        ///3： 放缩比例X（%）
        ///4： 放缩比例Y（%）
        ///5： 旋转角度（度）
        ///6： 旋转中心X（公厘）
        ///7： 旋转中心Y（公厘）
        ///8： 桶型校正+X（公厘）
        ///9： 桶型校正+Y（公厘）
        ///10：桶形校正-X（公厘）
        ///11：桶形校正-Y（公厘）
        ///12：梯形校正X（公厘）
        ///13：梯形校正Y（公厘）
        ///14：平行四边形校正X（公厘）
        ///15： 平行四边形校正Y（公厘）
        /// </param>
        /// <param name="dParam">参数数值</param>
        /// <returns>
        /// 0：成功;
        ///1：失败，可能原因 MMMark 未初始化;
        ///2：失败，可能原因 找不到指定名称的镜头文件。
        ///3：失败，可能原因 设定数值错误。
        ///4：失败，可能原因 保护锁不支持。
        /// </returns>
        public int SetLensCorParameterByName(int cardNo, string lpName, int lParamType, double dParam)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetLensCorParameterByName(lpName, lParamType, dParam);
        }

        /// <summary>
        /// 可取得镜头校正参数
        /// 此函式仅适用于 V2.7A-35.34、V2.7D-4.33、V2.7Dx64-2.24 版本以上
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="lpName">镜头文件名称</param>
        /// <param name="lParamType">
        /// 1： 原点偏位X （公厘）
        ///2： 原点偏位Y （公厘）
        ///3： 放缩比例X（%）
        ///4： 放缩比例Y（%）
        ///5： 旋转角度（度）
        ///6： 旋转中心X（公厘）
        ///7： 旋转中心Y（公厘）
        ///8： 桶型校正+X（公厘）
        ///9： 桶型校正+Y（公厘）
        ///10：桶形校正-X（公厘）
        ///11：桶形校正-Y（公厘）
        ///12：梯形校正X（公厘）
        ///13：梯形校正Y（公厘）
        ///14：平行四边形校正X（公厘）
        ///15： 平行四边形校正Y（公厘）
        /// </param>
        /// <returns>依照属性类型回传参数</returns>
        public double GetLensCorParameterByName(int cardNo, string lpName, int lParamType)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLensCorParameterByName(lpName, lParamType);
        }

        /// <summary>
        /// 可确认激光是否仍在雕刻状态中
        /// 1、在执行 StartMarking（） 为非阻断式雕刻模式下才能使用本函数。
        ///2、此函式为透过程序状态，进而判断是否仍雕刻中，而非透过取得状态讯号判断。
        ///3、不适用于GridMarking。
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>1：目前正在雕刻; 0：目前没有在雕刻</returns>
        public int IsMarking(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].IsMarking();
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        public void MarkClosing(int cardNo)
        {
            if (simulate) return; if (!Verification(cardNo)) return;
            markCards[cardNo].MarkClosing();
        }

        /// <summary>
        /// 可设定目前的 GalvoHead代码（此函式在 V2.6以上才可正常使用）
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="num">
        /// 1：第一个 GalvoHead。
        /// 2：第二个 GalvoHead。
        /// 3：第三个 GalvoHead。
        /// 4：第四个 GalvoHead
        /// 其他：无定义。
        /// </param>
        /// <returns>0：成功，非零值则表失败。</returns>
        public int SetCurrentHead(int cardNo, int num)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetCurrentHead(num);
        }

        /// <summary>
        /// 可对像素数据库开锁
        /// 在雕刻模式下，当系统为闭锁状态，要进行像素编辑时，必须要对像素数据库开锁，此时才可以对像素或是雷雕参数进行修改
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// 0：成功;
        /// 1：对应组件未初始化 / 没有数据正在闭锁/ 数据开锁失败：
        /// 2：正在雕刻 / 正在预览（V2.7A-35.28、V2.7D 4.29、V2.7Dx64 2.17 以上）
        /// </returns>
        public int MarkData_UnLock(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].MarkData_UnLock();
        }

        /// <summary>
        /// 取得目前控制卡是否在自动化流程中
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// 0： 目前不在自动化流程中。
        /// 1： 目前在自动化流程中。
        /// </returns>
        public int IsAutomation(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].IsAutomation();
        }

        /// <summary>
        /// 可取得 控制卡名称及使用中的激光配置文件名称、驱动版号、控制卡固件版号、分位版号
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="pstrName">控制卡名称及使用中的激光配置文件名称</param>
        /// <param name="pStrVer">驱动版号、控制卡韧体版号、分位版号</param>
        /// <returns>0：成功，非零值则表失败</returns>
        public int GetDriverInfo(int cardNo, ref string pstrName, ref string pStrVer)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetDriverInfo(ref pstrName, ref pStrVer);
        }

        /// <summary>
        /// 可取得单头镜头校正档名称
        /// 此函式仅适用于 MM27A-35.39、MM2.7D-4.39、MM2.7D x64-2.27版本以上
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="pstrName">传回镜头校正档名称</param>
        /// <returns>0： 成功，非零值表示取得失败</returns>
        public int GetLensName(int cardNo, ref string pstrName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetLensName(ref pstrName);
        }

        /// <summary>
        /// 取得目前工作的预估加工时间
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <returns>
        /// >=0：加工时间 [秒]。
        /// 小于 0：失败
        /// </returns>
        public double GetEstimatedTotalTime(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetEstimatedTotalTime();
        }

        /// <summary>
        /// 设定是否启用填满虚线参数多组AB功能。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">
        /// 目标对象的名称，定义如下。
        /// 若是“select”，代表对已选取对象作设定。
        /// 若是图层的名称，代表对该图层所有对象作设定。
        /// 若是对象的名称，代表对相同名称的所有对象作设定
        ///   </param>
        /// <param name="enable">
        /// 设置对象是否启用填满虚线参数多组AB功能
        /// </param>
        /// <returns>
        ///0：成功。
        ///1：失败，可能原因 1. MMMark未初始化 2.保护锁/板卡不支持填满虚线功能 3.找不到此对象。
        ///2：失败，是否启用填满虚线参数多组AB功能非定义值。
        ///10：失败，目前在MarkData_Lock下无法使用
        /// </returns>
        public int SetFillDashLineParamABMultiGroupEnable(int cardNo, string strName, bool enable)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetFillDashLineParamABMultiGroupEnable(strName, enable);
        }

        /// <summary>
        /// 取得指定对象是否启用填满虚线参数多组AB功能。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称</param>
        /// <returns>true:开启 false：关闭</returns>
        public bool GetFillDashLineParamABMultiGroupEnable(int cardNo, string strName)
        {
            if (simulate) return false; if (!Verification(cardNo)) return false;
            return markCards[cardNo].GetFillDashLineParamABMultiGroupEnable(strName);
        }

        /// <summary>
        /// 设定填满虚线参数多组AB组数。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">
        /// 目标对象的名称，定义如下。
        /// 若是“select”，代表对已选取对象作设定。
        /// 若是图层的名称，代表对该图层所有对象作设定。
        /// 若是对象的名称，代表对相同名称的所有对象作设定。
        /// </param>
        /// <param name="groupCnt">填满虚线参数多组AB组数（范围： 1组-20组）。</param>
        /// <returns>
        /// 0：成功。
        ///1：失败，可能原因 1. MMMark未初始化 2.保护锁/板卡不支持填满虚线功能 3.找不到此对象。
        ///2：失败，多组AB组数非定义值。
        ///10：失败，目前在MarkData_Lock下无法使用
        /// </returns>
        public int SetFillDashLineParamAB_GroupCnt(int cardNo, string strName, int groupCnt)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetFillDashLineParamAB_GroupCnt(strName, groupCnt);
        }

        /// <summary>
        /// 取得填满虚线参数多组AB组数。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称</param>
        /// <returns>填满虚线参数多组AB组数</returns>
        public int GetFillDashLineParamAB_GroupCnt(int cardNo, string strName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetFillDashLineParamAB_GroupCnt(strName);
        }

        /// <summary>
        /// 设定填满虚线多组参数A。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">
        /// 目标对象的名称，定义如下。
        ///若是“select”，代表对已选取对象作设定。
        ///若是图层的名称，代表对该图层所有对象作设定。
        ///若是对象的名称，代表对相同名称的所有对象作设定。
        /// </param>
        /// <param name="group">指定组数（范围： 1组-20组）</param>
        /// <param name="paramA">填满虚线参数A [公厘]</param>
        /// <returns>
        /// 0：成功。
        ///1：失败，可能原因 1. MMMark未初始化 2.保护锁/板卡不支持填满虚线功能 3.找不到此对象 4.参数A小于参数B。
        ///2：失败，可能原因 1.指定组数非定义值 2.参数A 小于等于0。
        ///10：失败，目前在MarkData_Lock下无法使用
        /// </returns>
        public int SetFillDashLineParamA_Group(int cardNo, string strName, int group, double paramA)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetFillDashLineParamA_Group(strName, group, paramA);
        }

        /// <summary>
        /// 取得指定对象填满虚线多组参数A。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称</param>
        /// <param name="group">指定组数（范围： 1组-20组）</param>
        /// <returns>填满虚线参数A [公厘]</returns>
        public double GetFillDashLineParamA_Group(int cardNo, string strName, int group)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetFillDashLineParamA_Group(strName, group);
        }

        /// <summary>
        /// 设定填满虚线多组参数B。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">
        /// 目标对象的名称，定义如下。
        ///若是“select”，代表对已选取对象作设定。
        ///若是图层的名称，代表对该图层所有对象作设定。
        ///若是对象的名称，代表对相同名称的所有对象作设定。
        /// </param>
        /// <param name="group">指定组数（范围： 1组-20组）</param>
        /// <param name="paramB">填满虚线参数B [公厘]</param>
        /// <returns>
        ///0：成功。
        ///1：失败，可能原因 1. MMMark未初始化 2.保护锁/板卡不支持填满虚线功能 3.找不到此对象 4.参数B大于参数A。
        ///2：失败，可能原因 1.指定组数非定义值 2.参数B 小于等于0。
        ///10：失败，目前在MarkData_Lock下无法使用。
        /// </returns>
        public int SetFillDashLineParamB_Group(int cardNo, string strName, int group, double paramB)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetFillDashLineParamB_Group(strName, group, paramB);
        }

        /// <summary>
        /// 取得指定对象填满虚线多组参数B。 （V2.7Dx64-2.43.3以上） 
        /// 保护锁需开通「虚线功能」模块，且搭配 PMC6或 PMC2e方可使用
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="strName">目标对象的名称</param>
        /// <param name="group">指定组数（范围： 1组-20组）</param>
        /// <returns>填满虚线参数B [公厘]</returns>
        public double GetFillDashLineParamB_Group(int cardNo, string strName, int group)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].GetFillDashLineParamB_Group(strName, group);
        }

        /// <summary>
        /// 预览中间增加停顿间隔
        /// </summary>
        /// <param name="cardNo">打标卡序号</param>
        /// <param name="dSec">:兩次預覽間的延遲時間[秒]:</param>
        /// <returns>0:成功 1:失敗</returns>
        public int SetPreviewDelay(int cardNo, double dSec)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            return markCards[cardNo].SetPreviewDelay(dSec);
        }

        /// <summary>
        /// 控制卡是否连线
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool IsCardConnect(int cardNo)
        {
            if (simulate) return true; if (!Verification(cardNo)) return false;
            var res= markCards[cardNo].IsCardConnect();
            return res == 1;
        }

        /// <summary>
        /// 雷射重新连线
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>0:成功，非零值則代表失败</returns>
        public int LaserReconnect(int cardNo)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].LaserReconnect();
            return res;
        }

        /// <summary>
        /// 可新增一個图层
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="layerName">新图层的名称</param>
        /// <returns>0：成功，非零值則表失敗。10：失敗，目前在MarkData_Lock下無法使用。(僅適用於V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本)</returns>
        public int AddLayer(int cardNo, string layerName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].AddLayer(layerName);
            return res;
        }

        /// <summary>
        /// 可删除指定图层
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="layerName">指定图层的名称</param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int DeleteLayer(int cardNo, string layerName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].DeleteLayer(layerName);
            return res;
        }

        /// <summary>
        /// 可设定指定图层是否可编辑
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="strName">指定物件名稱</param>
        /// <param name="canEdit"></param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int SetLayerEdit(int cardNo, string strName, bool canEdit)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].SetLayerEdit(strName,canEdit);
            return res;
        }

        /// <summary>
        /// 可将目前的数据库储存至ezm档案
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="strFileName">ezm档案的全路径</param>
        /// <returns></returns>
        public int SaveFile(int cardNo, string strFileName)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].SaveFile(strFileName);
            return res;
        }

        /// <summary>
        /// 可将目标对象复制至指定组下
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="strOrg">目标对象的名称</param>
        /// <param name="strCopy">复制出的新对象名称</param>
        /// <param name="strGroup">指定组或图层的名称。（可配置为空字符串（“”），即复制到当前图层下）</param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int CopyObject(int cardNo, string strOrg, string strCopy, string strGroup)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].CopyObject(strOrg,  strCopy, strGroup);
            return res;
        }

        /// <summary>
        /// 將當前圖檔與作為模板的ezm檔做比對, 判斷是否內容一樣。
        /// </summary>
        /// <param name="lpTemplateFile">作為模板的ezm檔的絕對路徑</param>
        /// <returns>0不同, 1相同, -1失敗</returns>
        public int IsEqualFile(int cardNo, string lpTemplateFile)
        {
            if (simulate) return 0; if (!Verification(cardNo)) return -99;
            var res = markCards[cardNo].IsEqualFile(lpTemplateFile);
            return res;
        }
        #endregion


        private bool Verification(int cardNo = 0)
        {
            if (isPause) return false;
            if (cardNo == 0)
            {
                return markCards.Any();
            }
            else
            {
                if (markCards.ContainsKey(cardNo))
                {
                    //if (isSingle)
                    //{
                    //    int ret = SetCurrentHead(cardNo);
                    //    return ret == 0;
                    //}
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void DrMarkPause()
        {
            if (!isPause)
            {
                isPause = true;
            }
        }

        public void DrMarkRun()
        {
            if (isPause)
            {
                isPause = false;
            }
        }
    }
}
