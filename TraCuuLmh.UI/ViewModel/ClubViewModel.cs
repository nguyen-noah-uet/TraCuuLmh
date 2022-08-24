using System.Collections.ObjectModel;

namespace TraCuuLmh.UI.ViewModel;

public class ClubViewModel
{
	private ObservableCollection<Model.Club> clubs;

	public ObservableCollection<Model.Club> Clubs => clubs ?? (clubs = CreateClubs());

	private ObservableCollection<Model.Club> CreateClubs()
	{
		return new ObservableCollection<Model.Club>
		{
			new Model.Club("UK Liverpool ", new DateTime(1892, 1, 1), new DateTime(2018, 2, 22, 3, 28, 33), 45362, "England"),
			new Model.Club("Manchester Utd.", new DateTime(1878, 1, 1), new DateTime(2018, 1, 1, 2, 56, 44), 76212, "England") { IsChampion = true },
			new Model.Club("Chelsea", new DateTime(1905, 1, 1), new DateTime(2018, 6, 17, 6, 19, 59), 42055, "England"),
			new Model.Club("Barcelona", new DateTime(1899, 1, 1), new DateTime(2018, 7, 12, 12, 25, 31), 99354, "Spain")
		};
	}
}