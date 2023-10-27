
namespace KeeperSecurity.Commands
{
    public interface IPasswordRules
    {
        string PasswordRulesIntro { get; }
        PasswordRule[] PasswordRules { get; }
    }
}
