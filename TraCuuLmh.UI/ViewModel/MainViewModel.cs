using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.XlsIO;
using System.Collections.ObjectModel;
using TraCuuLmh.UI.Model;
using TraCuuLmh.UI.Service;
using static System.Int32;
using Color = Syncfusion.Drawing.Color;

namespace TraCuuLmh.UI.ViewModel;

public partial class MainViewModel : BaseViewModel
{
	private readonly DataService _dataService;
	private ICollection<SinhvienLmh> _sinhVienCollection;
	public ObservableCollection<SinhvienLmh> SinhVienObservableCollection { get; set; } = new();
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

	[ObservableProperty]
	private bool _canSave;


	[RelayCommand]
	public async Task GetData()
	{
		try
		{
			IsBusy = true;
			CanSave = false;
			_sinhVienCollection.Clear();
			SinhVienObservableCollection.Clear();
			Term ??= Terms.Last();

			if (string.IsNullOrEmpty(PageSize))
			{
				PageSize = "100";
			}

			_sinhVienCollection = await _dataService.GetData(_studentId, _subjectId, _studentName, _subjectName, _term.TermId, Parse(_pageSize));
			foreach (var sinhvienLmh in _sinhVienCollection)
			{
				SinhVienObservableCollection.Add(sinhvienLmh);
			}
		}
		catch (Exception)
		{
			await Shell.Current.DisplayAlert("Error.", "Cannot get data", "Ok");
		}
		finally
		{
			Notification = $"Tìm thấy {SinhVienObservableCollection?.Count} kết quả.";
			CanSave = SinhVienObservableCollection?.Count > 0;
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
	[RelayCommand]
	public async Task SaveToExcel()
	{
		using (ExcelEngine excelEngine = new ExcelEngine())
		{
			var application = excelEngine.Excel;
			application.DefaultVersion = ExcelVersion.Excel2013;
			var workbook = application.Workbooks.Create(new[] { "Output" });
			var worksheet = application.Worksheets[0];
			worksheet.Range["A1"].Text = "Mã sinh viên";
			worksheet.Range["B1"].Text = "Họ và tên";
			worksheet.Range["C1"].Text = "Ngày sinh";
			worksheet.Range["D1"].Text = "Lớp khóa học";
			worksheet.Range["E1"].Text = "Mã LHP";
			worksheet.Range["F1"].Text = "Tên môn học";
			worksheet.Range["G1"].Text = "Nhóm";
			worksheet.Range["H1"].Text = "Tín chỉ";
			worksheet.Range["I1"].Text = "Ghi chú";
			worksheet.Range["A1:I1"].CellStyle.Font.Bold = true;
			worksheet.Range["A1:I1"].CellStyle.Color = Color.FromArgb(42, 118, 189);
			worksheet.Range["A1:I1"].CellStyle.Font.Color = ExcelKnownColors.White;
			worksheet.Range["A1:I1000"].AutofitColumns();
			worksheet.Range["A1:I1000"].AutofitRows();

			worksheet.ImportData(_sinhVienCollection, 2, 1, false);

			MemoryStream ms = new MemoryStream();
			try
			{
				var sheet2 = workbook.Worksheets[1];
				sheet2.Visibility = WorksheetVisibility.Hidden;
			}
			catch
			{
				// ignore
			}
			workbook.SaveAs(ms);
			ms.Position = 0;

			//Saves the memory stream as a file.
			SaveService saveService = new SaveService();
			await saveService.SaveAndView("Output.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
		}
	}
	public MainViewModel(DataService dataService)
	{
		_dataService = dataService;
		this.Title = "Tra cứu LMH";
		_sinhVienCollection = new List<SinhvienLmh>();
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