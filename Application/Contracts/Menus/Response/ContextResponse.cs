namespace Application.Contracts.Menus;

public record ContextResponse
{
    public Guid Id { get; set; }
    public string? Type { get; set; } = null!;
    public string? Description { get; set; }
    public List<MenuResponse>? Menus { get; set; }
}