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
        private readonly RoomActions _roomAct = new RoomActions(null);

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

            if (tmp.FirstName.Any(char.IsDigit) || tmp.LastName.Any(char.IsDigit))
            {
                MessageBox.Show("The name can't contain numbers!");
                return;
            }
            string firstName = char.ToUpper(tmp.FirstName.First()) + tmp.FirstName.Substring(1).ToLower();
            string lastName = char.ToUpper(tmp.LastName.First()) + tmp.LastName.Substring(1).ToLower();
            if(GetStudentBySSN(tmp.SSN)!=null)
            {
                MessageBox.Show("There is already a student with this SSN in the database!");
                return;
            }

            /*
            if(tmp.SSN.Length!=13 && tmp.SSN.Any(char.IsLetter))
            {
                MessageBox.Show("The Social Security Number must be 13 characters long and must be entirely numeric!");
                return;
            }*/

            _ctx.Students.Add(new Student()
            {
                firstName=firstName,
                lastName=lastName,
                ssn=tmp.SSN,
                isExmatriculated=false,
                idType=tmp.IdType,
                idUni=tmp.IdUni,
                idCampus=tmp.IdCampus,
                idRoom=tmp.IdRoom,
            }
            );
            var room = _ctx.CampusRooms.Where(p => p.id == tmp.IdRoom).FirstOrDefault();
            room.isOccupied = true;
            _studCtx.AllRooms = _roomAct.GetRoomsAvailableForCampus(tmp.IdCampus);
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
            _ctx.Students.Remove(student);
            _ctx.SaveChanges();
            _studCtx.AllStudents = AllStudents();
            _studCtx.AllRooms = _roomAct.GetRoomsAvailableForCampus(tmp.IdCampus);
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
            if (student.isExmatriculated is true && tmp.IdRoom != null)
            {
                MessageBox.Show("The Student is exmatriculated and can't be assigned a new room!");
                return;
            }
            student.firstName = tmp.FirstName;
            student.lastName = tmp.LastName;
            student.idType = tmp.IdType;
            student.idCampus = tmp.IdCampus;
            if(student.idRoom!=tmp.IdRoom)
            {
                var prevRoom = _ctx.CampusRooms.Where(p => p.id == student.idRoom).FirstOrDefault();
                var newRoom= _ctx.CampusRooms.Where(p => p.id == tmp.IdRoom).FirstOrDefault();
                prevRoom.isOccupied = false;
                newRoom.isOccupied = true;
            }
            student.idRoom = tmp.IdRoom;
            _ctx.SaveChanges();
            _studCtx.AllStudents = AllStudents();
            _studCtx.AllRooms = _roomAct.GetRoomsAvailableForCampus(tmp.IdCampus);
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
                    IsExmatriculated=item.isExmatriculated
                });
            }
            return result;
        }

        public StudentVM GetStudentById(int id)
        {
            var item = _ctx.Students.Where(p => p.id == id).FirstOrDefault();
            if (item is null)
            {
                MessageBox.Show("The student couldn't be found!");
                return null;
            }
            return new StudentVM()
            {
                Id = item.id,
                FirstName = item.firstName,
                LastName = item.lastName,
                SSN = item.ssn,
                IdType = item.idType,
                IdUni = item.idUni,
                IdCampus = item.idCampus,
                IdRoom = item.idRoom,
                IsExmatriculated=item.isExmatriculated    
            };
        }
        public StudentVM GetStudentBySSN(string ssn)
        {
            var item = _ctx.Students.Where(p => p.ssn == ssn).FirstOrDefault();
            if (item is null)
            {
                return null;
            }
            return new StudentVM()
            {
                Id = item.id,
                FirstName = item.firstName,
                LastName = item.lastName,
                SSN = item.ssn,
                IdType = item.idType,
                IdUni = item.idUni,
                IdCampus = item.idCampus,
                IdRoom = item.idRoom,
                IsExmatriculated = item.isExmatriculated
            };
        }
    }
}
