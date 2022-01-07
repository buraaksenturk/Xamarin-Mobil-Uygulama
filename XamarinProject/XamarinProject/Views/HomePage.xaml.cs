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
    public partial class HomePage : ContentPage
    {
        ProductFirebase productFirebase = new ProductFirebase();
        public HomePage()
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
        private async void searchbutonpressed(object sender, EventArgs e)
        {
            string searchValue = found.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var urunler = await productFirebase.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = urunler;
            }
            else
            {
                OnAppearing();
            }
        }
        private async void _search(object sender, TextChangedEventArgs e)
        {
            string searchValue = found.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var products = await productFirebase.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = products;
            }
            else
            {
                OnAppearing();
            }
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