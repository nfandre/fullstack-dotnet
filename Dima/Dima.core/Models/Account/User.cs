namespace Dima.core.Models.Account;

public class User
{
    // v1/identity/manage/info
    // url padrão do identity
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public Dictionary<string, string> Claims { get; set; } = [];
}