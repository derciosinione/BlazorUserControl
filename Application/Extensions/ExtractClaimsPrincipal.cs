using System.Security.Claims;
using Application.Modules.R2Y.Models;

namespace Application.Extensions;

public static class ExtractClaimsPrincipal
{
    public static AuthUserClaims? ToUserModel(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.Identity == null && !claimsPrincipal.Identity!.IsAuthenticated) return null;

        var claims = claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);

        return new AuthUserClaims
        {
            Id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value,
            Email = claimsPrincipal.FindFirst(ClaimTypes.Email)!.Value,
            Role = claimsPrincipal.FindFirst(ClaimTypes.Role)!.Value,
            Perfil = claims.TryGetValue("Perfil", out var profile) ? profile : string.Empty,
            MPerfil = claims.TryGetValue("MPerfil", out var mProfile) ? mProfile : string.Empty,
            IdRegiao = claims.TryGetValue("IdRegiao", out var claim1) ? claim1 : string.Empty,
            GuidRegion = claims.TryGetValue("GuidRegion", out var region) ? region : string.Empty,
            Idc = claims.TryGetValue("Idc", out var idc) ? idc : string.Empty,
            MDirector = claims.TryGetValue("MDirector", out var mDirector) ? mDirector : string.Empty,
            IsMember =
                claims.ContainsKey("IsMember") && bool.TryParse(claims["IsMember"], out var isMember) && isMember,
            Pais = claims.TryGetValue("Pais", out var country) ? country : string.Empty,
            Moeda = claims.TryGetValue("Moeda", out var coin) ? coin : string.Empty
        };
    }
}