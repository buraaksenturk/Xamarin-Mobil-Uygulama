using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductFollowPage : ContentPage
    {
        ProductFirebase productFirebase = new ProductFirebase();
        public ProductFollowPage()
        {
            InitializeComponent();

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
    {
        var products = await productFirebase.GetAll();
        ProductListView.ItemsSource = null;
        ProductListView.ItemsSource = products;
        ProductListView.IsRefreshing = false;
    }

    private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var prod = e.Item as ProductData;
            Navigation.PushAsync(new ProductDetails(prod));
            ((ListView)sender).SelectedItem = null;
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