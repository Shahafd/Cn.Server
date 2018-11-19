using CN.Common.Contracts;
using CN.Common.Contracts.IManagers;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.BL.Managers
{
    public class AccountsManager : IAccountsManager
    {
        public IAccountsRepository accountsRepository { get; set; }
        public AccountsManager(IAccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }


        public Tuple<User, RequestStatusEnum> UserLogin(UserLogin userLogin)
        {
            User user = accountsRepository.GetUserByUsername(userLogin.Username);
            if (user != null)
            {
                if (user.Password == userLogin.Password)
                {
                    return new Tuple<User, RequestStatusEnum>(user, RequestStatusEnum.Success);
                }
                else
                {
                    return new Tuple<User, RequestStatusEnum>(null, RequestStatusEnum.Unvalid);
                }
            }
            else
            {
                return new Tuple<User, RequestStatusEnum>(null, RequestStatusEnum.Unvalid);
            }
        }
    }
}
