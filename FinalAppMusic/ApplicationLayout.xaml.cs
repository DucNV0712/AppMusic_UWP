
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalAppMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ApplicationLayout : Page
    {
        private AccountServices accountServices = new AccountServices();
        public ApplicationLayout()
        {
            this.InitializeComponent();
            Loaded += LoadedPage;


        }
        private async void LoadedPage(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.GetFileAsync("token.txt");
            string tokenResult = await FileIO.ReadTextAsync(loginToken);

            if(tokenResult != "")
            {
                Account.Visibility = Visibility.Visible;
                authBtn.Visibility = Visibility.Collapsed;
                CreateMySong.Visibility = Visibility.Visible;
                MyListSong.Visibility = Visibility.Visible;

            }
            else
            {
                CreateMySong.Visibility = Visibility.Collapsed;
                MyListSong.Visibility = Visibility.Collapsed;
                Account.Visibility = Visibility.Collapsed;
                authBtn.Visibility = Visibility.Visible;
            }

            Account account = await accountServices.GetProfile(tokenResult);
         
            /* txtIntroduction.Text = account.introduction*//**//*;*/
            if(!string.IsNullOrEmpty(tokenResult))
            {
                avataUrl.ImageSource = new BitmapImage(new Uri(account.avatar));
            }
            fullName.Text = $"{account.firstName} {account.lastName}";

        }
        private void MyNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            MyNavView.IsBackEnabled = true;
            MyNavView.IsBackButtonVisible = NavigationViewBackButtonVisible.Visible;
            var tag = args.InvokedItemContainer.Tag.ToString();
            switch (tag)
            {
                case "home":
                    ContentPage.Navigate(typeof(Pages.DashboardPage), args.RecommendedNavigationTransitionInfo);
                    break;
                case "createSong":
                    ContentPage.Navigate(typeof(Pages.CreateSongPage), args.RecommendedNavigationTransitionInfo);
                    break;
                case "listSong":
                    ContentPage.Navigate(typeof(Pages.ListSongPage), args.RecommendedNavigationTransitionInfo);
                    break;
                case "myListSong":
                    ContentPage.Navigate(typeof(Pages.MyListSong), args.RecommendedNavigationTransitionInfo);
                    break;
                case "createMySong":
                    ContentPage.Navigate(typeof(Pages.CreateMySong), args.RecommendedNavigationTransitionInfo);
                    break;
                case "profile":
                    ContentPage.Navigate(typeof(Pages.ProfilePage), args.RecommendedNavigationTransitionInfo);
                    break;
            }
        }

        private void MyNavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentPage.CanGoBack)
            {
                ContentPage.GoBack();
            }
        }

    

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.LoginPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.RegisterPages));
        }

        private void MenuFlyoutItem_Profile(object sender, RoutedEventArgs e)
        {
            ContentPage.Navigate(typeof(Pages.ProfilePage));
        }

        private void MenuFlyoutItem_Setting(object sender, RoutedEventArgs e)
        {
            ContentPage.Navigate(typeof(Pages.SettingPage));
        }

        private void MenuFlyoutItem_Logout(object sender, RoutedEventArgs e)
        {

        }
    }
}

