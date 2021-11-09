using FinalAppMusic.Entities;
using FinalAppMusic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalAppMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateMySong : Page
    {
        private SongServices songServices = new SongServices();
        private int statusSong = 1;
        public CreateMySong()
        {
            this.InitializeComponent();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton choose = new RadioButton();

            switch (choose.Tag)
            {
                case "public":
                    statusSong = 0;
                    break;
                case "private":
                    statusSong = 1;
                    break;
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.GetFileAsync("token.txt");
            string tokenResult = await FileIO.ReadTextAsync(loginToken);
            if (!string.IsNullOrEmpty(txtThumbnail.Text)
                && !string.IsNullOrEmpty(txtNameSong.Text)
                && !string.IsNullOrEmpty(txtSinger.Text)
                && !string.IsNullOrEmpty(txtAuthor.Text)
                && !string.IsNullOrEmpty(txtUrlSong.Text)
                && !string.IsNullOrEmpty(txtDesc.Text))
            {
                var song = new Song
                {
                    thumbnail = txtThumbnail.Text,
                    name = txtNameSong.Text,
                    singer = txtSinger.Text,
                    author = txtAuthor.Text,
                    link = txtUrlSong.Text,
                    description = txtDesc.Text,
                    status = statusSong
                };
                var result = await songServices.MySongSave(song,tokenResult);
                if (result == true)
                {
                    ContentDialog dialogSussess = new ContentDialog();
                    dialogSussess.Title = "Notify";
                    dialogSussess.Content = "Create Song Success";
                    dialogSussess.CloseButtonText = "OK";
                    await dialogSussess.ShowAsync();
                    Frame.Navigate(typeof(Pages.ListSongPage));
                }
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.Title = "Notify";
                dialog.Content = "Please enter full information!";
                dialog.CloseButtonText = "OK";
                await dialog.ShowAsync();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtThumbnail.Text = "";
            txtNameSong.Text = "";
            txtSinger.Text = "";
            txtAuthor.Text = "";
            txtDesc.Text = "";
            txtUrlSong.Text = "";
        }
    }
}

