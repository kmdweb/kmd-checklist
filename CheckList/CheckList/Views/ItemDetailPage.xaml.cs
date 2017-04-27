using System;

using com.kmd.Models;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace com.kmd.Views {
    public partial class ItemDetailPage : ContentPage {
        public Item Item { get; set; }
        CustomEditor el;
        bool EditMode;

        public ItemDetailPage () {
            InitializeComponent ();

            Item = new Item {
                Text = "Task name",
                Description = "Task description"
            };

            StackLayout sl = this.FindByName<StackLayout> ("EditorBox");
            el = new CustomEditor () {
                Keyboard = Keyboard.Create (KeyboardFlags.All),
                VerticalOptions = new LayoutOptions () { Alignment = LayoutAlignment.Fill, Expands = true },
            };
            el.TextChanged += Desc_Changed;
            el.Focused += OnFocus_Description;
            el.SetBinding (Editor.TextProperty, new Binding ("Description", BindingMode.TwoWay));
            sl.Children.Add (el);

            BindingContext = this;
        }
        protected override void OnChildMeasureInvalidated (object sender, EventArgs e) {
            this.InvalidateMeasure ();
        }


        protected override void OnAppearing () {
            base.OnAppearing ();
            
            /*
             <Editor Text="{Binding Item.Description}" 
                    Focused="OnFocus_Description"
                    Keyboard="Chat" x:Name="EDesc" TextChanged="Desc_Changed"
                    VerticalOptions="FillAndExpand">
                </Editor>*/
            //Editor ed = this.FindByName<Editor> ("EDesc");
            //el.Keyboard = Keyboard.Create (KeyboardFlags.All);
        }

        public ItemDetailPage (Item i) {
            InitializeComponent ();
            Item = i;
            BindingContext = this;
            EditMode = true;
        }

        async void Save_Clicked (object sender, EventArgs e) {
            if (EditMode)
                MessagingCenter.Send (this, "UpdateItem", Item);
            else
                MessagingCenter.Send (this, "AddItem", Item);
            await Navigation.PopToRootAsync ();
        }

        private void OnFocus_Description (object sender, FocusEventArgs e) {

        }

        private void OnFocus_Name (object sender, FocusEventArgs e) {

        }

        private void Desc_Changed (object sender, TextChangedEventArgs e) {
            //InvalidateMeasure ();
            //((Editor) sender).
        }

        private void Txt_Changed (object sender, TextChangedEventArgs e) {
            //InvalidateMeasure ();
            int txtLen = this.Item.Text.Length;
            int total =(int)(txtLen * App.ScreenDensity);
            if total > ((Editor)sender).wi
        }
    }

    public class CustomEditor : Editor {

        protected override void OnChildAdded (Element child) {
            base.OnChildAdded (child);
        }
        protected override SizeRequest OnMeasure (double widthConstraint, double heightConstraint) {
            return base.OnMeasure (widthConstraint, heightConstraint);
        }

        protected override void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged (propertyName);
            Debug.WriteLine (this.Height);
        }

        protected override void OnSizeAllocated (double width, double height) {
            base.OnSizeAllocated (width, height);
        }

        protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint) {
            return base.OnSizeRequest (widthConstraint, heightConstraint);
        }

        public override SizeRequest GetSizeRequest (double widthConstraint, double heightConstraint) {
            return base.GetSizeRequest (widthConstraint, heightConstraint);
        }
    }
}