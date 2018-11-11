using System;
using System.Collections.ObjectModel;
using System.Windows;
using MVVM_Refregator.Common;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;

namespace MVVM_Refregator.ViewModel
{
    public class AnalysisPageViewModel : BindableBase
    {

        public ReactiveCommand Send_ReadJson { get; } = new ReactiveCommand();

        private ObservableCollection<FoodComposition> _foodList;
        public ObservableCollection<FoodComposition> FoodList { get { return _foodList; } private set { this.SetProperty(ref _foodList, value); } }

        public AnalysisPageViewModel()
        {
            //this.FoodList = JsonManager.ReadJson();
            Send_ReadJson.Subscribe((x) =>
            {
                //try
                //{
                //    this.FoodList = JsonManager.ReadJson();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("読み込みに失敗しました");
                //    MessageBox.Show(ex.Message);
                //}
                this.FoodList = JsonManager.ReadJson();
                MessageBox.Show("読込に成功しました");
            });
        }
    }
}
