using com.kmd.Models;
using com.kmd.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.kmd.Views {
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class ProgressPage : ContentPage {
        ItemsViewModel viewModel;
        public ProgressPage () {
            InitializeComponent ();
            Title = "My Tasks";
            BindingContext = viewModel = new ItemsViewModel ();
        }

        async void AddItem_Clicked (object sender, EventArgs e) {
            await Navigation.PushAsync (new NewItemPage ());
        }

        async void OnItemSelected (object sender, SelectedItemChangedEventArgs args) {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync (new ItemDetailPage (new ItemDetailViewModel (item)));

            // Manually deselect item
            ProgressListView.SelectedItem = null;
        }

        protected override void OnAppearing () {
            base.OnAppearing ();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute (null);
        }

        private void Cell_Appearing (object sender, EventArgs e) {
            var viewCell = (ViewCell) sender;
            //var children = viewCell.View.Children;
            var y = e;
        }


        private void OnFinishTapped (object sender, EventArgs e) {
            var fimage = ((Image) sender);
            var item = fimage.Source.BindingContext as Item;
            var row = fimage.Parent.Parent;
            var statusImg = row.FindByName<Image> ("StatusImg");
            var totalTime = row.FindByName<Label> ("TotalTime");

            fimage.IsVisible = false;

            item.Status = Item.ItemStatus.Completed;
            statusImg.Source = item.StatusIcon;

            totalTime.Text = item.VTotalTime;
            totalTime.IsVisible = true;
        }

        private void OnStatusImgTapped (object sender, EventArgs e) {
            var image = ((Image) sender);
            var item = image.Source.BindingContext as Item;
            switch (item.Status) {
                case Item.ItemStatus.Pending:
                    item.Status = Item.ItemStatus.Processing;
                    break;
                case Item.ItemStatus.Processing:
                    item.Status = Item.ItemStatus.Pending;
                    break;
                default:
                    return;
            }
            image.Source = item.StatusIcon;
        }
    }
}
