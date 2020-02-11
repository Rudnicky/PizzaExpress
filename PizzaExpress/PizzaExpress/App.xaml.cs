using PizzaExpress.Services.Navigation;
using PizzaExpress.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PizzaExpress
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();

            return navigationService.InitializeAsync();
        }

        protected async override void OnStart()
        {
            base.OnStart();

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
