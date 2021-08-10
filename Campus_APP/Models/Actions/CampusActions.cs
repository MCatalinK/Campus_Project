using Campus_APP.Helpers;
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
    class CampusActions:BaseVM
    {
        private readonly CampusDbEntities _ctx = new CampusDbEntities();
        private CampusVM _campCtx;

        public CampusActions(CampusVM campCtx)
        {
            _campCtx = campCtx;
        }

        public void AddMethod(object obj)
        {
            var tmp = obj as CampusVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }

            _ctx.Campus.Add(new Campu()
            {
                tax = tmp.Tax,
                idUni = tmp.IdUni
            }
            );
            _ctx.SaveChanges();
            _campCtx.AllCampuses = GetCampusesByUni(tmp.IdUni);
        }

        public void DeleteMethod(object obj)
        {
            var tmp = obj as CampusVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var campus = _ctx.Campus.Where(p => p.id == tmp.Id).FirstOrDefault();
            if (campus is null)
            {
                MessageBox.Show("No campus identified!");
                return;
            }
            _ctx.Campus.Remove(campus);
            _ctx.SaveChanges();
            _campCtx.AllCampuses = GetCampusesByUni(tmp.IdUni);
        }

        public void UpdateMethod(object obj)
        {
            var tmp = obj as CampusVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var campus = _ctx.Campus.Where(p => p.id == tmp.Id).FirstOrDefault();
            if (campus is null)
            {
                MessageBox.Show("No campus identified!");
                return;
            }
            campus.tax = tmp.Tax;
            campus.idUni = tmp.IdUni;
            _ctx.SaveChanges();
            _campCtx.AllCampuses = GetCampusesByUni(tmp.IdUni);
        }

        public ObservableCollection<CampusVM> AllCampuses()
        {
            ObservableCollection<CampusVM> result = new ObservableCollection<CampusVM>();
            var list = _ctx.Campus.ToList();
            foreach (var item in list)
            {
                result.Add(new CampusVM()
                {
                    Id = item.id,
                    Tax = item.tax,
                    IdUni=item.idUni
                });
            }
            return result;
        }
        public ObservableCollection<CampusVM> GetCampusesByUni(int id)
        {
            ObservableCollection<CampusVM> result = new ObservableCollection<CampusVM>();
            var list = _ctx.Campus.Where(p=>p.idUni==id).ToList();
            foreach (var item in list)
            {
                result.Add(new CampusVM()
                {
                    Id = item.id,
                    Tax = item.tax,
                    IdUni = item.idUni
                });
            }
            return result;
        }
        public CampusVM GetCampusById(int id)
        {
            var campus = _ctx.Campus.Where(p => p.id == id).FirstOrDefault();
            if(campus is null)
            {
                MessageBox.Show("No campus found!");
                return null;
            }
            return new CampusVM()
            {
                Id = campus.id,
                Tax = campus.tax,
                IdUni = campus.idUni
            };
        }
    }
}
