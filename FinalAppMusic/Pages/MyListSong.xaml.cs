using FinalAppMusic.Entities;
using FinalAppMusic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalAppMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyListSong : Page
    {
        private SongServices songServices = new SongServices();
        public MyListSong()
        {
            this.InitializeComponent();
            this.Loaded += LoadPage;

        }
        private async void LoadPage(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.GetFileAsync("token.txt");
            string tokenResult = await FileIO.ReadTextAsync(loginToken);
            List<Song> list = await songServices.GetMySong(tokenResult);
            ListSong.ItemsSource = list;
        }
        private async void ListSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            ApplicationLayout layout = rootFrame.Content as ApplicationLayout;
            var nameSong = layout.FindName("nameSong") as TextBlock;
            var nameSinger = layout.FindName("nameSinger") as TextBlock;
            var nowPlaying = layout.FindName("nowPlaying") as MediaPlayerElement;
            var thumbnail = layout.FindName("Thumbnail") as Image;
            var song = ListSong.SelectedItem as Song;
            nameSong.Text = song.name;
            nameSinger.Text = song.singer;
            thumbnail.Source = new BitmapImage(new Uri(song.thumbnail));
            nowPlaying.Source = MediaSource.CreateFromUri(new Uri(song.link));
            /*            nowPlaying.PosterSource = new BitmapImage(new Uri(song.thumbnail));
            */
            nowPlaying.MediaPlayer.Play();
        }
    }
}

