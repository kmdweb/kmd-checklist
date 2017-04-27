﻿using System;

using com.kmd.Models;
using com.kmd.ViewModels;

using Xamarin.Forms;

namespace com.kmd.Views {
    public partial class ListItemsPage : ContentPage {
        ItemsViewModel viewModel;

        public ListItemsPage () {
            InitializeComponent ();

            BindingContext = viewModel = new ItemsViewModel ();
        }

        async void OnItemSelected (object sender, SelectedItemChangedEventArgs args) {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync (new ViewItemPage (new ItemDetailViewModel (item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked (object sender, EventArgs e) {
            await Navigation.PushAsync (new ItemDetailPage ());
        }

        protected override void OnAppearing () {
            base.OnAppearing ();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute (null);
        }
    }
}
