using System;

namespace Drsoft.Plugin.Communication
{
    /// <summary>
    /// 二种方式选其一
    /// 使用GetResult函数同步获取结果就不能再使用ReceiveEventMsg
    /// 使用ReceiveEventMsg方式获取结果就不能再调用GetResult函数
    /// </summary>
    public abstract class CommunicationAbstract
    {
        public CommunicationParam? param = null;
        public abstract bool IsConnected { get;}
        public abstract event Func<byte[], byte[]>? OnReceiveEventMsg;
        public virtual event Action<Exception>? OnExceptionEvent;
        public int byteLength = 1024;

        public virtual void Start(CommunicationParam param)
        {
            if (param == null)
            {
                Exception ex = new Exception("CommunicationParam is null，please check");
                OnExceptionEvent?.Invoke(ex);
            }
        }
        public virtual void Start() => Start(param);
        public virtual void Reconnect() => Start(param);
        public abstract bool SendMsg(byte[] buffer);

        public abstract byte[] ReceiveMsg();

        public virtual byte[] GetResult(byte[] buffer)
        {
            if (param == null || param.IsUseReceiveEvent == true)
            {
                Exception ex;
                if (param == null)
                {
                    ex = new Exception("CommunicationParam is null，please check");
                }
                else
                {
                    ex = new Exception("IsUseReceiveEvent is true，can not action GetResult(), only action SendMsg()");
                }
                OnExceptionEvent?.Invoke(ex);
                return null;
            }
            return new byte[0];
        }

        public virtual void CleanInBuffer()
        {
        }
        public virtual void CleanOutBuffer()
        {
        }

        public abstract void Stop();
    }
}
