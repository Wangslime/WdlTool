using Drsoft.Plugin.ProxyAop;

namespace Drsoft.Plugin.ICommunication
{
    [ActionErrorFilter]
    [ActionExecutedFilter]
    public interface IVisualCamera
    {
        public string cameraName { get; set; }
        bool IsConnected { get; }
        void ReConnect();
        void TriggerSend();

        bool SendMessageInfo(string info);

        void Dispose();
    }
}
