using CN.Common.Contracts;
using CN.Common.Contracts.IManagers;
using CN.Common.Contracts.IRepositories;
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
        public INetworkRepository networkRepository { get; set; }
        public AccountsManager(INetworkRepository networkRepository)
        {
            this.networkRepository = networkRepository;
        }


        public Tuple<User, RequestStatusEnum> UserLogin(UserLogin userLogin)
        {
            User user = networkRepository.GetUserByUsername(userLogin.Username);
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
