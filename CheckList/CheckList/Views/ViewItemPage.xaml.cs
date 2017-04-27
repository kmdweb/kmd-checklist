
using com.kmd.Models;
using com.kmd.ViewModels;

using Xamarin.Forms;

namespace com.kmd.Views {
    public partial class ViewItemPage : ContentPage {
        ItemDetailViewModel viewModel;
        bool IsEditMode;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ViewItemPage () {
            InitializeComponent ();

            Title = "Task details";
        }

        public ViewItemPage (ItemDetailViewModel viewModel) {
            InitializeComponent ();

            BindingContext = this.viewModel = viewModel;
        }

        async void DeleteItem_Clicked (object sender, System.EventArgs e) {
            var answer = await DisplayAlert ("Delete?", "You are about to delete this task. Are you sure?", "Yes", "No");
            if (answer) {
                MessagingCenter.Send (this, "DeleteItem", viewModel.Item);
                await Navigation.PopToRootAsync ();
            }
        }

        private async void EditItem_Clicked (object sender, System.EventArgs e) {
            await Navigation.PushAsync (new ItemDetailPage (viewModel.Item));
        }

        private async void SaveItem_Clicked (object sender, System.EventArgs e) {
            ToolbarItem x = ((ToolbarItem) sender);
            MessagingCenter.Send (this, "UpdateItem", viewModel.Item);
            await Navigation.PopToRootAsync ();
        }
    }
}
