namespace Application.Modules.ContentManagement.Contracts.Menus.Response;

public record MenuResponse
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? CodeI18N { get; set; } = default!;
    public string? Action { get; set; }
    public int Order { get; set; }
    public List<SubMenuResponse?> SubMenus { get; set; } = [];
}