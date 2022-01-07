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
using Plugin.Media;

using Plugin.Media.Abstractions;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductAddPage : ContentPage
    {

        MediaFile file;
        ProductFirebase firebase = new ProductFirebase();
        
        public ProductAddPage()
        {
            InitializeComponent();
        }


        private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                foto.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void eklebuton_Clicked(object sender, EventArgs e)
        {
            ProductData product = new ProductData();
            string name = Product.Text;
            string market = Market.Text;
            string sale = fiyat.Text;
            DateTime Date = DateTime.Now;
            Date.ToString("MM/dd/yyyy H:mm");

            if (file != null)
            {
                string image = await firebase.Upload(file.GetStream(), Path.GetFileName(file.Path));
                product.Fotograf = image;
            }

            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("UYARI", "İsim Bilgisi Girilmedi", "TAMAM");
            }
            if (string.IsNullOrEmpty(market))
            {
                await DisplayAlert("UYARI", "Market Bilgisi Girilmedi", "TAMAM");
            }
            if (string.IsNullOrEmpty(sale))
            {
                await DisplayAlert("UYARI", "Fiyat Bilgisi Girilmedi", "TAMAM");
            }
             
            
            product.UrunAdi = name;
            product.MarketAdi = market;
            product.ucret = sale;
            product.Tarih = Date;

            var isSaved = await firebase.Save(product);
            if (isSaved)
            {
                
                await DisplayAlert("Bilgilendirme", "Ürün eklendi!", "TAMAM");
                Clear();
            } 
            else
            {
                await DisplayAlert("Hata", "Ürün Eklenmedi!", "TAMAM");
            }
        }

        public void Clear()
        {
            Product.Text = string.Empty;
            Market.Text = string.Empty;
            fiyat.Text = string.Empty;
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
        private void ProfilePageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        

       
    }
}