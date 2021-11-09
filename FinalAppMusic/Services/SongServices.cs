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
    public class SongServices
    {
        private static string ApiBaseUrl = "https://music-i-like.herokuapp.com";
        private static string ApiSongPath = "/api/v1/songs";
        private static string ApiMySongPath = "/api/v1/songs/mine";
        private static string ApiDataType = "application/json";


        public async Task<bool> Save(Song song)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUrl);
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(song);
                    var contentToSend = new StringContent(jsonContent, Encoding.UTF8, ApiDataType);
                    var result = await httpClient.PostAsync(ApiSongPath, contentToSend);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> MySongSave(Song song, string accessToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUrl);
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(song);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var contentToSend = new StringContent(jsonContent, Encoding.UTF8, ApiDataType);
                    var result = await httpClient.PostAsync(ApiMySongPath, contentToSend);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<List<Song>> GetAll()
        {
            List<Song> list = new List<Song>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUrl);
                    var result = await httpClient.GetAsync(ApiSongPath);
                    var resultContent = await result.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(resultContent);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return list;
        }
        public async Task<List<Song>> GetMySong(string accessToken)
        {
            List<Song> list = new List<Song>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(ApiBaseUrl);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var result = await httpClient.GetAsync(ApiMySongPath);
                    var resultContent = await result.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(resultContent);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return list;
        }

    }
}
