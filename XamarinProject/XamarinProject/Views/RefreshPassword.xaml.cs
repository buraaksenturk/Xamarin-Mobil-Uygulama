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
    public partial class RefreshPassword : ContentPage
    {
        UserData _data = new UserData();
        public RefreshPassword()
        {
            InitializeComponent();
        }

        private async void değistirbuton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string password = yenisifre.Text;
                string confirmPassword = yeniSifre2.Text;
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Şifre Değişikliği!", "Yeni bir şifre giriniz.", "Tamam");
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Şifre Değişikliği!", "Yeni şifreyi tekrar giriniz.", "Tamam");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Şifre Değişikliği!", "Şifreler eşleşmiyor!", "Tamam");
                    return;
                }
                string token = Preferences.Get("token", "");
                bool isChanged = await _data.RefreshPassword(token, password);
                if (isChanged)
                {
                    await DisplayAlert("Şifre Değişikliği", "Şifre Güncellendi!", "Tamam");
                }
                else
                {
                    await DisplayAlert("Şifre Değişikliği", "Şifre Güncellenemedi!", "Tamam");
                }
            }
            catch (Exception ex)

            {

            }
        }
    }
}