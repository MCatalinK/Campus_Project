using Campus_APP.Helpers;
using Campus_APP.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class StudentActions:BaseVM
    {
        private readonly CampusDbEntities _ctx = new CampusDbEntities();
        private readonly StudentVM _studCtx;

        public StudentActions(StudentVM studCtx)
        {
            _studCtx = studCtx;
        }

        public void AddMethod(object obj)
        {
            var tmp = obj as StudentVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var user = new User()
            {
                username = tmp.FirstName + tmp.LastName,
                password = tmp.SSN,
                idRole=2
            };
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            var stud = _ctx.Users.Where(p => p.username == user.username && p.password == user.password).FirstOrDefault();
            if(stud is null)
            {
                MessageBox.Show("No student found!");
                return;
            }

            _ctx.Students.Add(new Student()
            {
                firstName=tmp.FirstName,
                lastName=tmp.LastName,
                ssn=tmp.SSN,
                idType=tmp.IdType,
                idUni=tmp.IdUni,
                idCampus=tmp.IdCampus,
                idRoom=tmp.IdRoom,
                idUser=stud.id
            }
            );
            var room = _ctx.CampusRooms.Where(p => p.id == tmp.IdRoom).FirstOrDefault();
            room.isOccupied = true;
            _ctx.SaveChanges();
            _studCtx.AllStudents = AllStudents();
        }

        public void DeleteMethod(object obj)
        {
            var tmp = obj as StudentVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var student = _ctx.Students.Where(p => p.id == tmp.Id).FirstOrDefault();
            if (student is null)
            {
                MessageBox.Show("No student identified!");
                return;
            }
            var room = _ctx.CampusRooms.Where(p => p.id == student.idRoom).FirstOrDefault();
            room.isOccupied = false;
            var user = _ctx.Users.Where(p => p.id == student.idUser).FirstOrDefault();
            _ctx.Users.Remove(user);
            _ctx.Students.Remove(student);
            _ctx.SaveChanges();
            _studCtx.AllStudents = AllStudents();
        }

        public void UpdateMethod(object obj)
        {
            var tmp = obj as StudentVM;
            if (tmp is null)
            {
                MessageBox.Show("No object identified!");
                return;
            }
            var student = _ctx.Students.Where(p => p.id == tmp.Id).FirstOrDefault();
            if (student is null)
            {
                MessageBox.Show("No student identified!");
                return;
            }

            student.firstName = tmp.FirstName;
            student.lastName = tmp.LastName;
            student.idType = tmp.IdType;
            student.idCampus = tmp.IdCampus;
            student.idRoom = tmp.IdRoom;
            _ctx.SaveChanges();
            _studCtx.AllStudents = AllStudents();
        }

        public ObservableCollection<StudentVM> AllStudents()
        {
            ObservableCollection<StudentVM> result = new ObservableCollection<StudentVM>();
            var list = _ctx.Students.ToList();
            foreach(var item in list)
            {
                result.Add(new StudentVM()
                {
                    Id = item.id,
                    FirstName = item.firstName,
                    LastName = item.lastName,
                    SSN = item.ssn,
                    IdType = item.idType,
                    IdUni = item.idUni,
                    IdCampus = item.idCampus,
                    IdRoom = item.idRoom,
                    IdUser = item.idUser
                });
            }
            return result;
        }
    }
}
