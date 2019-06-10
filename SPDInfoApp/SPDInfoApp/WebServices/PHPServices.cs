using Newtonsoft.Json.Linq;
using SPDInfoApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace SPDInfoApp.WebServices
{
    public class PHPServices
    {

        public async Task<string> Login(string[] Params)
        {
            var jsonRequest = new { username = Params[0], pwd = Params[1] };
            string LoginParams = "username = " + Params[0] + " & password = " + Params[1];
            var dic4 = new Dictionary<string, string>
                {
                   {"username", Params[0] },
                   {"pwd", Params[1] },
                };

            return await Result("POST", "login.php", jsonRequest, Params, dic4);
        }

        public async Task<string> SubmitStudentFeedback(JsonStudentFeedback Params)
        {

            //PropertyInfo[] infos = Params.GetType().GetProperties();

            Dictionary<string, string> dix = new Dictionary<string, string>();

            dix = PrepareDictionary(Params);
            return await Result("POST", "SPDInfoFeedbaks_STD.php", null, null, dix);
        }

        private Dictionary<string, string> PrepareDictionary(object Params)
        {
            PropertyInfo[] infos = Params.GetType().GetProperties();

            Dictionary<string, string> dix = new Dictionary<string, string>();

            foreach (PropertyInfo info in infos)
            {
                var xx = info.GetValue(Params, null);
                if (info.Name != "Appid")
                {

                    bool isDateType = info.PropertyType.ToString().Equals("System.DateTime");
                    if (isDateType)
                    {
                        string dtVal = ((DateTime)xx).ToString("yyyy/MM/dd HH:mm:ss");

                        dix.Add(info.Name, xx == null ? null : dtVal);
                    }
                    else
                    {
                        dix.Add(info.Name, xx?.ToString());
                    }

                }
            }
            return dix;
        }

        public async Task<string> SubmitStudentInfo(SPDInfo Params)
        {

            PropertyInfo[] infos = Params.GetType().GetProperties();

            Dictionary<string, string> dix = new Dictionary<string, string>();

            foreach (PropertyInfo info in infos)
            {
                var xx = info.GetValue(Params, null);
                if (info.Name != "Appid")
                {

                    bool isDateType = info.PropertyType.ToString().Equals("System.DateTime");
                    if (isDateType)
                    {
                        string DateFormate = (info.Name == "EntryDate" ? "yyyy/MM/dd hh:mm:ss tt" : "yyyy/MM/dd");
                        string dtVal = ((DateTime)xx).ToString(DateFormate);

                        dix.Add(info.Name, xx == null ? null : dtVal);
                    }
                    else
                    {

                        bool isBoolType = info.PropertyType.ToString().Contains("Bool");

                        if (isBoolType)
                        {
                            dix.Add(info.Name, ((bool)xx ? 1 : 0).ToString());
                        }
                        else
                        {
                            dix.Add(info.Name, (xx ?? "").ToString());
                        }
                    }

                }
            }

            return await Result("POST", "insertSPDInfoRecords.php", null, null, dix);
        }

        public async Task<string> FetchStudentInfo(string[] Params)
        {

            var dic4 = new Dictionary<string, string>
                {
                   {"ApplicationID", Params[0] }
                };


            return await Result("POST", "fetchSPDInfoData.php", null, null, dic4);
        }


        public async Task<string> FetchStudentFeedback(string[] Params)
        {

            var jsonRequest = "";// new { ApplicationID = Params[0] };
            string LoginParams = "";// "ApplicationID = " + Params[0] ;
            var dic4 = new Dictionary<string, string>();
            //{
            //   {"ApplicationID", Params[0] }
            //};
            dic4.Add("ApplicationID", Params[0].ToString());
            // dic4 = PrepareDictionary(Params);
            return await Result("POST", "fetchFeedbaks_STD.php", jsonRequest, Params, dic4);

        }

        public async Task<string> SendExcel2Mail(string[] Params)
        {

            var dic4 = new Dictionary<string, string>
                {
                   {"Email", Params[0] }
                };


            return await Result("POST", "CreateExcel2Mail.php", null, null, dic4);
        }


        public async Task<string> DbUpdate(string[] Params)
        {
            var jsonRequest = new { TOWN = Params[0], WARDNO = Params[1], GISID = Params[2] };
            //    string LoginParams = "TOWN = " + Params[0] + " &WARDNO = " + Params[1] + " &GISID = " + Params[2];
            var dic4 = new Dictionary<string, string>
                {
                   {"USERNAME", Params[0] },
                   {"STATUS", Params[1] },
                   {"TOWN", Params[2] },
                   {"WARDNO", Params[3] },
                   {"GISID", Params[4] },
                   {"image1Name", Params[5] },
                   {"image2Name", Params[6] },
                   {"CAPTUREDATE", Params[7] }

                };

            return await Result("POST", "dbUpdate.php", jsonRequest, Params, dic4);
        }

        private async Task<string> Result(string ServiceType, string XUri, Object jsonRequest = null, string[] Param = null, Dictionary<string, string> Dic = null)
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet) { return "Network Error:No Internet connection available.\n !Turn ON your data connection or connect using wifi then try again."; }


            HttpClient client = new HttpClient();

            try
            {
                string BaseUrl = "http://geoinfotech.org.in/spdinfoservices/";
                client.BaseAddress = new Uri(BaseUrl + XUri);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, */*");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.8,sv-SE;q=0.5,sv;q=0.3");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Content = new FormUrlEncodedContent(Dic)
                };
                var value4 = new FormUrlEncodedContent(Dic);
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress.ToString(), value4);

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    //    var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    return result.ToString();
                }
                else
                {
                    return response.IsSuccessStatusCode.ToString();
                }
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message;
            }
            finally
            {
                client.Dispose();

            }

        }



    }
}
