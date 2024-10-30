

namespace ProductManagement.Application;

internal class ProductManagementSetting
{
    public string ProductDirectory { get; set; } = string.Empty;
    public string CategoryDirectory { get; set; } = string.Empty;

}
internal sealed class AppSettings : ProductManagementSetting
{
    public string ContentRootPath { get; set; } 
    public string EnvironmentName { get; set; }



}
