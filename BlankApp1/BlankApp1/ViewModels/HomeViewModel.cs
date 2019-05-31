using BlankApp1.Utils;
using BlankApp1.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp1.ViewModels
{
	public class HomeViewModel : ViewModelBase
    {
        public ICommand AddClicked { get; set; }
        private ImageSource _imageSource;
        public ImageSource Source
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        private ObservableCollection<PostItem> _items = new ObservableCollection<PostItem>();
        public ObservableCollection<PostItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public HomeViewModel(INavigationService navigationMethod, IPageDialogService dialogService) : base(navigationMethod, dialogService)
        {
            
            Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("add_button.png") : ImageSource.FromFile("Assets/add_button.png");
            Items = new ObservableCollection<PostItem>(PostDatabase.Instance.GetItemsAsync().Result);

            AddClicked = new Command(ImageButton_Clicked);
        }

        private async void ImageButton_Clicked()
        {
            await NavigationMethod.NavigateAsync(nameof(Post));
        }
    }
    
}
