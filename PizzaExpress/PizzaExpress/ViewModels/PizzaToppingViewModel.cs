using PizzaExpress.Models;
using PizzaExpress.Services.Navigation;
using PizzaExpress.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PizzaExpress.ViewModels
{
    public class PizzaToppingViewModel : ViewModelBase
    {
        private ObservableCollection<PizzaToppingsModel> _pizzaToppingsModels;
        public ObservableCollection<PizzaToppingsModel> PizzaToppingsModels
        {
            get => _pizzaToppingsModels;
            set
            {
                if (_pizzaToppingsModels != value)
                {
                    _pizzaToppingsModels = value;
                    OnPropertyChanged(nameof(PizzaToppingsModels));
                }
            }
        }

        public PizzaToppingViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = true;
        }

        public override async Task InitializeAsync(object data)
        {
            if (data is IEnumerable<PizzaToppingsModel> top20)
            {
                PizzaToppingsModels = new ObservableCollection<PizzaToppingsModel>(top20);

                IsBusy = false;
            }
        }
    }
}
