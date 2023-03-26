using Share.Models.ThirdNewsDtos;
using Share.Models;
using System.Net.Http.Json;

namespace DusiApp.Views;

public partial class NewsListPage : ContentPage
{
    NewsListViewModel ViewModel;

    public NewsListPage(NewsListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;

    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await ViewModel.LoadDataAsync();

        base.OnNavigatedTo(args);

    }
}
