using Authentication;
using System.Linq;
using System.Text.RegularExpressions;
using PasswordRule = KeeperSecurity.Commands.PasswordRule;

namespace KeeperSecurity.Utils
{
    /// <summary>
    ///     Represents a password complexity rule matcher.
    /// </summary>
    public class PasswordRuleMatcher
    {
        public PasswordRuleMatcher(PasswordRule[] rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// Gets the password rule list.
        /// </summary>
        /// <seealso cref="PasswordRule"/>
        public PasswordRule[] Rules { get; }

        /// <summary>
        ///     Matches password.
        /// </summary>
        /// <param name="password">Master Password.</param>
        /// <returns>A list of failed password rules.</returns>
        public string[] MatchFailedRules(string password)
        {
            return Rules?
                .Where(x =>
                {
                    var match = Regex.IsMatch(password, x.pattern);
                    if (!x.match) match = !match;

                    return !match;
                })
                .Select(x => x.description).ToArray();
        }

        public static PasswordRuleMatcher FromNewUserParams(NewUserMinimumParams userParams)
        {
            var rules = userParams.PasswordMatchDescription
                .Zip(userParams.PasswordMatchRegex,
                    (description, pattern) => new PasswordRule
                    {
                        description = description,
                        match = true,
                        pattern = pattern
                    })
                .ToArray();
            return new PasswordRuleMatcher(rules);
        }
    }
}
