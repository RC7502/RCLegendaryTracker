using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LegendaryTracker.Interfaces;
using LegendaryTracker.Models;

namespace LegendaryTracker.Services
{
    public class AccountService
    {
        private readonly IDAL _dal;

        public AccountService(IDAL dal)
        {
            _dal = dal;
        }

        public User GetAuthenticatedUser(string username, string password)
        {
            var user = _dal.GetUser(username);
            return (user != null && user.Password == password) ? user : null;
        }
    }
}