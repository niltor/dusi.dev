using Share.Models.ThirdNewsDtos;

namespace DusiApp.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	ThirdNewsItemDto item;
}
