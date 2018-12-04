using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace GameUtil
{
    public class HttpUtil
    {
        public static Encoding Encode = Encoding.UTF8;
        public static string ProcessRequestData(string url, string fileFormName, string data, string saveName, Dictionary<string, string> paramDict, bool localNet = false)
        {
            // 边界符
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            //var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 最后的结束符
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            // 文件参数头
            const string filePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                 "Content-Type: application/octet-stream\r\n\r\n";
            var fileHeader = string.Format(filePartHeader, fileFormName, saveName);
            var fileHeaderBytes = Encode.GetBytes(fileHeader);

            // 开始
            var memStream = new MemoryStream();
            memStream.Write(beginBoundary, 0, beginBoundary.Length);

            // 文件数据
            memStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

            var sendData = Encode.GetBytes(data);
            memStream.Write(sendData, 0, sendData.Length);
            var contentLine = Encoding.ASCII.GetBytes("\r\n");
            memStream.Write(contentLine, 0, contentLine.Length);
            /*
            var buffer = new byte[1024];
            int bytesRead; // =0
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
                */

            // Key-Value数据
            var stringKeyHeader = "\r\n--" + boundary +
                                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                    "\r\n\r\n{1}\r\n";
            if (!localNet)
            {
                // 格式：报文开始边界符
                memStream.Write(beginBoundary, 0, beginBoundary.Length);

                // Key-Value数据
                stringKeyHeader = "Content-Disposition: form-data; name=\"{0}\"" +
                                       "\r\n\r\n{1}\r\n";
            }


            var enumerator = paramDict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string paramStr = string.Format(stringKeyHeader, enumerator.Current.Key, enumerator.Current.Value);
                var paramBytes = Encode.GetBytes(paramStr);
                memStream.Write(paramBytes, 0, paramBytes.Length);
            }
            // 写入最后的结束边界符
            memStream.Write(endBoundary, 0, endBoundary.Length);

            //
            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            // 创建webRequest并设置属性
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.Timeout = 100000;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.ContentLength = tempBuffer.Length;

            var requestStream = webRequest.GetRequestStream();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            string responseContent;
            using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }

            httpWebResponse.Close();
            webRequest.Abort();

            return responseContent;
        }

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>

        // 同步接口
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Proxy = null;
            request.Method = "GET";
            request.KeepAlive = false;
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
                request.Timeout = timeout.Value;
            else
                request.Timeout = 2000;// 默认2s

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        // 异步接口
        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// 创建POST方式的HTTP请求,以字符的方式传入数据，json格式目前用于大数据对接
        public static HttpWebResponse CreatePostHttpResponse(string url, string strData, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据
            if (!string.IsNullOrEmpty(strData))
            {

                byte[] data = requestEncoding.GetBytes(strData);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        static void Test(string[] args)
        {
            string url = "http://192.168.191.175:801/func/iface_user_r.aspx?act=ios_getidentifier&";
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("mac", "");
            parameters.Add("idfv", "");
            parameters.Add("appid", "");
            parameters.Add("mode", "");
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in parameters.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                }
                i++;
            }
            Console.WriteLine(buffer);

            StringBuilder desbuff = new StringBuilder();
            desbuff.AppendFormat("1bec508e");
            string encodedata = DES8Bit.DESEncrypt(buffer.ToString(), desbuff.ToString());
            string senddata = "args=" + encodedata;
            url += senddata;
            //Console.WriteLine(url);

            string ret = "F9DFEB3D6DF10637A0167B04D82CA1988DA6878A36B429AF";
            ret = DES8Bit.DESDecryptt(ret, desbuff.ToString());
            Console.WriteLine(ret);

            HttpWebResponse response = HttpUtil.CreateGetHttpResponse(url, null, null, null);
            //打印返回值
            Stream stream = response.GetResponseStream();   //获取响应的字符串流
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流
            string html = sr.ReadToEnd();   //从头读到尾，放到字符串html
            sr.Close();
            stream.Close();
            response.Close();
            Console.WriteLine(html);
        }
    }
}
