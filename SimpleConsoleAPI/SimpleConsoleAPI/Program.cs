using OAuth;
using System;
using System.Net;
using System.Web;
using System.IO;

namespace SimpleConsoleAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            OAuthBase oauthBase = new OAuthBase();
            string uri = "https://api-isv-oa.aps.odin.com/aps/2/resources/";
            string consumerKey = "blablaservice-v1-ad95e2443d814b8";
            string consumerSecret = "63473882d7d74aeb97d2ea17caff8f52";
            string timestamp = oauthBase.GenerateTimeStamp();
            string nonce = oauthBase.GenerateNonce();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Get; // change it to  WebRequestMethods.Http.Post if needed

            string normalizedUrl;
            string normalizedRequestParameters;

            // generating signature based on requst parameters
            string sig = HttpUtility.UrlEncode(
                oauthBase.GenerateSignature(
                    new Uri(uri),
                    consumerKey, 
                    consumerSecret, 
                    string.Empty, string.Empty,
                    request.Method, 
                    timestamp, 
                    nonce, 
                    out normalizedUrl, out normalizedRequestParameters));

            request.Headers.Add("Authorization", String.Format(
               "OAuth " +
                "oauth_consumer_key=\"{0}\"" +
               ",oauth_signature_method=\"HMAC-SHA1\"" +
               ",oauth_timestamp=\"{1}\"" +
               ",oauth_nonce=\"{2}\"" +
               ",oauth_version=\"1.0\"" +
               ",oauth_signature=\"{3}\"",
               consumerKey, timestamp, nonce, sig
               ));

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                       string text = reader.ReadToEnd();
                       Console.WriteLine(text);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
