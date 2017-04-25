using CheckList.Utils;
using com.kmd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency (typeof (com.kmd.Services.MockDataStore))]
namespace com.kmd.Services {
    public class MockDataStore : IDataStore<Item> {
        bool isInitialized;
        List<Item> items;

        public async Task<bool> AddItemAsync (Item item) {
            await InitializeAsync ();

            items.Add (item);

            return await Task.FromResult (true);
        }

        public async Task<bool> UpdateItemAsync (Item item) {
            await InitializeAsync ();

            var _item = items.Where ((Item arg) => arg.Id == item.Id).FirstOrDefault ();
            items.Remove (_item);
            items.Add (item);

            return await Task.FromResult (true);
        }

        public async Task<bool> DeleteItemAsync (Item item) {
            await InitializeAsync ();

            var _item = items.Where ((Item arg) => arg.Id == item.Id).FirstOrDefault ();
            items.Remove (_item);

            return await Task.FromResult (true);
        }

        public async Task<Item> GetItemAsync (string id) {
            await InitializeAsync ();

            return await Task.FromResult (items.FirstOrDefault (s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync (bool forceRefresh = false) {
            await InitializeAsync ();

            return await Task.FromResult (items);
        }

        public Task<bool> PullLatestAsync () {
            try {
                string str = DependencyService.Get<IFileOperations> ().LoadText ("checklist-store") ?? string.Empty;
                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>> (str);
                if (dict != null) {
                    items = JsonConvert.DeserializeObject<List<Item>> (dict ["items"]?.ToString () ?? string.Empty);
                    if(items != null)
                        isInitialized = true;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine ("Pull-latest Error....: " + ex.ToString ());
            }
            return Task.FromResult (true);
        }


        public Task<bool> SyncAsync () {
            try {
                Dictionary<string, object> dict = new Dictionary<string, object> ();
                dict ["items"] = JsonConvert.SerializeObject (items);
                string data = JsonConvert.SerializeObject (dict);
                DependencyService.Get<IFileOperations> ().SaveText ("checklist-store", data);
            }
            catch(Exception ex) {
                Debug.WriteLine ("Sync-Async Error....: " + ex.ToString ());
            }
            return Task.FromResult (true);
        }

        public async Task InitializeAsync () {
            if (isInitialized)
                return;

            items = new List<Item> ();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            foreach (Item item in _items) {
                items.Add (item);
            }

            isInitialized = true;
        }
    }
}
