using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WdlHttpClientNet
{
    public class HttpClientNet
    {
        private static readonly object LockObj = new object();
        private static HttpClient httpClient = null;
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
            //catch (WebException ex)
            //{
            //    if (ex.Status == WebExceptionStatus.ProtocolError)
            //    {
            //        using (HttpWebResponse err = ex.Response as HttpWebResponse)
            //        {
            //            if (err != null)
            //            {
            //                using (StreamReader streamReader = new StreamReader(err.GetResponseStream()))
            //                {
            //                    string htmlResponse = streamReader.ReadToEnd();
            //                    string txtResults = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
            //                    strReturn = txtResults;
            //                }
            //                return false;
            //            }
            //        }
            //    }
            //    strReturn = ex.Message + ex.StackTrace;
            //    return false;
            //}
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
            //catch (WebException ex)
            //{
            //    if (ex.Status == WebExceptionStatus.ProtocolError)
            //    {
            //        using (HttpWebResponse? err = ex.Response as HttpWebResponse)
            //        {
            //            if (err != null)
            //            {
            //                using (StreamReader streamReader = new StreamReader(err.GetResponseStream()))
            //                {
            //                    string htmlResponse = streamReader.ReadToEnd();
            //                    string txtResults = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
            //                    tuple = Tuple.Create(false, txtResults);
            //                }
            //            }
            //        }
            //    }
            //    tuple = Tuple.Create(false, ex.Message + ex.StackTrace);
            //    return tuple;
            //}
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
