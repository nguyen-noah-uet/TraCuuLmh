using HtmlAgilityPack;
using System.Text.Json;

namespace ConsoleApp1
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			List<SinhvienLmh> sinhvienCollection = new();
			var url =
				"http://112.137.129.87/qldt/?SinhvienLmh[masvTitle]=20020683&SinhvienLmh[hotenTitle]=&SinhvienLmh[ngaysinhTitle]=&SinhvienLmh[lopkhoahocTitle]=&SinhvienLmh[tenlopmonhocTitle]=&SinhvienLmh[tenmonhocTitle]=&SinhvienLmh[nhom]=&SinhvienLmh[sotinchiTitle]=&SinhvienLmh[ghichu]=&SinhvienLmh[term_id]=034&SinhvienLmh_page=1&ajax=sinhvien-lmh-grid";
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
					sinhvienCollection.Add(sinhvienLmh);
				}
			}
			var x = JsonSerializer.Serialize(sinhvienCollection);
			Console.WriteLine(x);
			Console.WriteLine(sinhvienCollection.Count());
		}
	}
}