using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Policy
{
    public class SystemUserPolicy : ISystemUserPolicy
    {
        private readonly ISystemUserRules systemUserRules;

        public SystemUserPolicy(ISystemUserRules systemUserRules, ISystemUserRepository systemUserRepository)
        {
            this.systemUserRules = systemUserRules;
            SystemUserRepository = systemUserRepository;
        }

        public ISystemUserRepository SystemUserRepository { get; set; }

        public IRuleCollection<SystemUser> InvariantRules()
        {
            return new RuleCollection<SystemUser>
                (
                IdRule(), LoginNameRule()
                );
        }

        private static IRule<SystemUser> IdRule()
        {
            return
                new Rule<SystemUser>
                    (
                    "Id may not be empty.",
                    (item, rule) => item.Id.Equals(Guid.Empty));
        }

        private IRule<SystemUser> LoginNameRule()
        {
            return
                new Rule<SystemUser>
                    (
                    "Login name errors:",
                    (item, rule) =>
                        {
                            var result = systemUserRules.LoginNameRules().BrokenBy(item.LoginName);

                            if (result.IsEmpty)
                            {
                                return false;
                            }

                            rule.Message.AddDetailMessages(result.Messages);

                            return true;
                        });
        }
    }
}
