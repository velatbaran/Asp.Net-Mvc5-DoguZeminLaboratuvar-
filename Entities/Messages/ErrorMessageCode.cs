using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Messages
{
    public enum ErrorMessageCode
    {
        EMailAlreadyExists = 101,
        EmailOrPassWrong = 102,
        UserCouldNotUpdate = 103,
        UserNotFound = 104,
        UsernameAlreadyExists = 105,
        CouldNotDeleteUser = 106,
        CouldNotUserRegister = 107,
        CouldNotEmail = 108,
        CouldNotSendPassword = 109
    }
}
