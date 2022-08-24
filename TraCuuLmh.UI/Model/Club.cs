using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace TraCuuLmh.UI.Model;

public partial class Club : ObservableObject
{
	[ObservableProperty]
	private string name;
	[ObservableProperty]
	private DateTime established;
	[ObservableProperty]
	private DateTime time;
	[ObservableProperty]
	private int stadiumCapacity;
	[ObservableProperty]
	private bool isChampion;
	[ObservableProperty]
	private string country;

	public Club(string name, DateTime established, DateTime time, int stadiumCapacity, string country)
	{
		this.name = name;
		this.established = established;
		this.time = time;
		this.stadiumCapacity = stadiumCapacity;
		this.country = country;
	}


	public List<string> Countries => new List<string> { "England", "Spain", "France", "Bulgaria" };
}