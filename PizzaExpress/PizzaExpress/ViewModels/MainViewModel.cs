using PizzaExpress.Services.Navigation;
using PizzaExpress.ViewModels.Base;

namespace PizzaExpress.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
