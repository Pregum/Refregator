using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Refregator.Model
{
    public class WorkStepModel : BindableBase
    {
        private ObservableCollection<IStep> _registerSteps = new ObservableCollection<IStep>();

        public ObservableCollection<IStep> RegisterSteps
        {
            get { return this._registerSteps; }
            set { this.SetProperty(ref _registerSteps, value); }
        }

        //private static WorkStepModel _singleton = null;

        //public static WorkStepModel GetInstance()
        //{
        //    _singleton = _singleton ?? new WorkStepModel();
        //    return _singleton;
        //}

        /// <summary>
        /// ctor
        /// </summary>
        public WorkStepModel()
        {
            this.RegisterSteps.Add(new FoodNameEditStep());
            this.RegisterSteps.Add(new FoodBoughtDateEditStep());
            this.RegisterSteps.Add(new FoodLimitDateEditStep());
            this.RegisterSteps.Add(new FoodConfirmStep());
        }
    }
}
