using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TraCuuLmh.UI.Model;
using TraCuuLmh.UI.Service;
using static System.Int32;

namespace TraCuuLmh.UI.ViewModel;

public partial class MainViewModel : BaseViewModel
{
	private readonly DataService _dataService;
	public ObservableCollection<SinhvienLmh> SinhVienCollection { get; set; } = new();
	public ObservableCollection<Term> Terms { get; set; } = new();

	[ObservableProperty]
	private string _studentId;

	[ObservableProperty]
	private string _subjectId;

	[ObservableProperty]
	private Term _term;

	[ObservableProperty]
	private string _pageSize;

	[ObservableProperty]
	private string _studentName;
	[ObservableProperty]
	private string _subjectName;

	[ObservableProperty]
	private string _nextMode = "Nâng cao";


	[ObservableProperty]
	private bool _isAdvancedMode;

	[ObservableProperty]
	private string _notification;


	[RelayCommand]
	public async Task GetData()
	{
		try
		{
			IsBusy = true;
			SinhVienCollection.Clear();
			if (Term is null)
			{
				Term = Terms.Last();
			}

			if (string.IsNullOrEmpty(PageSize))
			{
				PageSize = "100";
			}

			var list = await _dataService.GetData(_studentId, _subjectId, _studentName, _subjectName, _term.TermId, Parse(_pageSize));
			foreach (var sinhvienLmh in list)
			{
				SinhVienCollection.Add(sinhvienLmh);
			}
		}
		catch (Exception)
		{
			await Shell.Current.DisplayAlert("Error.", "Cannot get data", "Ok");
		}
		finally
		{
			Notification = $"Tìm thấy {SinhVienCollection?.Count} kết quả.";
			IsBusy = false;
		}

	}
	[RelayCommand]
	public async Task CopyToClipboard(string text)
	{
		await Clipboard.SetTextAsync(text);
		Notification = $" Đã copy '{text}'";

	}
	[RelayCommand]
	public void SwitchMode(string nextMode)
	{
		if (!IsAdvancedMode)
		{
			NextMode = "Cơ bản";
			IsAdvancedMode = true;
		}
		else
		{
			NextMode = "Nâng cao";
			IsAdvancedMode = false;
		}
	}
	public MainViewModel(DataService dataService)
	{
		_dataService = dataService;
		this.Title = "Tra cứu LMH";
		GetTerms();
	}

	public void GetTerms()
	{
		var list = new List<Term>()
		{
			new() { TermId = 25, TermName = "Học kỳ 1 năm 2018-2019" },
			new() { TermId = 26, TermName = "Học kỳ 2 năm 2018-2019" },
			new() { TermId = 27, TermName = "Học kỳ 1 năm 2019-2020" },
			new() { TermId = 28, TermName = "Học kỳ 2 năm 2019-2020" },
			new() { TermId = 29, TermName = "Học kỳ 1 năm 2020-2021" },
			new() { TermId = 30, TermName = "Học kỳ 2 năm 2020-2021" },
			new() { TermId = 31, TermName = "Học kỳ hè năm 2020-2021" },
			new() { TermId = 32, TermName = "Học kỳ 1 năm 2021-2022" },
			new() { TermId = 33, TermName = "Học kỳ 2 năm 2021-2022" },
			new() { TermId = 34, TermName = "Học kỳ hè năm 2021-2022" },
			new() { TermId = 35, TermName = "Học kỳ 1 năm 2022-2023 (Hiện tại)" },
		};
		foreach (var term in list)
		{
			Terms.Add(term);
		}
	}

}