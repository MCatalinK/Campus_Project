using Campus_APP.Helpers;
using Campus_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class UserActions:BaseVM
    {
        CampusDbEntities ctx = new CampusDbEntities();
        public UserActions()
        {
            int nrUsers = ctx.Users.Count();
            if (nrUsers is 0)
            {
                ctx.Users.Add(new User()
                {
                    username = "admin",
                    password = "admin",
                    idRole = 1
                });
                ctx.SaveChanges();
            }
        }
        public void AddMethod(object obj)
        {
            UserVM userVM = obj as UserVM;
            if (userVM is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            var users = ctx.Users.Where(p => p.username == userVM.Username).FirstOrDefault();
            if (users != null)
            {
                MessageBox.Show("This user already exists!");
                return;
            }
            ctx.Users.Add(new User()
            {
                username = userVM.Username,
                password = userVM.Password,
                idRole = userVM.IdRole
            });
            ctx.SaveChanges();
        }
        public void DeleteMethod(object obj)
        {
            UserVM userVM = obj as UserVM;
            if (userVM is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            var user = ctx.Users.Where(p => p.id == userVM.Id).FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            ctx.Users.Remove(user);
            ctx.SaveChanges();
        }
        public UserVM GetUser(UserVM normalUser)
        {
            var user = ctx.Users.Where(p => p.username.Equals(normalUser.Username) && p.password.Equals(normalUser.Password))
                .FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return null;
            }
            return new UserVM()
            {
                Id = user.id,
                Username = user.username,
                Password = user.password,
                IdRole = user.idRole
            };
        }
    }
}
