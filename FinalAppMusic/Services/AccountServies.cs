using FinalAppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FinalAppMusic.Services
{
    public class AccountServices
    {
        private static string ApiBaseUri = "https://music-i-like.herokuapp.com";
        private static string ApiAccountPath = "/api/v1/accounts";
        private static string ApiLoginPath = "/api/v1/accounts/authentication";
        private static string ApiDataType = "application/json";

        public async Task<bool> Register(Account account)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUri);
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(account);
                    var contentToSend = new StringContent(jsonContent, Encoding.UTF8, ApiDataType);
                    var result = await httpClient.PostAsync(ApiAccountPath, contentToSend);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine(resultContent);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public async Task<bool> Login(string loginEmail, string loginPassword)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUri);
                    var loginInfomation = new
                    {
                        email = loginEmail,
                        password = loginPassword,
                    };
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(loginInfomation);
                    var contentToSend = new StringContent(jsonContent, Encoding.UTF8, ApiDataType);
                    var result = await httpClient.PostAsync(ApiLoginPath, contentToSend);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(resultContent);
                    Debug.WriteLine(resultContent);
                    Debug.WriteLine(token.access_token);
                    if(token.access_token != "")
                    {
                        Helper helper = new Helper();
                        helper.WriteToken(token.access_token);
                        return true;
                    }
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Title = "Notify";
                        dialog.Content = "Error Infomation!";
                        dialog.CloseButtonText = "OK";
                        await dialog.ShowAsync();
                        return false;
                    }
                   
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<Account> GetProfile(string accessToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUri);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var result = await httpClient.GetAsync(ApiAccountPath);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(resultContent);
                    Debug.WriteLine(account.firstName);
                    return account;
                }

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

    }
}
