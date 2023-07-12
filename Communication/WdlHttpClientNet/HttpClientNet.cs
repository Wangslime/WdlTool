using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WdlHttpClientNet
{
    public class HttpClientNet
    {
        private static readonly object LockObj = new object();
        private static HttpClient httpClient = null;
        public static int Timeout = 0;
        public HttpClientNet()
        {
            GetInstance();
        }

        public HttpClientNet(int timeout)
        {
            GetInstance(timeout);
        }
        private static HttpClient GetInstance(int timeout = 0)
        {
            if (httpClient == null)
            {
                lock (LockObj)
                {
                    if (httpClient == null)
                    {
                        httpClient = new HttpClient();
                        if (timeout != 0)
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
                        }
                    }
                }
            }
            return httpClient;
        }

        #region POST
        public string PostSync(string url, string strJson)//post同步请求方法
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/json";
                if (Timeout > 0)
                {
                    request.Timeout = Timeout;
                }

                if (!string.IsNullOrEmpty(strJson))
                {
                    var bytes = Encoding.UTF8.GetBytes(strJson);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = $"请求数据失败。请求字符串：{url}\r\n状态码：{response.StatusCode}\r\n描述信息：{response.StatusDescription}";
                        throw new ApplicationException(message);
                    }

                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }
                    return responseValue;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Post(string url, string strJson)//post同步请求方法
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                HttpContent content = new StringContent(strJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //由HttpClient发出Post请求
                Task<HttpResponseMessage> res = httpClient.PostAsync(url, content);
                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    return res.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception(res.Result.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> PostAsync(string strUrl, string strJson)//post异步请求方法
        {
            Tuple<bool, string> tuple;
            try
            {
                HttpContent content = new StringContent(strJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //由HttpClient发出异步Post请求
                HttpResponseMessage res = await httpClient.PostAsync(strUrl, content);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    return await res.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(res.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string HttpPostFormData(string url, string name, string strJson)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent
                {
                    { new StringContent(strJson), name }
                };
                //由HttpClient发出Post请求
                Task<HttpResponseMessage> res = httpClient.PostAsync(url, multipartFormDataContent);
                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    return res.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception(res.Result.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<string> HttpPostFormDataAsync(string url, string name, string strJson)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent
                {
                    { new StringContent(strJson), name }
                };
                //由HttpClient发出Post请求
                Task<HttpResponseMessage> res = httpClient.PostAsync(url, multipartFormDataContent);
                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    return res.Result.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(res.Result.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string HttpPostToken(string strUrl, string strJson, string token)
        {
            if (string.IsNullOrEmpty(strUrl))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                HttpContent content = new StringContent(strJson);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //由HttpClient发出Post请求
                Task<HttpResponseMessage> res = httpClient.PostAsync(strUrl, content);
                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    return res.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception(res.Result.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<string> HttpPostTokenAsync(string strUrl, string strJson, string token)
        {
            if (string.IsNullOrEmpty(strUrl))
            {
                throw new Exception("未设置URL,请调用SetServiceURL函数设置连接字符串");
            }
            try
            {
                var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                HttpContent content = new StringContent(strJson);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //由HttpClient发出Post请求
                Task<HttpResponseMessage> res = httpClient.PostAsync(strUrl, content);
                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    return res.Result.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(res.Result.StatusCode.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GET
        public string Get(string url)
        {
            try
            {
                return httpClient.GetStringAsync(url).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<string> GetAsync(string url)
        {
            try
            {
                return httpClient.GetStringAsync(url);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string HttpGetToken(string strUrl, string token)
        {
            try
            {
                var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
                return httpClient.GetStringAsync(strUrl).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<string> HttpGetTokenAsync(string strUrl, string token)
        {
            try
            {
                var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
                return httpClient.GetStringAsync(strUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
