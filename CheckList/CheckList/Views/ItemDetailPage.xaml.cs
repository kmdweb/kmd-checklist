
using com.kmd.ViewModels;

using Xamarin.Forms;

namespace com.kmd.Views {
    public partial class ItemDetailPage : ContentPage {
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage () {
            InitializeComponent ();

            Title = "Task details";
        }

        public ItemDetailPage (ItemDetailViewModel viewModel) {
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
    }
}
