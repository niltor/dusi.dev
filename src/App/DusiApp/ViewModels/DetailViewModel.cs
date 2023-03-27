using CommunityToolkit.Maui.Alerts;
using Core.Entities.CMS;
using Core.Utils;
using Dusi.Manage.Client;
using Microsoft.Maui.Controls;
using Share.Models.ThirdNewsDtos;
namespace DusiApp.ViewModels;


[QueryProperty(nameof(News), "News")]
public partial class DetailViewModel : BaseViewModel
{
    private readonly AdminClient _api;

    [ObservableProperty]
    public ThirdNewsItemDto news;

    [ObservableProperty]
    public string source;

    [ObservableProperty]
    public bool isLoading;

    public DetailViewModel()
    {
        Source = string.Empty;
        IsLoading = true;
        _api = ApiService.AdminClient;
    }

    [RelayCommand]
    private async void WebViewNavigated(WebNavigatedEventArgs e)
    {
        IsLoading = false;

        if (e.Result != WebNavigationResult.Success)
        {
            // TODO: handle failed navigation in an appropriate way
            await Shell.Current.DisplayAlert("Navigation failed", e.Result.ToString(), "OK");
        }
    }


    /// <summary>
    /// 设置分类
    /// </summary>
    [RelayCommand]
    private async Task SetNewsTypeAsync()
    {
        var newsTypeOptions = EnumHelper.ToList(typeof(NewsType));
        var options = newsTypeOptions.Select(o => o.Name).ToArray();
        string action = await AppShell.Current.DisplayActionSheet("设置分类", "取消", null, options);

        if (action == "取消") { return; }
        var index = options.ToList().IndexOf(action);
        // 请求
        var data = new ThirdNewsBatchUpdateDto
        {
            Ids = new List<Guid> { News.Id },
            IsDelete = false,
            NewsType = (NewsType)index
        };
        var res = await _api.ThirdNews.BatchUpdateAsync(data);
        if (res != null)
        {
            await Toast.Make("操作成功").Show();
        }
        else
        {
            await Toast.Make(_api.ErrorMsg.Detail).Show();
        }
    }

    /// <summary>
    /// 标记
    /// </summary>
    [RelayCommand]
    private async Task SetTechTypeAsync()
    {
        var newsTypeOptions = EnumHelper.ToList(typeof(TechType));
        var options = newsTypeOptions.Select(o => o.Name).ToArray();
        string action = await AppShell.Current.DisplayActionSheet("设置分类", "取消", null, options);

        if (action == "取消") { return; }
        var index = options.ToList().IndexOf(action);
        var data = new ThirdNewsBatchUpdateDto
        {
            Ids = new List<Guid> { News.Id },
            IsDelete = false,
            TechType = (TechType)index
        };
        var res = await _api.ThirdNews.BatchUpdateAsync(data);
        if (res != null)
        {
            await Toast.Make("操作成功").Show();
        }
        else
        {
            await Toast.Make(_api.ErrorMsg.Detail).Show();
        }
    }

    /// <summary>
    /// 设置为内部保留
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task SetInnerAsync()
    {
        var data = new ThirdNewsBatchUpdateDto
        {
            Ids = new List<Guid> { News.Id },
            IsDelete = false,
            NewsStatus = NewsStatus.Internal
        };
        var res = await _api.ThirdNews.BatchUpdateAsync(data);
        if (res != null)
        {
            await Toast.Make("操作成功").Show();
        }
        else
        {
            await Toast.Make(_api.ErrorMsg.Detail).Show();
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        bool answer = await AppShell.Current.DisplayAlert("⚡删除!", "是否确认删除该内容", "Yes", "No");
        if (answer)
        {
            var data = new ThirdNewsBatchUpdateDto
            {
                Ids = new List<Guid> { News.Id },
                IsDelete = true
            };
            var res = await _api.ThirdNews.BatchUpdateAsync(data);
            if (res != null)
            {
                await Toast.Make("删除成功").Show();
                // 跳转回列表
                await AppShell.Current.Navigation.PopAsync();
            }
            else
            {
                await Toast.Make(_api.ErrorMsg.Detail).Show();
            }
        }
    }

}
