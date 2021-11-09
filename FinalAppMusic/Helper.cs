using FinalAppMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FinalAppMusic
{
    public class Helper
    {
        public async void WriteToken(string token)
        {
            // Create sample file; replace if exists.
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.CreateFileAsync("token.txt",CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(loginToken,token);
        }
        public async void ReadToken(Token token)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.GetFileAsync("token.txt");
            string tokenResult = await FileIO.ReadTextAsync(loginToken);
            token.access_token = tokenResult;

        }
    }
}
