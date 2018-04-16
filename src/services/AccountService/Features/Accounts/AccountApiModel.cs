using Core.Entities;

namespace AccountService.Features.Accounts
{
    public class AccountApiModel
    {        
        public int AccountId { get; set; }
        public string Name { get; set; }

        public static AccountApiModel FromAccount(Account account)
        {
            var model = new AccountApiModel();
            model.AccountId = account.AccountId;
            model.Name = account.Name;
            return model;
        }
    }
}
