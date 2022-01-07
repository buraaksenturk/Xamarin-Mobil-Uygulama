using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.Views;

namespace XamarinProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {

        public ProductDetails(ProductData product)
        {
            InitializeComponent();

            resim.Source = product.Fotograf;
            LabelAd.Text = product.UrunAdi;
            LabelMarket.Text = product.MarketAdi;
            LabelFiyat.Text = product.ucret;
            DateTime Date = product.Tarih;
            LabelTarih.Text = Date.ToString("MM/dd/yyyy H:mm");

        }

        private void geributon_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}