using ServiceReference1;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WebService
{
    public class WebServiceTest
    {
        string tosWebUrl = "http://139.159.216.143:8888/IMSSServer";
        int timeout = 3;
        public async Task<string> RunWebService<T>(string strSend)
        {
            string retStr = "";
            try
            {
                BasicHttpBinding binding = new()
                {
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                };
                if (timeout != 0)
                {
                    binding.ReceiveTimeout = TimeSpan.FromSeconds(timeout);
                    binding.SendTimeout = TimeSpan.FromSeconds(timeout);
                }
                EndpointAddress address = new(@tosWebUrl);
                using MSSWebServiceClient client = new(binding, address);
                MySoapHeader Header = new();
                Header.username = "admin";
                Header.password = "LCy*F=KIQm@L";
                AddressHeader soapheader = AddressHeader.CreateAddressHeader("Header", "http://tempuri.org", Header);
                EndpointAddressBuilder eab = new(client.Endpoint.Address);
                eab.Headers.Add(soapheader);//将地址头加入进去
                client.Endpoint.Address = eab.ToEndpointAddress();

                //string strSend = jOSend.ToString(Formatting.None);// EncryptTool.AESEncryptV(jOSend.ToString(Formatting.None));
                var excuteIMSSResponse = await client.ExcuteIMSSAsync(strSend);
                retStr = excuteIMSSResponse.Body.@return;
                //resultString = EncryptTool.AESDecryptV(resultString);
                //T t = retStr.JsonToObj<T>();

                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);
                //Logs.TosWebService($"调用WebService接口:\r\n输入参数 = {jOSend}\r\n返回结果 = {retStr}\r\n耗时 = {Math.Round(end.Subtract(start).TotalMilliseconds)}ms");

                return retStr;

            }
            catch (Exception ex)
            {
                retStr = ex.Message + " , " + ex.StackTrace;
                TimeSpan end = new TimeSpan(DateTime.Now.Ticks);
                //Logs.Error($"调用WebService接口报错:\r\n输入参数 = {jOSend}\r\n返回结果 = {retStr}\r\n耗时 = {Math.Round(end.Subtract(start).TotalMilliseconds)}ms");
                return "";
            }
        }
    }

    public class MySoapHeader
    {
        public string username = "";
        public string password = "";
    }
}