using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProject.Views
{
    public class ProductFirebase
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://xamarinprojectfirebase-default-rtdb.europe-west1.firebasedatabase.app/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("xamarinprojectfirebase.appspot.com");
        public async Task<bool> Save(ProductData product)
        {
            var data = await firebaseClient.Child(nameof(ProductData)).PostAsync(JsonConvert.SerializeObject(product));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProductData>>GetAll()
        {
            return (await firebaseClient.Child(nameof(ProductData)).OnceAsync<ProductData>()).Select(item => new ProductData
            {
                UrunAdi = item.Object.UrunAdi,
                MarketAdi = item.Object.MarketAdi,
                ucret = item.Object.ucret,
                Fotograf = item.Object.Fotograf,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<ProductData>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ProductData)).OnceAsync<ProductData>()).Select(item => new ProductData
            {
                UrunAdi = item.Object.UrunAdi,
                MarketAdi = item.Object.MarketAdi,
                Fotograf = item.Object.Fotograf,
                Tarih = item.Object.Tarih,
                Id = item.Key
            }).Where(c => c.UrunAdi.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<ProductData> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ProductData) + "/" + id).OnceSingleAsync<ProductData>());
        }
        public async Task<bool> Update(ProductData product)
        {
            await firebaseClient.Child(nameof(ProductData) + "/" + product.Id).PutAsync(JsonConvert.SerializeObject(product));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(ProductData) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }

        internal void GetById(string v, object id)
        {
            throw new NotImplementedException();
        }
    }
}
