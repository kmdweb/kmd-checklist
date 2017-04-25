using com.kmd.Models;
using com.kmd.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            var image = ((Image) sender);
            var item = image.Source.BindingContext as Item;
            var row = image.Parent.Parent;
            var statusImg = row.FindByName<Image> ("StatusImg");
            var totalTime = row.FindByName<Label> ("TotalTime");

            item.Status = Item.ItemStatus.Completed;
            statusImg.IsVisible = false;
            totalTime.Text = item.VTotalTime;
            totalTime.IsVisible = true;
            image.Source = item.StatusIcon;
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

        #region Refresh items on any action
        
        //private void OnFinishTapped (object sender, EventArgs e) {
        //    var image = ((Image) sender);
        //    var item = image.Source.BindingContext as Item;
        //    if (item.Status != Item.ItemStatus.Completed) {
        //        item.Status = Item.ItemStatus.Completed;
        //        viewModel.LoadItemsCommand.Execute (this);
        //    }
        //}

        //private void OnStatusImgTapped (object sender, EventArgs e) {
        //    var image = ((Image) sender);
        //    var item = image.Source.BindingContext as Item;
        //    switch (item.Status) {
        //        case Item.ItemStatus.Pending:
        //            item.Status = Item.ItemStatus.InProgress;
        //            break;
        //        case Item.ItemStatus.InProgress:
        //            item.Status = Item.ItemStatus.Pending;
        //            break;
        //        default:
        //            return;
        //    }
        //    viewModel.LoadItemsCommand.Execute (this);
        //}

        #endregion

    }


    #region progresss view model - auto generated

    //class ProgressViewModel : INotifyPropertyChanged {
    //    public ObservableCollection<Item> Items { get; }
    //    public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; }

    //    public ProgressViewModel () {
    //        Items = new ObservableCollection<Item> (new []
    //        {
    //            new Item { Text = "Baboon", Detail = "Africa & Asia" },
    //            new Item { Text = "Capuchin Monkey", Detail = "Central & South America" },
    //            new Item { Text = "Blue Monkey", Detail = "Central & East Africa" },
    //            new Item { Text = "Squirrel Monkey", Detail = "Central & South America" },
    //            new Item { Text = "Golden Lion Tamarin", Detail= "Brazil" },
    //            new Item { Text = "Howler Monkey", Detail = "South America" },
    //            new Item { Text = "Japanese Macaque", Detail = "Japan" },
    //        });

    //        var sorted = from item in Items
    //                     orderby item.Text
    //                     group item by item.Text [0].ToString () into itemGroup
    //                     select new Grouping<string, Item> (itemGroup.Key, itemGroup);

    //        ItemsGrouped = new ObservableCollection<Grouping<string, Item>> (sorted);

    //        RefreshDataCommand = new Command (
    //            async () => await RefreshData ());
    //    }

    //    public ICommand RefreshDataCommand { get; }

    //    async Task RefreshData () {
    //        IsBusy = true;
    //        //Load Data Here
    //        await Task.Delay (2000);

    //        IsBusy = false;
    //    }

    //    bool busy;
    //    public bool IsBusy {
    //        get { return busy; }
    //        set {
    //            busy = value;
    //            OnPropertyChanged ();
    //            ((Command) RefreshDataCommand).ChangeCanExecute ();
    //        }
    //    }


    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void OnPropertyChanged ([CallerMemberName]string propertyName = "") =>
    //        PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));

    //    public class Item {
    //        public string Text { get; set; }
    //        public string Detail { get; set; }

    //        public override string ToString () => Text;
    //    }

    //    public class Grouping<K, T> : ObservableCollection<T> {
    //        public K Key { get; private set; }

    //        public Grouping (K key, IEnumerable<T> items) {
    //            Key = key;
    //            foreach (var item in items)
    //                this.Items.Add (item);
    //        }
    //    }
    //}
    
    #endregion
}
