using Campus_APP.Helpers;
using Campus_APP.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class UniversityActions:BaseVM
    {
        private readonly CampusDbEntities _ctx = new CampusDbEntities();
        private readonly UniversityVM _uniCtx;
        public UniversityActions(UniversityVM uniCtx)
        {
            _uniCtx = uniCtx;
        }

        public void AddMethod(object obj)
        {
            var uni = obj as string;
            if (uni is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            if(_ctx.Universities.Where(p=>p.name==uni).FirstOrDefault()!=null)
            {
                MessageBox.Show("University already exists!");
                return;
            }
            _ctx.Universities.Add(new University() { name = uni });
            _ctx.SaveChanges();
            _uniCtx.AllUniversities = AllUniversities();
        }

        public void DeleteMethod(object obj)
        {
            var uni = obj as UniversityVM;
            if (uni is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var tmp = _ctx.Universities.Where(p => p.id == uni.Id).FirstOrDefault();
            if(tmp is null)
            {
                MessageBox.Show("No university identified!");
                return;
            }
            _ctx.Universities.Remove(tmp);
            _ctx.SaveChanges();
            _uniCtx.AllUniversities = AllUniversities();
        }

        public void UpdateMethod(object obj)
        {
            var uni = obj as UniversityVM;
            if (uni is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var tmp = _ctx.Universities.Where(p => p.id == uni.Id).FirstOrDefault();
            if (tmp is null)
            {
                MessageBox.Show("No university identified!");
                return;
            }
            tmp.name = uni.Name;
            _ctx.SaveChanges();
            _uniCtx.AllUniversities = AllUniversities();
        }

        public ObservableCollection<UniversityVM> AllUniversities()
        {
            ObservableCollection<UniversityVM> result = new ObservableCollection<UniversityVM>();
            var list = _ctx.Universities.ToList();
            foreach(var item in list)
            {
                result.Add(new UniversityVM()
                {
                    Id = item.id,
                    Name = item.name
                });
            }
            return result;
        }

        public UniversityVM GetUniversityById(int id)
        {
            var uni = _ctx.Universities.Where(p => p.id == id).FirstOrDefault();
            if(uni is null)
            {
                MessageBox.Show("No university found!");
                return null;
            }
            return new UniversityVM()
            {
                Id = uni.id,
                Name = uni.name
            };
        }
    }
}
