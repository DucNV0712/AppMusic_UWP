using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Microsoft.Toolkit;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalAppMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalAppMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private AccountServices accountServices = new AccountServices();
        public static string AssessToken = "";
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtPassword.Password) )
            {
                if (txtEmail.Text.IsEmail())
                {
                    var result = await accountServices.Login(txtEmail.Text, txtPassword.Password.ToString());
                    if (result == true)
                    {
                        Frame.Navigate(typeof(ApplicationLayout));
                    }
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Title = "Notify";
                        dialog.Content = "Login Fail!";
                        dialog.CloseButtonText = "OK";
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.Title = "Notify";
                    dialog.Content = $"'{txtEmail.Text}' Not Email!";
                    dialog.CloseButtonText = "OK";
                    await dialog.ShowAsync();
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

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.RegisterPages));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ApplicationLayout));
        }
    }
}
