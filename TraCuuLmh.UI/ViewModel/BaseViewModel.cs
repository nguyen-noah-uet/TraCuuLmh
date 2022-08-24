

using CommunityToolkit.Mvvm.ComponentModel;

namespace TraCuuLmh.UI.ViewModel
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotBusy))]
		private bool _isBusy;

		public bool IsNotBusy => !_isBusy;
		[ObservableProperty]
		private string _title;
	}
}
