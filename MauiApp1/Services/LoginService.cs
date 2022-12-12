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
        
        private string _baseUrl = "http://172.16.1.6:7270";

		private string _basePostResponse = "login succesfully";

		public async Task<LoginResposeModel> Addlogin(AddLoginRequest loginRequest)
		{
            //throw new NotImplementedException();

            var returnRespone = new LoginResposeModel();
            var text = "Please wait";

            try
            {
				using (var httpClient = new HttpClient())
				{
					string url = $"{_baseUrl}/local/logindetails";
                    url = "";

                    var serializeContent = JsonConvert.SerializeObject(loginRequest);
					var apiResponse = await httpClient
                        .PostAsync(url, new StringContent(serializeContent,Encoding.UTF8,"application/json"));
					if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var response = await apiResponse.Content.ReadAsStringAsync();
                        var  deserilizeResponse =
                            JsonConvert.DeserializeObject<LoginResposeModel>(response.ToString());
                        return deserilizeResponse;
                    }
					else
					{
						text = "Network Error!";
					}
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
    }
}
