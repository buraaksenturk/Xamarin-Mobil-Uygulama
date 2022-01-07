using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserData _userData = new UserData();
        public LoginPage()
        {
            InitializeComponent();
  
        }

        private async void girisbuton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = Mail.Text;
                string password = Sifre.Text;
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("UYARI!", "E-mail Alanı Boş Olamaz!", "TAMAM");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("UYARI!", "Şifre Alanı Boş Olamaz!", "TAMAM");
                    return;
                }

                string token = await _userData.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    Preferences.Set("userPassword", password);
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Başarısız Giriş!", "Giriş Yapılmadı, bilgileri kontrol edip tekrar deneyin.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("HATA!", "E-mail adresini yanlış girdiniz.", "Tamam");
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("HATA!", "Şifrenizi yanlış girdiniz.", "Tamam");
                }
                else if (ex.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                {
                    await DisplayAlert("HATA!", "Çok sayıda yanlış bilgi girdiniz!\nBir süre sonra tekrar deneyin.", "Tamam");
                }
                else
                {
                    await DisplayAlert("HATA!", ex.Message, "Tamam");
                }
            }

        }

        private void kayitbuton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignPage());
        }

        private void tanitimbutton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new tanitim());
        }
    }
}