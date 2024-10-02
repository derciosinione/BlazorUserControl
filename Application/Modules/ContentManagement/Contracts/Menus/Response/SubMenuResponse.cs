namespace Application.Modules.ContentManagement.Contracts.Menus.Response;

public record SubMenuResponse
{
    public Guid Id { get; init; }
    public Guid MenuId { get; init; }
    public string? Name { get; set; }
    public string? CodeI18N { get; set; } = default!;
    public string? Action { get; set; }
    public int Order { get; set; }
}