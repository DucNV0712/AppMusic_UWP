using FinalAppMusic.Services;
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
using FinalAppMusic.Entities;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalAppMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPages : Page
    {
        private AccountServices services = new AccountServices();
        private int genderChoose = 1;
        public RegisterPages()
        {
            this.InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton gender = sender as RadioButton;
            switch (gender.Tag)
            {
                case "male":
                    genderChoose = 1;
                    break;
                case "female":
                    genderChoose = 0;
                    break;
            }
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var firstName = txt_firstname.Text;
            var lastName = txt_lastname.Text;
            var password = txt_passworld.Password.ToString();
            var confirmPassword = txt_confirmPassword.Password.ToString();
            var address = txt_address.Text;
            var phone = txt_phone.Text;
            var email = txt_email.Text;
            var avatar = txt_avt.Text;
            var introduction = txt_introduct.Text;

            if (!string.IsNullOrEmpty(firstName)
                && !string.IsNullOrEmpty(lastName)
                && !string.IsNullOrEmpty(password)
                && !string.IsNullOrEmpty(confirmPassword)
                && !string.IsNullOrEmpty(address)
                && !string.IsNullOrEmpty(phone)
                && !string.IsNullOrEmpty(email)
                && !string.IsNullOrEmpty(avatar)
                && !string.IsNullOrEmpty(introduction))
            {
                if (email.IsEmail()){
                    if(password == confirmPassword)
                    {
                        var account = new Account
                        {
                            firstName = firstName,
                            lastName = lastName,
                            password = password,
                            address = address,
                            phone = phone,
                            email = email,
                            avatar = avatar,
                            gender = genderChoose,
                            introduction = introduction,
                            birthday = pickerBirthday.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        };
                        Debug.WriteLine(account);
                        var result = await services.Register(account);

                        if (result == true)
                        {
                            ContentDialog dialogSussess = new ContentDialog();
                            dialogSussess.Title = "Notify";
                            dialogSussess.Content = "Create Account Success";
                            dialogSussess.CloseButtonText = "OK";
                            await dialogSussess.ShowAsync();
                            Frame.Navigate(typeof(LoginPage));
                        }
                        else
                        {
                            ContentDialog dialogError = new ContentDialog();
                            dialogError.Title = "Notify";
                            dialogError.Content = "Error!";
                            dialogError.CloseButtonText = "OK";
                            await dialogError.ShowAsync();
                        }
                    }
                    else {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Title = "Notify";
                        dialog.Content = "Password not match.";
                        dialog.CloseButtonText = "OK";
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.Title = "Notify";
                    dialog.Content = $"'{email}' Not Email!";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ApplicationLayout));
        }
    }
}
