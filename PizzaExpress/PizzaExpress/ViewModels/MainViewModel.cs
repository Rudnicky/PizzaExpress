using PizzaExpress.Services.Navigation;
using PizzaExpress.Services.PizzaService;
using PizzaExpress.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PizzaExpress.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPizzaService _pizzaService;
        private bool _isConnected = false;

        public ICommand MostPopularPizzaButtonClickedCommand => new Command(async () => await MostPopularPizzaButton_Clicked());

        public MainViewModel(INavigationService navigationService, IPizzaService pizzaService) : base(navigationService)
        {
            _pizzaService = pizzaService;
            _isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Connected", "OK");

                _isConnected = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Disconnected", "OK");

                _isConnected = false;
            }
        }

        private async Task MostPopularPizzaButton_Clicked()
        {
            if (!_isConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Please make sure that you have connection to the internet", "OK");

                return;
            }

            IsBusy = true;

            var pizzaToppings = await _pizzaService.Get();

            var top20 = _pizzaService.GetTop20(pizzaToppings);

            await NavigationService.NavigateToAsync<PizzaToppingViewModel>(top20);

            IsBusy = false;
        }
    }
}
