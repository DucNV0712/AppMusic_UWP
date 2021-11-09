using FinalAppMusic.Entities;
using FinalAppMusic.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FinalAppMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        private AccountServices accountServices = new AccountServices();
        public ProfilePage()
        {
            this.InitializeComponent();
            Loaded += LoadedPage;
        }

        private async void LoadedPage(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile loginToken = await storageFolder.GetFileAsync("token.txt");
            string tokenResult = await FileIO.ReadTextAsync(loginToken);
 
            Account account = await accountServices.GetProfile(tokenResult);
            txtFirstName.Text = account.firstName;
            txtLaststName.Text = account.lastName;
            txtEmail.Text = account.email;
            txtPhone.Text = account.phone;
            txtAddress.Text = account.address;
            firstName.Text = account.firstName;
            lastName.Text = account.lastName;
           /* txtIntroduction.Text = account.introduction*//**//*;*/
            avataUrl.ImageSource = new BitmapImage(new Uri(account.avatar));


            Debug.WriteLine(account.firstName);
        }
    }
}
