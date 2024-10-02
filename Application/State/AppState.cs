using Application.Modules.ContentManagement.Contracts.Menus.Response;
using Application.Modules.R2Y.Models;

namespace Application.State;

public class AppState
{
    public AuthUserClaims? UserInfo { get; set; }
    public ContextResponse? MenuContext { get; set; }
    public int Counter { get; set; }

    public void CleanState()
    {
        UserInfo = null;
        MenuContext = null;
    }
}