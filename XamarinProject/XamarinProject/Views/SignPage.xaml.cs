using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase;
using Firebase.Storage;
using System.IO;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignPage : ContentPage
    {
        
        UserData _userData = new UserData();
        public SignPage()
        {
            InitializeComponent();
        }

        async void imageaddbutton(System.Object sender, System.EventArgs e)
        {
            var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            
            var task = new FirebaseStorage("xamarinprojectfirebase.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child(Preferences.Get("userEmail", "isimsiz"))
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

            task.Progress.ProgressChanged += (s, args) =>
            {
                progressBar.Progress = args.Percentage;
            };       
        }


        private async void SignButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                string email = Mail.Text;
                string password = Sifre.Text;
                string confirmPassword = SifreTekrar.Text;

                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("UYARI!", "Eksik Veya Boş Alan Bıraktınız !", "TAMAM");
                    return;
                }
                
                if (password != confirmPassword)
                {
                    await DisplayAlert("UYARI!", "Şifre ve Şifre Tekrarı Eşleşmiyor\nLütfen Dikkat Deneyiniz.", "TAMAM");
                    return;
                }

                bool isSave = await _userData.Register(email, password);
                if (isSave)
                {
                    await DisplayAlert("Yeni Kullanıcı!", "Tebrikler! Kayıt Oldunuz", "TAMAM");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Başarısız", "Kayıt İşleminiz Başarısız", "TAMAM");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("UYARI!", "E-mail adresi zaten kullanılıyor!\nFarklı mail adresi deneyiniz.", "Tamam");
                }
                else
                {
                    await DisplayAlert("HATA!", ex.Message, "Tamam");
                }
            }

        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }

        

}