namespace GUIDE.PLATFORM.MyControl
{
    public class EnumClass
    {
    }

    /// <summary>
    /// 灯光，开闭所，着箱
    /// </summary>
    public enum LightType
    {
        None,
        LightOn,
        LightOff,
        Lock,
        UnLock,
        CntrTouchOn,
        CntrTouchOff,
    }

    /// <summary>
    /// 按钮
    /// </summary>
    public enum RscButtonType
    { 
        None,
        Auto,
        HalfAuto,
        Manual,
        AutoRun,
        TerminalRun,
        TierCheck,
        TaskRollBack,
        More,
    }

    /// <summary>
    /// 岸桥颜色
    /// </summary>
    public enum BridgeColor
    { 
        Yellow,
        Green, 
        Blue, 
        Red,
    }

    /// <summary>
    /// 主题皮肤
    /// </summary>
    public enum SkinStyle
    { 
        Black,
        Blue,
    }

    public enum LockType 
    {
        Unlock,
        Lock,
    }

    public enum ContainerType
    { 
        GpEmpty,
        GpFull,
        HqEmpty,
        HqFull,
    }
}
