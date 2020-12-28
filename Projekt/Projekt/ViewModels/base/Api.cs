using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public class Api
    {
        //akademik: 
        //const string URL = "http://192.168.137.125:5000";
        //const string URL = "http://82.145.78.188:5000";
        //
        const string URL = "http://82.145.78.212:5000";
        //const string URL = "http://192.168.0.10:5000";
        const string Content = "application/json; charset=utf-8";

        /// <summary>
        /// OBSŁUGA ZABYTAN
        /// </summary>
        /// <param name="Method">GET, POST, PUT, DELETE</param>
        /// <param name="Url">REQUEST</param>
        /// <param name="ObjJson">OBJEKT DO WYSLANIA METODY [POST PUT]</param>
        /// <param name="RequestHeaders">OBIEKT DO WYSŁANIA METODY [GET DELETE]</param>
        /// <returns></returns>
        protected string Execute(string Method, string Url, object ObjJson = null, NameValueCollection RequestHeaders = null)
        {
            //TWORZENIE ZAPYTANIA
            //-------------------
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + Url);
            request.Accept = Content;
            request.ContentType = Content;
            request.Method = Method;
            if (RequestHeaders != null)
            {
                foreach (string header in RequestHeaders.AllKeys)
                {
                    request.Headers.Add(header, RequestHeaders[header]);
                }
            }
            
            //WYSYŁANIE DANYCH W POSACI JSON
            //------------------------------
            if (ObjJson != null)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string text;
                    if (ObjJson is string)
                    {
                        text = ObjJson.ToString();
                    }
                    else
                    {
                        text = JsonConvert.SerializeObject(ObjJson);
                    }

                    streamWriter.Write(text);
                }
            }

            //ODBIERANIE DANYCH
            //-----------------
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        async static protected Task<string> ExecuteAsync(string Method, string Url, object ObjJson = null, NameValueCollection RequestHeaders = null)
        {
            //TWORZENIE ZAPYTANIA
            //-------------------
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + Url);
            request.Accept = Content;
            request.ContentType = Content;
            request.Method = Method;
            if (RequestHeaders != null)
            {
                foreach (string header in RequestHeaders.AllKeys)
                {
                    request.Headers.Add(header, RequestHeaders[header]);
                }
            }

            //WYSYŁANIE DANYCH W POSACI JSON
            //------------------------------
            if (ObjJson != null)
            {
                using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync().ConfigureAwait(false)))
                {
                    string text;
                    if (ObjJson is string)
                    {
                        text = ObjJson.ToString();
                    }
                    else
                    {
                        text = JsonConvert.SerializeObject(ObjJson);
                    }

                    await streamWriter.WriteAsync(text).ConfigureAwait(false);
                }
            }

            //ODBIERANIE DANYCH
            //-----------------
            try
            {
                string result = "";
                await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null).ContinueWith(async task =>
                {
                    HttpWebResponse response = (HttpWebResponse)task.Result;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        result = await streamReader.ReadToEndAsync();
                    }

                });

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

