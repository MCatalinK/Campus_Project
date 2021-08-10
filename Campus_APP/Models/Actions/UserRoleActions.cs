using Campus_APP.Helpers;
using Campus_APP.Helpers.Enums;
using Campus_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Campus_APP.Models.Actions
{
    class UserRoleActions:BaseVM
    {
        CampusDbEntities ctx = new CampusDbEntities();
        public UserRoleActions()
        {
        }
        public void SetRoles()
        {
            var rolesList = Enum.GetNames(typeof(UserRolesEnum)).ToList();
            foreach (var role in rolesList)
                if (ctx.UserRoles.Where(p => p.name.Equals(role)).FirstOrDefault() is null)
                    ctx.UserRoles.Add(new UserRole()
                    {
                        name = role
                    });

            ctx.SaveChanges();
        }
        public ObservableCollection<UserRoleVM> AllRoles()
        {
            List<UserRole> roles = ctx.UserRoles.ToList();
            ObservableCollection<UserRoleVM> result = new ObservableCollection<UserRoleVM>();
            foreach (UserRole role in roles)
            {
                result.Add(new UserRoleVM()
                {
                    Id = role.id,
                    Name = role.name
                });
            }
            return result;
        }
        public UserRoleVM GetRole(int id)
        {
            var role = ctx.UserRoles.Where(p => p.id == id).FirstOrDefault();
            if (role is null) throw new Exception("Role doesn't exists!");
            return new UserRoleVM() { Id = role.id, Name = role.name };
        }
    }
}
