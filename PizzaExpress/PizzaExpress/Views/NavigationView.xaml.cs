
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaExpress.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationView : NavigationPage
    {
        public NavigationView() : base()
        {
            InitializeComponent();
        }

        public NavigationView(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}