using PizzaExpress.Services.Navigation;
using PizzaExpress.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaExpress.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand MostPopularPizzaButtonClickedCommand => new Command(async () => await MostPopularPizzaButton_Clicked());

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private async Task MostPopularPizzaButton_Clicked()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync<PizzaToppingViewModel>();

            IsBusy = false;
        }
    }
}
