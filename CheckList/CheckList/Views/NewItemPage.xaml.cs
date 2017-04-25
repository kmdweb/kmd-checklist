using System;

using com.kmd.Models;

using Xamarin.Forms;

namespace com.kmd.Views {
    public partial class NewItemPage : ContentPage {
        public Item Item { get; set; }

        public NewItemPage () {
            InitializeComponent ();

            Item = new Item {
                Text = "Task name",
                Description = "Task description"
            };

            BindingContext = this;
        }

        async void Save_Clicked (object sender, EventArgs e) {
            MessagingCenter.Send (this, "AddItem", Item);
            await Navigation.PopToRootAsync ();
        }

        private void OnFocus_Description (object sender, FocusEventArgs e) {
            ((Editor) sender).Text = string.Empty;
        }

        private void OnFocus_Name (object sender, FocusEventArgs e) {
            ((Entry) sender).Text = string.Empty;
        }
    }
}