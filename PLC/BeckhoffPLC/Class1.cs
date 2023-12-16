using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace MyAssembly
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Input
    {
        /// <summary>
        /// 暂停按钮
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pause { get; set; } = false;
        /// <summary>
        /// 报警消音按钮
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mute { get; set; } = false;
        /// <summary>
        /// 总气压表
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AirPress { get; set; } = false;
        /// <summary>
        /// 安全门开关1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor1 { get; set; } = false;
        /// <summary>
        /// 安全门开关2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor2 { get; set; } = false;
        /// <summary>
        /// 安全门开关3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor3 { get; set; } = false;
        /// <summary>
        /// 安全门开关4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor4 { get; set; } = false;
        /// <summary>
        /// 安全门开关5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor5 { get; set; } = false;
        /// <summary>
        /// 安全门开关6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor6 { get; set; } = false;
        /// <summary>
        /// 安全门开关7
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor7 { get; set; } = false;
        /// <summary>
        /// 安全门开关8
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor8 { get; set; } = false;
        /// <summary>
        /// 打标状态信号3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark3_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark4_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark5_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark6_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号7
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark7_Status { get; set; } = false;
        /// <summary>
        /// 光栅感应1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SaftyGrating1 { get; set; } = false;
        /// <summary>
        /// 光栅感应2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SaftyGrating2 { get; set; } = false;
        /// <summary>
        /// 光栅感应3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SaftyGrating3 { get; set; } = false;
        /// <summary>
        /// 光栅感应4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SaftyGrating4 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve2_5 { get; set; } = false;
        /// <summary>
        /// 上游准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UpStreamReady { get; set; } = false;
        /// <summary>
        /// 上游故障
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UpStreamAlm { get; set; } = false;
        /// <summary>
        /// 下游允许
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean DownStreamAllow { get; set; } = false;
        /// <summary>
        /// 下游故障
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean DownStreamAlm { get; set; } = false;
        /// <summary>
        /// 打标状态信号8
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark8_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号9
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark9_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号10
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark10_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号11
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark11_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号12
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark12_Status { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve2_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve2_16 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor1 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor2 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor3 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor4 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor5 { get; set; } = false;
        /// <summary>
        /// 龙门1光敏检测6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Sensor6 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor1 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor2 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor3 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor4 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor5 { get; set; } = false;
        /// <summary>
        /// 龙门2光敏检测6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Sensor6 { get; set; } = false;
        /// <summary>
        /// 冷水机1报警信号
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Cooling1_Alm { get; set; } = false;
        /// <summary>
        /// 冷水机2报警信号
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Cooling2_Alm { get; set; } = false;
        /// <summary>
        /// 打标状态信号1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark1_Status { get; set; } = false;
        /// <summary>
        /// 打标状态信号2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark2_Status { get; set; } = false;
        /// <summary>
        /// 放卷纠偏电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwSteer_LimP { get; set; } = false;
        /// <summary>
        /// 放卷纠偏电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwSteer_Home { get; set; } = false;
        /// <summary>
        /// 放卷纠偏电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwSteer_LimN { get; set; } = false;
        /// <summary>
        /// 放卷升降电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwLift_LimP { get; set; } = false;
        /// <summary>
        /// 放卷升降电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwLift_Home { get; set; } = false;
        /// <summary>
        /// 放卷升降电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean UwLift_LimN { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl1Basic { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl1Work { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl2Basic { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl2Work { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl3Basic { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl3Work { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl4Basic { get; set; } = false;
        /// <summary>
        /// 入口海绵夹紧气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl4Work { get; set; } = false;
        /// <summary>
        /// 入口流水线检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltSensor1 { get; set; } = false;
        /// <summary>
        /// 入口流水线检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltSensor2 { get; set; } = false;
        /// <summary>
        /// A侧刮膜电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling1_LimP { get; set; } = false;
        /// <summary>
        /// A侧刮膜电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling1_Home { get; set; } = false;
        /// <summary>
        /// A侧刮膜电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling1_LimN { get; set; } = false;
        /// <summary>
        /// A侧打齐模组1正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align11_LimP { get; set; } = false;
        /// <summary>
        /// A侧打齐模组1原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align11_Home { get; set; } = false;
        /// <summary>
        /// A侧打齐模组1负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align11_LimN { get; set; } = false;
        /// <summary>
        /// A侧打齐模组2正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align12_LimP { get; set; } = false;
        /// <summary>
        /// A侧打齐模组2原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align12_Home { get; set; } = false;
        /// <summary>
        /// A侧打齐模组2负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align12_LimN { get; set; } = false;
        /// <summary>
        /// A区升降平台正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z1_LimP { get; set; } = false;
        /// <summary>
        /// A区升降平台原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z1_Home { get; set; } = false;
        /// <summary>
        /// A区升降平台负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z1_LimN { get; set; } = false;
        /// <summary>
        /// A侧托盘输送检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_Sensor1 { get; set; } = false;
        /// <summary>
        /// A侧托盘输送检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_Sensor2 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_16 { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸1伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl1Work { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸1缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl1Basic { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸2伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl2Work { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸2缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl2Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵辊夹紧气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl1Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵辊夹紧气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl1Work { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve6_7 { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸3伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl3Work { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸3缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl3Basic { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸4伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl4Work { get; set; } = false;
        /// <summary>
        /// A侧托盘打齐气缸4缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl4Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵辊夹紧气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl2Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵辊夹紧气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl2Work { get; set; } = false;
        /// <summary>
        /// 中间流水线检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidBeltSensor1 { get; set; } = false;
        /// <summary>
        /// 中间流水线检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidBeltSensor2 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve6_16 { get; set; } = false;
        /// <summary>
        /// B侧刮膜电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling2_LimP { get; set; } = false;
        /// <summary>
        /// B侧刮膜电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling2_Home { get; set; } = false;
        /// <summary>
        /// B侧刮膜电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Peeling2_LimN { get; set; } = false;
        /// <summary>
        /// B侧打齐模组1正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align21_LimP { get; set; } = false;
        /// <summary>
        /// B侧打齐模组1原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align21_Home { get; set; } = false;
        /// <summary>
        /// B侧打齐模组1负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align21_LimN { get; set; } = false;
        /// <summary>
        /// B侧打齐模组2正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align22_LimP { get; set; } = false;
        /// <summary>
        /// B侧打齐模组2原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align22_Home { get; set; } = false;
        /// <summary>
        /// B侧打齐模组2负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Align22_LimN { get; set; } = false;
        /// <summary>
        /// B区升降平台正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z2_LimP { get; set; } = false;
        /// <summary>
        /// B区升降平台原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z2_Home { get; set; } = false;
        /// <summary>
        /// B区升降平台负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Z2_LimN { get; set; } = false;
        /// <summary>
        /// B侧托盘输送检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_Sensor1 { get; set; } = false;
        /// <summary>
        /// B侧托盘输送检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_Sensor2 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve7_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve7_16 { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸1伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl1Work { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸1缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl1Basic { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸2伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl2Work { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸2缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl2Basic { get; set; } = false;
        /// <summary>
        /// 龙门1正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_LimP { get; set; } = false;
        /// <summary>
        /// 龙门1原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_Home { get; set; } = false;
        /// <summary>
        /// 龙门1负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_LimN { get; set; } = false;
        /// <summary>
        /// B侧出口滚轮伸缩气缸缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutRollerCylBasic { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸3伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl3Work { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸3缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl3Basic { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸4伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl4Work { get; set; } = false;
        /// <summary>
        /// B侧托盘打齐气缸4缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl4Basic { get; set; } = false;
        /// <summary>
        /// 龙门2正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_LimP { get; set; } = false;
        /// <summary>
        /// 龙门2原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_Home { get; set; } = false;
        /// <summary>
        /// 龙门2负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_LimN { get; set; } = false;
        /// <summary>
        /// B侧出口滚轮伸缩气缸伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutRollerCylWork { get; set; } = false;
        /// <summary>
        /// 收卷纠偏电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwSteer_LimP { get; set; } = false;
        /// <summary>
        /// 收卷纠偏电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwSteer_Home { get; set; } = false;
        /// <summary>
        /// 收卷纠偏电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwSteer_LimN { get; set; } = false;
        /// <summary>
        /// 收卷升降电机正限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwLift_LimP { get; set; } = false;
        /// <summary>
        /// 收卷升降电机原点
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwLift_Home { get; set; } = false;
        /// <summary>
        /// 收卷升降电机负限位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RwLift_LimN { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl1Basic { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl1Work { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl2Basic { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl2Work { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl3Basic { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl3Work { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl4Basic { get; set; } = false;
        /// <summary>
        /// 出口海绵辊夹紧气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl4Work { get; set; } = false;
        /// <summary>
        /// 出口流水线检测1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutBeltSensor1 { get; set; } = false;
        /// <summary>
        /// 出口流水线检测2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutBeltSensor2 { get; set; } = false;
        /// <summary>
        /// 中间海绵夹紧气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidCyl3Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵夹紧气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl3Work { get; set; } = false;
        /// <summary>
        /// 中间海绵夹紧气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidCyl4Basic { get; set; } = false;
        /// <summary>
        /// 中间海绵夹紧气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl4Work { get; set; } = false;
        /// <summary>
        /// A侧入口滚轮伸缩气缸缩回
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InRollerCylBasic { get; set; } = false;
        /// <summary>
        /// A侧入口滚轮伸缩气缸伸出
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InRollerCylWork { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve10_7 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve10_8 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve10_9 { get; set; } = false;
        /// <summary>
        /// 升降平台A中间负压检测
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_MidVacOk { get; set; } = false;
        /// <summary>
        /// 升降平台A边沿负压检测
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_CycleVac1Ok { get; set; } = false;
        /// <summary>
        /// 升降平台B中间负压检测
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_MidVacOk { get; set; } = false;
        /// <summary>
        /// 升降平台B边沿负压检测
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_CycleVac1Ok { get; set; } = false;
        /// <summary>
        /// 龙门防撞光电1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean GantrySafeSensor1 { get; set; } = false;
        /// <summary>
        /// 龙门防撞光电2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean GantrySafeSensor2 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve10_16 { get; set; } = false;
        /// <summary>
        /// 入口流水线检测左
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltSensorL { get; set; } = false;
        /// <summary>
        /// 入口流水线检测右
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltSensorR { get; set; } = false;
        /// <summary>
        /// 打标卡1准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark1_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡2准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark2_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡3准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark3_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡4准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark4_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡5准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark5_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡6准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark6_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡7准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark7_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡8准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark8_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡9准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark9_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡10准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark10_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡11准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark11_Ready { get; set; } = false;
        /// <summary>
        /// 打标卡12准备好
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark12_Ready { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve11_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve11_16 { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl1Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl1Work { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl2Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl2Work { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl3Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl3Work { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl4Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl4Work { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸5松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl5Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸5夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl5Work { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸6松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl6Basic { get; set; } = false;
        /// <summary>
        /// A侧入口POE压膜气缸6夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_InPoeCyl6Work { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve12_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve12_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve12_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve12_16 { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl1Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl1Work { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl2Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl2Work { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl3Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl3Work { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl4Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl4Work { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸5松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl5Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸5夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl5Work { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸6松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl6Basic { get; set; } = false;
        /// <summary>
        /// A侧出口POE压膜气缸6夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_OutPoeCyl6Work { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve13_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve13_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve13_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve13_16 { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl1Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl1Work { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl2Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl2Work { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl3Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl3Work { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl4Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl4Work { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸5松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl5Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸5夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl5Work { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸6松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl6Basic { get; set; } = false;
        /// <summary>
        /// B侧入口POE压膜气缸6夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_InPoeCyl6Work { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve14_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve14_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve14_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve14_16 { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸1松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl1Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸1夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl1Work { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸2松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl2Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸2夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl2Work { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸3松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl3Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸3夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl3Work { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸4松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl4Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸4夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl4Work { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸5松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl5Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸5夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl5Work { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸6松开
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl6Basic { get; set; } = false;
        /// <summary>
        /// B侧出口POE压膜气缸6夹紧
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_OutPoeCyl6Work { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve15_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve15_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve15_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve15_16 { get; set; } = false;
        /// <summary>
        /// 位移尺11
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11 { get; set; } = 0;
        /// <summary>
        /// 位移尺12
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12 { get; set; } = 0;
        /// <summary>
        /// 位移尺21
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21 { get; set; } = 0;
        /// <summary>
        /// 位移尺22
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22 { get; set; } = 0;
        /// <summary>
        /// 收卷卷径测量
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_A_EOT { get; set; } = 0;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_B_EOT { get; set; } = 0;
        /// <summary>
        /// 放卷卷径测量
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_A_EOT { get; set; } = 0;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_B_EOT { get; set; } = 0;
        /// <summary>
        /// 放卷寻边传感器
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_TED { get; set; } = 0;
        /// <summary>
        /// 收卷寻边传感器
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_TED { get; set; } = 0;
        /// <summary>
        /// 放卷寻边随动传感器
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_TED_Wind { get; set; } = 0;
        /// <summary>
        /// 收卷寻边随动传感器
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_TED_Wind { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Output
    {
        /// <summary>
        /// 暂停按钮指示灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PauseLight { get; set; } = false;
        /// <summary>
        /// 报警消音指示灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MuteLight { get; set; } = false;
        /// <summary>
        /// 三色灯-黄灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean yellowLight { get; set; } = false;
        /// <summary>
        /// 三色灯-绿灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean greenLight { get; set; } = false;
        /// <summary>
        /// 三色灯-红灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean redLight { get; set; } = false;
        /// <summary>
        /// 三色灯-蜂鸣器
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Buzzer { get; set; } = false;
        /// <summary>
        /// 照明灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean LedLight { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_8 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_9 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_10 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_11 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_12 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve1_16 { get; set; } = false;
        /// <summary>
        /// 打标卡1启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark1Start { get; set; } = false;
        /// <summary>
        /// 打标卡2启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mark2Start { get; set; } = false;
        /// <summary>
        /// 龙门1光源
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1Light { get; set; } = false;
        /// <summary>
        /// 龙门2光源
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2Light { get; set; } = false;
        /// <summary>
        /// 入口流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltMotorFw { get; set; } = false;
        /// <summary>
        /// 入口流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InBeltMotorBw { get; set; } = false;
        /// <summary>
        /// 中间流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidBeltMotorFw { get; set; } = false;
        /// <summary>
        /// 中间流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidBeltMotorBw { get; set; } = false;
        /// <summary>
        /// 出口流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutBeltMotorFw { get; set; } = false;
        /// <summary>
        /// 出口流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutBeltMotorBw { get; set; } = false;
        /// <summary>
        /// 允许上游进料
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AllowToUp { get; set; } = false;
        /// <summary>
        /// 故障输出至上游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AlmToUp { get; set; } = false;
        /// <summary>
        /// 本机准备好至下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ReadyToDown { get; set; } = false;
        /// <summary>
        /// 故障输出至下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AlmToDown { get; set; } = false;
        /// <summary>
        /// 龙门1激光器使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1LaserEnable { get; set; } = false;
        /// <summary>
        /// 龙门2激光器使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2LaserEnable { get; set; } = false;
        /// <summary>
        /// 入口压膜气缸1电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl1 { get; set; } = false;
        /// <summary>
        /// 入口压膜气缸2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean InPressCyl2 { get; set; } = false;
        /// <summary>
        /// 中间压膜气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MidPressCyl { get; set; } = false;
        /// <summary>
        /// 出口压膜气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OutPressCyl { get; set; } = false;
        /// <summary>
        /// A侧托盘横向打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl1 { get; set; } = false;
        /// <summary>
        /// A侧托盘入口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl3 { get; set; } = false;
        /// <summary>
        /// A侧托盘出口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignCyl4 { get; set; } = false;
        /// <summary>
        /// B侧托盘横向打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl1 { get; set; } = false;
        /// <summary>
        /// B侧托盘入口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl3 { get; set; } = false;
        /// <summary>
        /// B侧托盘出口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignCyl4 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_11 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_12 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve3_16 { get; set; } = false;
        /// <summary>
        /// 升降平台A边沿真空发生器1电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_CycleVac1 { get; set; } = false;
        /// <summary>
        /// 升降平台A中间真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_MidVac { get; set; } = false;
        /// <summary>
        /// 升降平台B边沿真空发生器3电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_CycleVac1 { get; set; } = false;
        /// <summary>
        /// 升降平台B中间真空发生器4电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_MidVac { get; set; } = false;
        /// <summary>
        /// A侧真空泵启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_5 { get; set; } = false;
        /// <summary>
        /// B侧真空泵启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_6 { get; set; } = false;
        /// <summary>
        /// 升降平台A边沿真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_CycleVac2 { get; set; } = false;
        /// <summary>
        /// 升降平台B边沿真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_CycleVac2 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_9 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_10 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_11 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_12 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_13 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_14 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_15 { get; set; } = false;
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve4_16 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_1 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_2 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_3 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_4 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_5 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_6 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空7
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_7 { get; set; } = false;
        /// <summary>
        /// 平台A动力辊真空8
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_8 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_9 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_10 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空3
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_11 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空4
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_12 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空5
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_13 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空6
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_14 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空7
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_15 { get; set; } = false;
        /// <summary>
        /// 平台B动力辊真空8
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Reserve5_16 { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_HMIToPLC
    {
        /// <summary>
        /// 龙门1拍照完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_CameraShootDone { get; set; } = false;
        /// <summary>
        /// 龙门2拍照完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_CameraShootDone { get; set; } = false;
        /// <summary>
        /// 龙门1打标完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_MarkDone { get; set; } = false;
        /// <summary>
        /// 龙门2打标完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_MarkDone { get; set; } = false;
        /// <summary>
        /// 图片加载完成可以加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_ReadyToMark { get; set; } = false;
        /// <summary>
        /// 图片加载完成可以加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_ReadyToMark { get; set; } = false;
        /// <summary>
        /// 单次测功率完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PowerMeterDone { get; set; } = false;
        /// <summary>
        /// 测功率完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PowerMeterFinish { get; set; } = false;
        /// <summary>
        /// 功率测试_振镜号1-12 空闲0
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 iPowerTestUnitNum { get; set; } = 0;
        /// <summary>
        /// 台面A打齐偏移下发标志位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_AlignFlag { get; set; } = false;
        /// <summary>
        /// 台面A打齐偏移值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationA_AlignOffset { get; set; } = 0;
        /// <summary>
        /// 台面B打齐偏移下发标志位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_AlignFlag { get; set; } = false;
        /// <summary>
        /// 台面B打齐偏移值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationB_AlignOffset { get; set; } = 0;
        /// <summary>
        /// 膜清洗
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean DirtyNeedClean { get; set; } = false;
        /// <summary>
        /// 上位已收到测功率开始
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ClearPowerMeterStart { get; set; } = false;
        /// <summary>
        /// 设备不再进片
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean NoFeedIn { get; set; } = false;
        /// <summary>
        /// 更换硅胶膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeDirtyField { get; set; } = false;
        /// <summary>
        /// 上位报警
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PcALM { get; set; } = false;
        /// <summary>
        /// 上位机下发暂停信号
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PcPause { get; set; } = false;
        /// <summary>
        /// 上位机下发打标跳过信号
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)] public System.Boolean[] VisionPhotoError { get; set; } = new System.Boolean[12];
        /// <summary>
        /// 确认后，反向拉膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean FilmRollMoveRev_A { get; set; } = false;
        /// <summary>
        /// 确认后，反向拉膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean FilmRollMoveRev_B { get; set; } = false;
        /// <summary>
        /// A侧更换硅胶膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeDirtyField_A { get; set; } = false;
        /// <summary>
        /// A侧更换硅胶膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeDirtyField_B { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_PLCToHMI
    {
        /// <summary>
        /// 龙门1拍照请求
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_CameraRequest { get; set; } = false;
        /// <summary>
        /// 龙门2拍照请求
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_CameraRequest { get; set; } = false;
        /// <summary>
        /// 龙门1打标请求
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1_MarkRequest { get; set; } = false;
        /// <summary>
        /// 龙门2打标请求
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2_MarkRequest { get; set; } = false;
        /// <summary>
        /// 台面A加工完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationA_ProcessDone { get; set; } = false;
        /// <summary>
        /// 台面B加工完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean StationB_ProcessDone { get; set; } = false;
        /// <summary>
        /// 龙门加工台面号
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 GantryProcessStationgNum { get; set; } = 0;
        /// <summary>
        /// 龙门1拍照序号
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1CameraLineNum { get; set; } = 0;
        /// <summary>
        /// 龙门1加工序号
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1ProcessLineNum { get; set; } = 0;
        /// <summary>
        /// 龙门2拍照序号
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2CameraLineNum { get; set; } = 0;
        /// <summary>
        /// 龙门2加工序号
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2ProcessLineNum { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry1ProcessDone { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Gantry2ProcessDone { get; set; } = false;
        /// <summary>
        /// 功率测试_振镜号1-12 空闲0
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 iPowerTestUnitNum { get; set; } = 0;
        /// <summary>
        /// 功率测试_单激光号1-3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 iPowerTestLaserNum { get; set; } = 0;
        /// <summary>
        /// 台面A打齐拍照位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationA_AlignCamPos { get; set; } = 0;
        /// <summary>
        /// 台面B打齐拍照位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationB_AlignCamPos { get; set; } = 0;
        /// <summary>
        /// 测功率开始
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PowerMeterStart { get; set; } = false;
        /// <summary>
        /// 设备有片
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SystemHaveWafer { get; set; } = false;
        /// <summary>
        /// 组件ID
        /// </summary>
        [field: MarshalAs(UnmanagedType.LPStr)] public System.String GroupId { get; set; } = "";
        /// <summary>
        /// 换膜标
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeFilm { get; set; } = false;
        /// <summary>
        /// 自动拉膜过程中，卷径过小
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean FlimNotEnough { get; set; } = false;
        /// <summary>
        /// 换膜标记
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeFilm_A { get; set; } = false;
        /// <summary>
        /// 换膜标记
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ChangeFilm_B { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Camera
    {
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.R4)] public System.Single[] InCellDataWrite1 { get; set; } = new System.Single[6];
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.R4)] public System.Single[] InCellDataWrite2 { get; set; } = new System.Single[6];
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Parameter
    {
        /// <summary>
        /// 位移尺11安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11BasePos { get; set; } = 0;
        /// <summary>
        /// 位移尺12安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12BasePos { get; set; } = 0;
        /// <summary>
        /// 龙门1等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1WaitPos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A拍照位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAGrabPos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A拍照位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAGrabPos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark1Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark2Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark3Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark4Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark5Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark6Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark7Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面A打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationAMark8Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark1Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark2Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark3Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark4Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark5Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark6Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark7Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面A打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationAMark8Pos { get; set; } = 0;
        /// <summary>
        /// 刮膜1起点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Peeling1StartPos { get; set; } = 0;
        /// <summary>
        /// 刮膜1终点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Peeling1EndPos { get; set; } = 0;
        /// <summary>
        /// Z1下降位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z1_DownPos { get; set; } = 0;
        /// <summary>
        /// Z1顶升位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z1_UpPos { get; set; } = 0;
        /// <summary>
        /// 位移尺21安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21BasePos { get; set; } = 0;
        /// <summary>
        /// 位移尺22安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22BasePos { get; set; } = 0;
        /// <summary>
        /// 龙门2等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2WaitPos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B拍照位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBGrabPos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B拍照位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBGrabPos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark1Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark2Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark3Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark4Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark5Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark6Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark7Pos { get; set; } = 0;
        /// <summary>
        /// 龙门1台面B打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1StationBMark8Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark1Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark2Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark3Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark4Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark5Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark6Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark7Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2台面B打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2StationBMark8Pos { get; set; } = 0;
        /// <summary>
        /// 刮膜2起点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Peeling2StartPos { get; set; } = 0;
        /// <summary>
        /// 刮膜2终点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Peeling2EndPos { get; set; } = 0;
        /// <summary>
        /// Z2下降位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z2_DownPos { get; set; } = 0;
        /// <summary>
        /// Z2顶升位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z2_UpPos { get; set; } = 0;
        /// <summary>
        /// 挡光伺服1位置0
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter1Pos0 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服1位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter1Pos1 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服1位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter1Pos2 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服1位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter1Pos3 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服2位置0
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter2Pos0 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服2位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter2Pos1 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服2位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter2Pos2 { get; set; } = 0;
        /// <summary>
        /// 挡光伺服2位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CamShutter2Pos3 { get; set; } = 0;
        /// <summary>
        /// 放卷提升位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwLiftUpPos { get; set; } = 0;
        /// <summary>
        /// 收卷提升位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwLiftUpPos { get; set; } = 0;
        /// <summary>
        /// 焊接次数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ProcessTimes { get; set; } = 0;
        /// <summary>
        /// 拍照超时时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 GrabTimeOutSet { get; set; } = 0;
        /// <summary>
        /// 台面A真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationA_VacOkDelay { get; set; } = 0;
        /// <summary>
        /// 台面B真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationB_VacOkDelay { get; set; } = 0;
        /// <summary>
        /// 台面A破真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationA_BlowDelay { get; set; } = 0;
        /// <summary>
        /// 台面B破真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationB_BlowDelay { get; set; } = 0;
        /// <summary>
        /// 自动测功率片数设定
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 AutoLeaserMeasureNum { get; set; } = 0;
        /// <summary>
        /// 龙门1测功率位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry1PowerMeterPos { get; set; } = 0;
        /// <summary>
        /// 龙门2测功率位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry2PowerMeterPos { get; set; } = 0;
        /// <summary>
        /// 功率计测量光速一龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 LeftOffset { get; set; } = 0;
        /// <summary>
        /// 功率计测量光速二龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 MidOffset { get; set; } = 0;
        /// <summary>
        /// 功率计测量光速三龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RightOffset { get; set; } = 0;
        /// <summary>
        /// 功率计测量位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos1 { get; set; } = 0;
        /// <summary>
        /// 功率计测量位2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos2 { get; set; } = 0;
        /// <summary>
        /// 功率计测量位3
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos3 { get; set; } = 0;
        /// <summary>
        /// 功率计测量位4
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos4 { get; set; } = 0;
        /// <summary>
        /// 功率计测量位5
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos5 { get; set; } = 0;
        /// <summary>
        /// 功率计测量位6
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PowerMeterMeasurePos6 { get; set; } = 0;
        /// <summary>
        /// 放卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_ATorqueSet { get; set; } = 0;
        /// <summary>
        /// 收卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_ATorqueSet { get; set; } = 0;
        /// <summary>
        /// 拉膜长度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 TapeLength { get; set; } = 0;
        /// <summary>
        /// 台面1到位延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationPosADelay { get; set; } = 0;
        /// <summary>
        /// 台面2到位延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 StationPosBDelay { get; set; } = 0;
        /// <summary>
        /// 放卷力矩模式速度限幅
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwTorqueModeVeloLimt { get; set; } = 0;
        /// <summary>
        /// 收卷力矩模式速度限幅
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwTorqueModeVeloLimt { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_ARadius_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_ARadius_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_ARadius_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_ARadius_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_BRadius_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_BRadius_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_BRadius_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_BRadius_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteer_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteer_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteer_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteer_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteer_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteer_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteer_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteer_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 位移尺11检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 位移尺11检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 位移尺11检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 位移尺11检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler11_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 位移尺12检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 位移尺12检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 位移尺12检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 位移尺12检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler12_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 位移尺21检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 位移尺21检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 位移尺21检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 位移尺21检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler21_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 位移尺22检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 位移尺22检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 位移尺22检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 位移尺22检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Ruler22_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 打齐11等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Align11WaitPos { get; set; } = 0;
        /// <summary>
        /// 打齐12等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Align12WaitPos { get; set; } = 0;
        /// <summary>
        /// 打齐21等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Align21WaitPos { get; set; } = 0;
        /// <summary>
        /// 打齐22等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Align22WaitPos { get; set; } = 0;
        /// <summary>
        /// Z1刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z1_PeelingPos { get; set; } = 0;
        /// <summary>
        /// Z2刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Z2_PeelingPos { get; set; } = 0;
        /// <summary>
        /// 龙门11安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry11BasePos { get; set; } = 0;
        /// <summary>
        /// 龙门12安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry12BasePos { get; set; } = 0;
        /// <summary>
        /// 龙门21安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry21BasePos { get; set; } = 0;
        /// <summary>
        /// 龙门22安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry22BasePos { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏放基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteerSetPos_Unwind { get; set; } = 0;
        /// <summary>
        /// 放卷纠偏收基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwSteerSetPos_Wind { get; set; } = 0;
        /// <summary>
        /// 收卷纠偏放基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteerSetPos_Unwind { get; set; } = 0;
        /// <summary>
        /// 收卷纠偏收基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwSteerSetPos_Wind { get; set; } = 0;
        /// <summary>
        /// 放卷张紧力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UnWindTorque { get; set; } = 0;
        /// <summary>
        /// 收卷张紧力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 WindTorque { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_BRadius_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_BRadius_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_BRadius_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 放卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_BRadius_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_ARadius_AnalogMax { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_ARadius_AnalogMin { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_ARadius_MeasurementMax { get; set; } = 0;
        /// <summary>
        /// 收卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_ARadius_MeasurementMin { get; set; } = 0;
        /// <summary>
        /// 放卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Uw_BTorqueSet { get; set; } = 0;
        /// <summary>
        /// 收卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Rw_BTorqueSet { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Command
    {
        /// <summary>
        /// 启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Start { get; set; } = false;
        /// <summary>
        /// 停止
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Stop { get; set; } = false;
        /// <summary>
        /// 回零
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Home { get; set; } = false;
        /// <summary>
        /// 暂停
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pause { get; set; } = false;
        /// <summary>
        /// 消音
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Mute { get; set; } = false;
        /// <summary>
        /// 报警清除
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Alarm_Ack { get; set; } = false;
        /// <summary>
        /// 检修模式
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RepairMode { get; set; } = false;
        /// <summary>
        /// 屏蔽安全门
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ShiledSafeDoor { get; set; } = false;
        /// <summary>
        /// 所有轴去使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AllAxis_Disable { get; set; } = false;
        /// <summary>
        /// 标定模式
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean CaliMode { get; set; } = false;
        /// <summary>
        /// 禁止上游进片
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RefusePiece_Upstream { get; set; } = false;
        /// <summary>
        /// 模拟下游要片
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Simu_Downstream { get; set; } = false;
        /// <summary>
        /// 屏蔽拍照
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ShiledCam { get; set; } = false;
        /// <summary>
        /// 屏蔽打标
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ShiledMark { get; set; } = false;
        /// <summary>
        /// 功率计测量开始
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PowerMeterStart { get; set; } = false;
        /// <summary>
        /// 清洗膜
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean CleanTape { get; set; } = false;
        /// <summary>
        /// 计数清零
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ClearStatistical { get; set; } = false;
        /// <summary>
        /// 龙门A打齐
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualAlignStationA { get; set; } = false;
        /// <summary>
        /// 龙门B打齐
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualAlignStationB { get; set; } = false;
        /// <summary>
        /// 台面A顶起
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualLiftUpStationA { get; set; } = false;
        /// <summary>
        /// 台面B顶起
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualLiftUpStationB { get; set; } = false;
        /// <summary>
        /// 台面A下降
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualLiftDownStationA { get; set; } = false;
        /// <summary>
        /// 台面B下降
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualLiftDownStationB { get; set; } = false;
        /// <summary>
        /// 物料进料到A台面
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualUpStreamToStationA { get; set; } = false;
        /// <summary>
        /// 物料A台面到B台面
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualStationAToStationB { get; set; } = false;
        /// <summary>
        /// 物料B台面到出料
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualStationBToDownStream { get; set; } = false;
        /// <summary>
        /// 所有皮带正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualAllBeltFw { get; set; } = false;
        /// <summary>
        /// 所有皮带反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualAllBeltBw { get; set; } = false;
        /// <summary>
        /// 台面A到刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AxisZStationAToPeelingPos { get; set; } = false;
        /// <summary>
        /// 台面B到刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AxisZStationBToPeelingPos { get; set; } = false;
        /// <summary>
        /// 屏蔽台面A加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ShiledStationA { get; set; } = false;
        /// <summary>
        /// 屏蔽台面B加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ShiledStationB { get; set; } = false;
        /// <summary>
        /// R2R正向点动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RollMoveFor_A { get; set; } = false;
        /// <summary>
        /// R2R反向点动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RollMoveRev_A { get; set; } = false;
        /// <summary>
        /// R2R正向点动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RollMoveFor_B { get; set; } = false;
        /// <summary>
        /// R2R反向点动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean RollMoveRev_B { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Simu_Upstream { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Ceshi { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Status
    {
        /// <summary>
        /// 运行中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Running { get; set; } = false;
        /// <summary>
        /// 停止
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Stop { get; set; } = false;
        /// <summary>
        /// 暂停
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pause { get; set; } = false;
        /// <summary>
        /// 报警
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean AutoAlarm { get; set; } = false;
        /// <summary>
        /// 1秒脉冲
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean OneSecend { get; set; } = false;
        /// <summary>
        /// 回零中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Homing { get; set; } = false;
        /// <summary>
        /// 回零完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Homed { get; set; } = false;
        /// <summary>
        /// 进料计数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CountInPiece { get; set; } = 0;
        /// <summary>
        /// 出料计数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CountOutPiece { get; set; } = 0;
        /// <summary>
        /// 平台A放卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UW_A_ActRadius { get; set; } = 0;
        /// <summary>
        /// 平台B收卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RW_B_ActRadius { get; set; } = 0;
        /// <summary>
        /// 打标时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CT_Mark { get; set; } = 0;
        /// <summary>
        /// 拍照时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CT_Camera { get; set; } = 0;
        /// <summary>
        /// 加工时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 CT_Machining { get; set; } = 0;
        /// <summary>
        /// 安全门
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeDoor { get; set; } = false;
        /// <summary>
        /// 安全光栅
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean SafeGrating { get; set; } = false;
        /// <summary>
        /// 产能
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 PPS { get; set; } = 0;
        /// <summary>
        /// 手动指令完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ManualOperationComplete { get; set; } = false;
        /// <summary>
        /// 龙门11反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry11ActTorque { get; set; } = 0;
        /// <summary>
        /// 龙门12反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry12ActTorque { get; set; } = 0;
        /// <summary>
        /// 龙门21反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry21ActTorque { get; set; } = 0;
        /// <summary>
        /// 龙门22反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry22ActTorque { get; set; } = 0;
        /// <summary>
        /// 龙门11反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry11MaxTorque { get; set; } = 0;
        /// <summary>
        /// 龙门12反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry12MaxTorque { get; set; } = 0;
        /// <summary>
        /// 龙门21反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry21MaxTorque { get; set; } = 0;
        /// <summary>
        /// 龙门22反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Gantry22MaxTorque { get; set; } = 0;
        /// <summary>
        /// 放卷反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UwActTorque { get; set; } = 0;
        /// <summary>
        /// 收卷反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RwActTorque { get; set; } = 0;
        /// <summary>
        /// 龙门1图片位置转换 1-24
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Pictrue_Gantry1Pos { get; set; } = 0;
        /// <summary>
        /// 龙门2图片位置转换 1-24
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Pictrue_Gantry2Pos { get; set; } = 0;
        /// <summary>
        /// 上游到台面A
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pictrue_UpstreamToStaA { get; set; } = false;
        /// <summary>
        /// 台面A到台面B
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pictrue_StaAToStaB { get; set; } = false;
        /// <summary>
        /// 台面B到下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Pictrue_StaBToDownstream { get; set; } = false;
        /// <summary>
        /// 台面有片显示
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Display_StaAup_HavePiece { get; set; } = false;
        /// <summary>
        /// 台面有片显示
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Display_StaAdown_HavePiece { get; set; } = false;
        /// <summary>
        /// 台面有片显示
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Display_StaBup_HavePiece { get; set; } = false;
        /// <summary>
        /// 台面有片显示
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Display_StaBdown_HavePiece { get; set; } = false;
        /// <summary>
        /// 平台B放卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 UW_B_ActRadius { get; set; } = 0;
        /// <summary>
        /// 平台A收卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RW_A_ActRadius { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_ProcessInfo
    {
        /// <summary>
        /// 表示PC是否异常
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean PCError { get; set; } = false;
        /// <summary>
        /// 表示下位机是否允许加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ReadyToProcess { get; set; } = false;
        /// <summary>
        /// 回退到上一个工位
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean ReturnLastPos { get; set; } = false;
        /// <summary>
        /// 表示龙门1的6个打标头是否加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)] public System.Boolean[] MarkInfo1 { get; set; } = new System.Boolean[6];
        /// <summary>
        /// 表示龙门1正在加工第几条线
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ProcessLineNum1 { get; set; } = 0;
        /// <summary>
        /// 表示龙门2的6个打标头是否加工
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)] public System.Boolean[] MarkInfo2 { get; set; } = new System.Boolean[6];
        /// <summary>
        /// 表示龙门2正在加工第几条线
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ProcessLineNum2 { get; set; } = 0;
        /// <summary>
        /// 表示龙门1的加工位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ProcessStaNum1 { get; set; } = 0;
        /// <summary>
        /// 表示龙门2的加工位
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ProcessStaNum2 { get; set; } = 0;
        /// <summary>
        /// 心跳计数，从1到1000反复增加
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 HeartBeat { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class sT_AxisParameter
    {
        /// <summary>
        /// 回零偏移
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 HomeOffset { get; set; } = 0;
        /// <summary>
        /// 相对距离
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 RelDistance { get; set; } = 0;
        /// <summary>
        /// 绝对位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 AbsPosition1 { get; set; } = 0;
        /// <summary>
        /// 绝对位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 AbsPosition2 { get; set; } = 0;
        /// <summary>
        /// 回零速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 HomeVelo { get; set; } = 0;
        /// <summary>
        /// 手动速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ManualVelo { get; set; } = 0;
        /// <summary>
        /// 工作速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 WorkVelo { get; set; } = 0;
        /// <summary>
        /// 加速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Acc { get; set; } = 0;
        /// <summary>
        /// 减速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 Dec { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class sT_AxisCommand
    {
        /// <summary>
        /// 去使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Disable { get; set; } = false;
        /// <summary>
        /// 回零
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Home { get; set; } = false;
        /// <summary>
        /// 走相对
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MoveRel { get; set; } = false;
        /// <summary>
        /// 走绝对1
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MoveAbs1 { get; set; } = false;
        /// <summary>
        /// 走绝对2
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean MoveAbs2 { get; set; } = false;
        /// <summary>
        /// 点动+
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean JogFw { get; set; } = false;
        /// <summary>
        /// 点动-
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean JogBw { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class sT_AxisState
    {
        /// <summary>
        /// 使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Enable { get; set; } = false;
        /// <summary>
        /// 运动中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Moving { get; set; } = false;
        /// <summary>
        /// 回零中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Homing { get; set; } = false;
        /// <summary>
        /// 回零完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Homed { get; set; } = false;
        /// <summary>
        /// 轴错误
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Error { get; set; } = false;
        /// <summary>
        /// 轴错误ID
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ErrorID { get; set; } = 0;
        /// <summary>
        /// 设定位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 SetPos { get; set; } = 0;
        /// <summary>
        /// 实际位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ActPos { get; set; } = 0;
        /// <summary>
        /// 实际速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 ActVelo { get; set; } = 0;
        /// <summary>
        /// 耦合状态
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Coupled { get; set; } = false;
        /// <summary>
        /// 安全距离
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] public System.Int32 SafetyDistance { get; set; } = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_Axis
    {
        /// <summary>
        /// 
        /// </summary>
        public sT_AxisParameter Param { get; set; } = new sT_AxisParameter();
        /// <summary>
        /// 
        /// </summary>
        public sT_AxisCommand Command { get; set; } = new sT_AxisCommand();
        /// <summary>
        /// 
        /// </summary>
        public sT_AxisState Status { get; set; } = new sT_AxisState();
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ST_PieceStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.LPStr)] public System.String ID { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean HavePiece { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Processing { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean Processed { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AlarmClass
    {
        /// <summary>
        /// 故障警报
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 301, ArraySubType = UnmanagedType.I1)] public System.Boolean[] Alarm { get; set; } = new System.Boolean[301];
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class g_PlcVersionClass
    {
        /// <summary>
        /// PLC程序版本号
        /// </summary>
        [field: MarshalAs(UnmanagedType.LPStr)] public System.String g_PlcVersion { get; set; } = "";
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class FirstCycleClass
    {
        /// <summary>
        /// 第一个扫描周期
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] public System.Boolean FirstCycle { get; set; } = false;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Global_Variables
    {
        /// <summary>
        /// 
        /// </summary>
        public ST_Input stInput { get; set; } = new ST_Input();
        /// <summary>
        /// 
        /// </summary>
        public ST_Output stOutput { get; set; } = new ST_Output();
        /// <summary>
        /// 
        /// </summary>
        public ST_HMIToPLC stHMIToPLC { get; set; } = new ST_HMIToPLC();
        /// <summary>
        /// 
        /// </summary>
        public ST_PLCToHMI stPLCToHMI { get; set; } = new ST_PLCToHMI();
        /// <summary>
        /// 
        /// </summary>
        public ST_Camera stCamera { get; set; } = new ST_Camera();
        /// <summary>
        /// 
        /// </summary>
        public ST_Parameter stParam { get; set; } = new ST_Parameter();
        /// <summary>
        /// 
        /// </summary>
        public ST_Command stCommand { get; set; } = new ST_Command();
        /// <summary>
        /// 
        /// </summary>
        public ST_Status stStatus { get; set; } = new ST_Status();
        /// <summary>
        /// 
        /// </summary>
        public ST_ProcessInfo stProcessInfo { get; set; } = new ST_ProcessInfo();
        /// <summary>
        /// 龙门1-1平移伺服
        /// </summary>
        public ST_Axis stAxis_Gantry11 { get; set; } = new ST_Axis();
        /// <summary>
        /// 龙门1-2平移伺服
        /// </summary>
        public ST_Axis stAxis_Gantry12 { get; set; } = new ST_Axis();
        /// <summary>
        /// 龙门2-1平移伺服
        /// </summary>
        public ST_Axis stAxis_Gantry21 { get; set; } = new ST_Axis();
        /// <summary>
        /// 龙门2-2平移伺服
        /// </summary>
        public ST_Axis stAxis_Gantry22 { get; set; } = new ST_Axis();
        /// <summary>
        /// A侧横向打齐模组1
        /// </summary>
        public ST_Axis stAxis_Align11 { get; set; } = new ST_Axis();
        /// <summary>
        /// A侧横向打齐模组2
        /// </summary>
        public ST_Axis stAxis_Align12 { get; set; } = new ST_Axis();
        /// <summary>
        /// B侧横向打齐模组3
        /// </summary>
        public ST_Axis stAxis_Align21 { get; set; } = new ST_Axis();
        /// <summary>
        /// B侧横向打齐模组4
        /// </summary>
        public ST_Axis stAxis_Align22 { get; set; } = new ST_Axis();
        /// <summary>
        /// A侧升降平台伺服
        /// </summary>
        public ST_Axis stAxis_Z1 { get; set; } = new ST_Axis();
        /// <summary>
        /// B侧升降平台伺服
        /// </summary>
        public ST_Axis stAxis_Z2 { get; set; } = new ST_Axis();
        /// <summary>
        /// 放卷升降伺服
        /// </summary>
        public ST_Axis stAxis_Uw_ALift { get; set; } = new ST_Axis();
        /// <summary>
        /// 放卷驱动伺服
        /// </summary>
        public ST_Axis stAxis_Uw_A { get; set; } = new ST_Axis();
        /// <summary>
        /// 收卷驱动伺服
        /// </summary>
        public ST_Axis stAxis_Rw_A { get; set; } = new ST_Axis();
        /// <summary>
        /// 清洗辊伺服
        /// </summary>
        public ST_Axis stAxis_Clean_A { get; set; } = new ST_Axis();
        /// <summary>
        /// 收卷升降伺服
        /// </summary>
        public ST_Axis stAxis_Rw_Blift { get; set; } = new ST_Axis();
        /// <summary>
        /// 放卷驱动伺服
        /// </summary>
        public ST_Axis stAxis_Uw_B { get; set; } = new ST_Axis();
        /// <summary>
        /// 收卷驱动伺服
        /// </summary>
        public ST_Axis stAxis_Rw_B { get; set; } = new ST_Axis();
        /// <summary>
        /// 清洗辊伺服
        /// </summary>
        public ST_Axis stAxis_Clean_B { get; set; } = new ST_Axis();
        /// <summary>
        /// 放卷纠偏伺服
        /// </summary>
        public ST_Axis stAxis_Uw_ASteer { get; set; } = new ST_Axis();
        /// <summary>
        /// A侧刮膜装置伺服
        /// </summary>
        public ST_Axis stAxis_Peeling1 { get; set; } = new ST_Axis();
        /// <summary>
        /// A侧托盘输送伺服
        /// </summary>
        public ST_Axis stAxis_StationA_Belt { get; set; } = new ST_Axis();
        /// <summary>
        /// B侧刮膜装置伺服
        /// </summary>
        public ST_Axis stAxis_Peeling2 { get; set; } = new ST_Axis();
        /// <summary>
        /// B侧托盘输送伺服
        /// </summary>
        public ST_Axis stAxis_StationB_Belt { get; set; } = new ST_Axis();
        /// <summary>
        /// 收卷纠偏伺服
        /// </summary>
        public ST_Axis stAxis_Rw_BSteer { get; set; } = new ST_Axis();
        /// <summary>
        /// 动力辊1
        /// </summary>
        public ST_Axis stAxis_PowerRoll1 { get; set; } = new ST_Axis();
        /// <summary>
        /// 动力辊2
        /// </summary>
        public ST_Axis stAxis_PowerRoll2 { get; set; } = new ST_Axis();
        /// <summary>
        /// 编码器轴1
        /// </summary>
        public ST_Axis stAxis_Encoder1 { get; set; } = new ST_Axis();
        /// <summary>
        /// 编码器轴2
        /// </summary>
        public ST_Axis stAxis_Encoder2 { get; set; } = new ST_Axis();
        /// <summary>
        /// 清洗辊伺服
        /// </summary>
        public ST_Axis stAxis_Clean_UwA { get; set; } = new ST_Axis();
        /// <summary>
        /// 清洗辊伺服
        /// </summary>
        public ST_Axis stAxis_Clean_UwB { get; set; } = new ST_Axis();
        /// <summary>
        /// 上游工位状态
        /// </summary>
        public ST_PieceStatus StaUpstream { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// A上工位状态
        /// </summary>
        public ST_PieceStatus StaAup { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// A下工位状态
        /// </summary>
        public ST_PieceStatus StaAdown { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// B上工位状态
        /// </summary>
        public ST_PieceStatus StaBup { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// B下工位状态
        /// </summary>
        public ST_PieceStatus StaBdown { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// 下游工位状态
        /// </summary>
        public ST_PieceStatus StaDownstream { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// 空状态
        /// </summary>
        public ST_PieceStatus StaEmpty { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// A传送B中间状态
        /// </summary>
        public ST_PieceStatus StaMidDown { get; set; } = new ST_PieceStatus();
        /// <summary>
        /// 故障警报
        /// </summary>
        public AlarmClass Alarm { get; set; } = new AlarmClass();
        /// <summary>
        /// PLC程序版本号
        /// </summary>
        public g_PlcVersionClass g_PlcVersion { get; set; } = new g_PlcVersionClass();
        /// <summary>
        /// 第一个扫描周期
        /// </summary>
        public FirstCycleClass FirstCycle { get; set; } = new FirstCycleClass();
    }
}
