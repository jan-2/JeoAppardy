using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace JeoAppardy.Client
{
  public sealed partial class MainPage : Page
  {
    public MainPage() {
      DataContext = this;
      this.InitializeComponent();
    }

    public UI.Client ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = new UI.Client(this.Frame, e.Parameter);
      base.OnNavigatedTo(e);
    }
  }
}