using Common;
using DoguZemin.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoguZemin.InitCommon
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            Users user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }
}