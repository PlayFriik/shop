#pragma warning disable 1591
namespace WebApp.Models.Home;

public class Index
{
    public WebApp.BLL.DTO.Product? NowAvailable { get; set; }

    public List<WebApp.BLL.DTO.Product> BestSellers { get; set; } = default!;
}