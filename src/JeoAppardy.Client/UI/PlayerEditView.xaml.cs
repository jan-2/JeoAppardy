using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace JeoAppardy.Client.UI
{
  public sealed partial class PlayerEditView : Page
  {
    public PlayerEditView()
    {
      this.DataContext = this;
      this.InitializeComponent();
    }
    public PlayerEdit ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = new PlayerEdit(this.Frame, e.Parameter as Game);
      base.OnNavigatedTo(e);
    }
  }
}
