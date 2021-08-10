using Campus_APP.Helpers;
using Campus_APP.Helpers.Enums;
using Campus_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class StudentTypeActions:BaseVM
    {
        CampusDbEntities ctx = new CampusDbEntities();

        public void SetTypes()
        {
            var list = Enum.GetNames(typeof(StudentTypeEnum)).ToList();
            foreach (var item in list)
                if (ctx.StudentTypes.Where(p => p.name.Equals(item)).FirstOrDefault() is null)
                    ctx.StudentTypes.Add(new StudentType()
                    {
                        name = item
                    });

            ctx.SaveChanges();
        }
        public ObservableCollection<StudentTypeVM> AllTypes()
        {
            var list = ctx.StudentTypes.ToList();
            ObservableCollection<StudentTypeVM> result = new ObservableCollection<StudentTypeVM>();
            foreach (var item in list)
            {
                result.Add(new StudentTypeVM()
                {
                    Id = item.id,
                    Name = item.name
                });
            }
            return result;
        }
        public StudentTypeVM GetType(int id)
        {
            var item = ctx.StudentTypes.Where(p => p.id == id).FirstOrDefault();
            if (item is null)
            {
                MessageBox.Show("No type found!");
                return null;
            }
            return new StudentTypeVM() { Id = item.id, Name = item.name };
        }
    }
}
