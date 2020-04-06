﻿using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Models;

namespace Fambook.UserService.Services.Interfaces
{
    public interface IUserService
    {
        void Create(User user);
        User Get(int id);
        void Delete(User user);
    }
}
