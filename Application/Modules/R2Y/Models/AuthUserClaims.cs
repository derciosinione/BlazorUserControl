namespace Application.Modules.R2Y.Models;

public record AuthUserClaims
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Perfil { get; set; }
    public string? MPerfil { get; set; }
    public string? IdRegiao { get; set; }
    public string? GuidRegion { get; set; }
    public string? Idc { get; set; }
    public string? MDirector { get; set; }
    public bool IsMember { get; set; }
    public string? Pais { get; set; }
    public string? Moeda { get; set; }
    public string? Role { get; set; }
}