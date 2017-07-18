using OAuth;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;


namespace SimpleAPI.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {


            return products;


        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            /*
            var crypto = new HMACSHA1();
            var key = "{0}&{1}".FormatWith("", "");

            crypto.Key = _encoding.GetBytes(key);
            signature = signatureBase.HashWith(crypto);

            break;
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            


            OAuthBase oauthBase = new OAuthBase();
            string uri = "https://api-isv-oa.aps.odin.com/aps/2/resources/";
            string consumerKey = "blablaservice-v1-ad95e2443d814b8";
            string consumerSecret = "63473882d7d74aeb97d2ea17caff8f52";
            string timestamp = oauthBase.GenerateTimeStamp();
            string nonce = oauthBase.GenerateNonce();
            string normalizedUrl;
            string normalizedRequestParameters;
            string sig = HttpUtility.UrlEncode(oauthBase.GenerateSignature(
                new Uri(uri), consumerKey, consumerSecret, string.Empty, string.Empty,
                "GET", timestamp, nonce, out normalizedUrl, out normalizedRequestParameters));
            string requestUrl = String.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(normalizedUrl);
            request.Headers.Add("Authorization", String.Format("OAuth {0}", normalizedRequestParameters));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            */
            product.Name = "PASHA";// requestUrl;// response.ToString();
            return Ok(product);
        }
    }
}
