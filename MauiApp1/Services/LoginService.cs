using MauiApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp1.Services
{
    public class LoginService : ILoginService
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        private string _baseUrl = "http://172.16.1.114:7037";

		public async Task<AddLoginRequest> Addlogin(LoginResposeModel loginRequest)
		{
            //throw new NotImplementedException();

            var returnRespone = new AddLoginRequest();
            var text = "Please wait";

            try
            {
				using (var httpClient = new HttpClient())
				{
                    string responseString = "";

                    string url = $"{_baseUrl}/local/logindetails";

                    //added
                    HttpResponseMessage response = new();
                    response.EnsureSuccessStatusCode();
                    HttpRequestMessage request = new HttpRequestMessage();
                    string strJSON1 = JsonConvert.SerializeObject(loginRequest);
                    HttpMethod method = new HttpMethod("POST");

                    if (strJSON1.Length > 0)
                    {
                        string ostr = strJSON1.Remove(strJSON1.Length - 1, 1);
                        ostr = ostr.Remove(0, 1);
                        strJSON1 = ostr;
                        //strJSON1 = strJSON1.Replace("jsonurlcatagory", "jsonurl");

                        var content = new StringContent(strJSON1, Encoding.UTF8, "application/json");
                        var response2 = await httpClient.PostAsync(url, content); //client.PostAsync


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            responseString = await response2.Content.ReadAsStringAsync();

                            returnRespone.message = responseString;


                        }
                        else
                        {

                            var ReturnString = "ERROR on fetching report requests. upload failed; " + Environment.NewLine + " response StatusCode is : " + response2.StatusCode.ToString() + Environment.NewLine;
                            returnRespone.message = ReturnString;

                        }
                    }
                    return returnRespone;

                    //var serializeContent = JsonConvert.SerializeObject(loginRequest);
                    //               var apiResponse = await httpClient
                    //                   .PostAsync(url, new StringContent(serializeContent,Encoding.UTF8,"application/json"));
                    //if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    //{
                    //	var respons2e = await apiResponse.Content.ReadAsStringAsync();
                    //                   var  deserilizeResponse =
                    //                       JsonConvert.DeserializeObject<LoginResposeModel>(respons2e.ToString());
                    //                   return deserilizeResponse;
                    //               }
                    //else
                    //{
                    //	text = "Network Error!";
                    //}
                }
			}
			catch (Exception ex)
			{
				string msg = ex.Message;
			}
			return returnRespone;
		}

		public async Task<List<LoginResposeModel>> GetAllLoginList()
        {
            var text = "Please wait";
            var returnRespone = new List<LoginResposeModel>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string url = $"{_baseUrl}/local/logindetails";
                    var apiResponse = await httpClient.GetAsync(url);
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        returnRespone = 
                            JsonConvert.DeserializeObject<List<LoginResposeModel>>(response.ToString());
                    } else
                    {
                        text = "Network Error!";
                    }
                }
            } catch(Exception ex)
            {
                string msg = ex.Message;
            }
            return returnRespone;
        }

        //Task<LoginResposeModel> ILoginService.Addlogin(AddLoginRequest loginRequest)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
