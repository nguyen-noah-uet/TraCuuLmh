#nullable enable
using HtmlAgilityPack;
using TraCuuLmh.UI.Model;

namespace TraCuuLmh.UI.Service
{
	public class DataService
	{
		public async Task<List<SinhvienLmh>> GetData(string? studentId, string? subjectId, string? studentName, string? subjectName = "", int termId = 34, int pageSize = 100)
		{
			subjectId = subjectId?.Replace(" ", "+") ?? string.Empty;
			studentName = studentName?.Replace(" ", "+") ?? string.Empty;
			subjectName = subjectName?.Replace(" ", "+") ?? string.Empty;
			List<SinhvienLmh> studentCollection = new();
			var url =
				$"http://112.137.129.87/qldt/?SinhvienLmh[masvTitle]={studentId}&SinhvienLmh[hotenTitle]={studentName}&SinhvienLmh[ngaysinhTitle]=&SinhvienLmh[lopkhoahocTitle]=&SinhvienLmh[tenlopmonhocTitle]={subjectId}&SinhvienLmh[tenmonhocTitle]={subjectName}&SinhvienLmh[nhom]=&SinhvienLmh[sotinchiTitle]=&SinhvienLmh[ghichu]=&SinhvienLmh[term_id]=0{termId}&SinhvienLmh_page=1&ajax=sinhvien-lmh-grid&pageSize={pageSize}";
			HtmlDocument htmlDocument = await new HtmlWeb().LoadFromWebAsync(url);
			var tbodyElement = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/div[4]/div[2]/div/div/table/tbody");
			var trCollection = tbodyElement.ChildNodes;
			foreach (var trElement in trCollection)
			{
				var tdCollection = trElement.ChildNodes;
				if (tdCollection.Count > 0)
				{
					SinhvienLmh sinhvienLmh = new()
					{
						MaSv = tdCollection[2].InnerText,
						HoVaTen = tdCollection[3].InnerText,
						NgaySinh = tdCollection[4].InnerText,
						LopKhoaHoc = tdCollection[5].InnerText,
						TenLopMonHoc = tdCollection[6].InnerText,
						TenMonHoc = tdCollection[7].InnerText,
						Nhom = tdCollection[8].InnerText,
						SoTinChi = tdCollection[9].InnerText,
						GhiChu = tdCollection[10].InnerText,
					};
					studentCollection.Add(sinhvienLmh);
				}
			}

			return studentCollection;
		}
	}
}
