using Share.Models;
using System.Net.Http.Json;
using Share.Models.ThirdNewsDtos;

namespace DusiApp.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<ThirdNewsItemDto> items;

    public ListDetailViewModel()
    {

    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;

        try
        {
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task LoadMore()
    {

    }

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<ThirdNewsItemDto>(await GetNewsAsync());
    }

    /// <summary>
    /// 获取资讯内容
    /// </summary>
    /// <returns></returns>
    public async Task<List<ThirdNewsItemDto>> GetNewsAsync()
    {
        // TODO:改写调用sdk
        var data = new ThirdNewsFilterDto()
        {
            PageIndex = 1,
            PageSize = 100,
        };

        var token = Preferences.Default.Get("AccessToken", string.Empty);
        using var http = new HttpClient();
        http.Timeout = TimeSpan.FromSeconds(5);
        http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "bearer " + token);
        var res = await http.PostAsJsonAsync("http://10.0.2.2:5002/api/admin/thirdNews/filter", data);

        if (res.IsSuccessStatusCode)
        {
            var resData = await res.Content.ReadFromJsonAsync<PageList<ThirdNewsItemDto>>();
            return resData.Data;
        }
        return new List<ThirdNewsItemDto>();
    }

    [RelayCommand]
    private async void GoToDetails(ThirdNewsItemDto item)
    {
        //await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
        //{
        //    { "Item", item }
        //});

        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            { "News", item }
        });

    }
}
