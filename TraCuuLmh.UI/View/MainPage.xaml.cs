using TraCuuLmh.UI.ViewModel;

namespace TraCuuLmh.UI
{
	public partial class MainPage : ContentPage
	{

		public MainPage(MainViewModel viewModel)
		{
			InitializeComponent();
			this.BindingContext = viewModel;
		}

	}
}