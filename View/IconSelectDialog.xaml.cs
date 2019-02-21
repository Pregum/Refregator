using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

using MVVM_Refregator.ViewModel;

using Reactive.Bindings;

namespace MVVM_Refregator.View
{
    /// <summary>
    /// IconSelectDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class IconSelectDialog : Window
    {
        //public BitmapImage SelectedImage { get; set; } = new BitmapImage();
        //public ObservableCollection<BitmapImage> AllImages { get; set; } = new ObservableCollection<BitmapImage>();

        public IconSelectViewModel IconVM { get; } = new IconSelectViewModel();


        public IconSelectDialog()
        {
            ReadFiles();
            InitializeComponent();
        }

        public void ReadFiles()
        {
            //var directoryPath = @"..\..\Resources";
            // var directoryPath = @"./Resources";
            var envir = Environment.CurrentDirectory;

            //foreach ( var item in Application.Current.Resources.Values)
            //{

            //}



            //var files = System.IO.Directory.GetFiles(directoryPath, "*.png");

            //for (int i = 0; i < files.Length; i++)
            //{
            //    var image = new BitmapImage();
            //    image.BeginInit();
            //    image.UriSource = new Uri(files[i], UriKind.Relative);
            //    image.CacheOption = BitmapCacheOption.OnLoad;
            //    image.EndInit();
            //    this.IconVM.AllImages.Add(image);
            //}
            //BitmapImage image = (BitmapImage)App.Current.FindResource("Steak");
            //this.IconVM.AllImages.Add(image);

            foreach (System.Collections.DictionaryEntry resource in Application.Current.Resources)
            {
                var image = resource.Value as BitmapImage;
                if (image != null)
                {
                    this.IconVM.AllImages.Add(image);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.IconVM;
        }
    }
}

namespace MVVM_Refregator.ViewModel
{

    public class IconSelectViewModel : INotifyPropertyChanged
    {

        private BitmapImage selectedImage = new BitmapImage();

        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                this.IsSelected.Value = selectedImage != null;
                this.OnPropertyChanged(nameof(this.SelectedImage));
            }
        }

        private ObservableCollection<BitmapImage> allImages = new ObservableCollection<BitmapImage>();

        public ObservableCollection<BitmapImage> AllImages
        {
            get { return allImages; }
            set
            {
                allImages = value;
                this.OnPropertyChanged(nameof(this.AllImages));
            }
        }

        public ReactiveProperty<bool> IsSelected { get; private set; } = new ReactiveProperty<bool>(false);

        public bool IsCancel { get; private set; } = true;

        public ReactiveCommand Send_OK { get; } = new ReactiveCommand();

        public ReactiveCommand Send_Cancel { get; } = new ReactiveCommand();

        public IconSelectViewModel()
        {
            this.Send_OK.Subscribe((param) =>
            {
                this.IsCancel = false;
                var window = param as Window;
                window.Close();
            });
            this.Send_Cancel.Subscribe((param) =>
            {
                this.IsCancel = true;
                var window = param as Window;
                window.Close();
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
