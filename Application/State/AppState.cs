using Application.Contracts.Menus;
using Application.Models;

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