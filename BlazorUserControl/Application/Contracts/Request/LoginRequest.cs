namespace BlazorUserControl.Application.Contracts.Request;

public record LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public LoginRequest() { }
    
    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}