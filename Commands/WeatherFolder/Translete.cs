using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Translator
{
    class YandexTranslator
    {
        public string Translate(string s)
        {
            if (s.Length > 0)
            {
                string url = "https://translate.yandex.net/api/v1.5/tr.json/translate?"
                    + "key=trnsl.1.1.20180725T165031Z.6d6482df165e457c.04e5062f48f8149a9b20650a6ef9fee4e68c3158"
                    + "&text=" + s
                    + "&lang=en-ru";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                Translation translation = JsonConvert.DeserializeObject<Translation>(response);

                return translation.text[0];
            }
            else {
                return "";
            }
        }

        class Translation
        {
            public string code { get; set; }
            public string lang { get; set; }
            public string[] text { get; set; }
        }
    }
}