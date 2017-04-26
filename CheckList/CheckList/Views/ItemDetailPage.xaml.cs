
using com.kmd.Models;
using com.kmd.ViewModels;

using Xamarin.Forms;

namespace com.kmd.Views {
    public partial class ItemDetailPage : ContentPage {
        ItemDetailViewModel viewModel;
        bool IsEditMode;

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

        private void EditItem_Clicked (object sender, System.EventArgs e) {
            ToolbarItem x = ((ToolbarItem) sender);
            ContentPage page = ((ToolbarItem) sender).Parent as ContentPage;
            x.Icon = "check_g.png";
            x.Clicked -= EditItem_Clicked;
            x.Clicked += SaveItem_Clicked;

            Entry en = page.FindByName<Entry> ("EText");
            Editor ed = page.FindByName<Editor> ("EDesc");
            en.IsVisible = true;
            ed.IsVisible = true;
            Label le = page.FindByName<Label> ("LText");
            Label led = page.FindByName<Label> ("LDesc");
            le.IsVisible = false;
            led.IsVisible = false;


        }

        private async void SaveItem_Clicked (object sender, System.EventArgs e) {
            ToolbarItem x = ((ToolbarItem) sender);
            MessagingCenter.Send (this, "UpdateItem", viewModel.Item);
            await Navigation.PopToRootAsync ();
        }
    }
}
