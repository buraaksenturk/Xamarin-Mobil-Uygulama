using System;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Storage;
namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
       
        UserData _userData = new UserData();
        public ProfilePage()
        {
            InitializeComponent();
            UserLabel.Text = Preferences.Get("userEmail", "default");
            UserPass.Text = Preferences.Get("userPassword", "******");
            foto.Source = Preferences.Get("images.jpeg", "profilbuton2");


        }


        private void HomePageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
        private void ProductFollowPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductFollowPage());
        }
        private void ProductAddPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductAddPage());
        }
        private async void ProfilePageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void changebuton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RefreshPassword());
        }

        [Obsolete]
        private async void deletebutton_Clicked(object sender, EventArgs e)
        {

            string a = Preferences.Get("token", "");
            bool sil = await _userData.Delete(a);
            if (sil)
            {
                await DisplayAlert("Bildirim", "Kişi Silindi", "Tamam");

                await Navigation.PushAsync(new LoginPage());
            }

        }
    }
}