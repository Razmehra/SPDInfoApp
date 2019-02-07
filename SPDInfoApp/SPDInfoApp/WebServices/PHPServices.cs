using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<string> Submit(string[] Params)
        {
            var dic4 = new Dictionary<string, string>
                {
                   {"AppearingClass", Params[0] },
                   {"ApplicationID", Params[1] },
                   {"StudentName", Params[2] },
                   {"RollNo", Params[3] },
                   {"EnrolmentNo", Params[4] },
                   {"DOB", Params[5] },
                   {"Medium", Params[6] },
                   {"Gender", Params[7] },
                   {"Category", Params[8] },
                   {"RegCastCertificate", Params[9] },
                   {"IsHandicapped", Params[10] },
                   {"HandicappDetail", Params[11] },
                   {"BloodGroup", Params[12] },
                   {"PhoneMobile", Params[13] },
                   {"SSSMId", Params[14] },
                   {"AadharNo", Params[15] },
                   {"EMail", Params[16] },
                   {"AddressPermanent", Params[17] },
                   {"AddressCurrent", Params[18] },
                   {"IsUrban", Params[19] },
                   {"NativePlace", Params[20] },
                   {"RegNativeCertificateNo", Params[21] },
                   {"FHName", Params[22] },
                   {"MotherName", Params[23] },
                   {"PhoneMobile_Gaurdian", Params[24] },
                   {"IncomeFather", Params[25] },
                   {"OccupationFather", Params[26] },
                   {"BankAcNo", Params[27] },
                   {"BankIFSC", Params[28] },
                   {"BankName", Params[29] },
                   {"BankBranch", Params[30] },
                   {"VoterID", Params[31] },
                   {"PANNo", Params[32] },
                   {"DrivingLicNo", Params[33] },
                   {"ScholershipName", Params[34] }
                };

            return await Result("POST", "insertSPDInfoRecords.php", null, null, dic4);
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

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Content = new FormUrlEncodedContent(Dic);
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
