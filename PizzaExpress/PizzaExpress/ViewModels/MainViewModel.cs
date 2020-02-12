using PizzaExpress.Services.Navigation;
using PizzaExpress.Services.PizzaService;
using PizzaExpress.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaExpress.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPizzaService _pizzaService;

        public ICommand MostPopularPizzaButtonClickedCommand => new Command(async () => await MostPopularPizzaButton_Clicked());

        public MainViewModel(INavigationService navigationService, IPizzaService pizzaService) : base(navigationService)
        {
            _pizzaService = pizzaService;
        }

        private async Task MostPopularPizzaButton_Clicked()
        {
            IsBusy = true;

            var pizzaToppings = await _pizzaService.Get();

            var top20 = _pizzaService.GetTop20(pizzaToppings);

            await NavigationService.NavigateToAsync<PizzaToppingViewModel>(top20);

            IsBusy = false;
        }
    }
}
