using Campus_APP.Helpers;
using Campus_APP.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class RoomActions:BaseVM
    {
        private readonly CampusDbEntities _ctx = new CampusDbEntities();
        private readonly CampusRoomVM _roomCtx;

        public RoomActions(CampusRoomVM roomCtx)
        {
            _roomCtx = roomCtx;
        }

        public void AddMethod(object obj)
        {
            var tmp = obj as string;
            if(tmp is null)
            {
                MessageBox.Show("The room couldn't be identified!");
                return;
            }
            int id = Int32.Parse(tmp);
            var lastRoom = _ctx.CampusRooms.Where(p => p.idCampus == id)
                .OrderByDescending(p => p.noRoom)
                .FirstOrDefault();
            if (lastRoom is null)
                _ctx.CampusRooms.Add(new CampusRoom()
                {
                    idCampus = id,
                    noRoom = 1,
                    isOccupied = false
                });
            else
                _ctx.CampusRooms.Add(new CampusRoom()
                {
                    idCampus = id,
                    noRoom = lastRoom.noRoom+1,
                    isOccupied = false
                });
            _ctx.SaveChanges();
            _roomCtx.AllRooms = GetRoomsByCampus(id);
        }
        
        public void DeleteMethod(object obj)
        {
            var tmp = obj as CampusRoomVM;
            if (tmp is null)
            {
                MessageBox.Show("The room couldn't be identified!");
                return;
            }
            var room = _ctx.CampusRooms.Where(p => p.id == tmp.Id).FirstOrDefault();
            if(room is null)
            {
                MessageBox.Show("The room couldn't be found!");
                return;
            }
            _ctx.CampusRooms.Remove(room);
            var list = _ctx.CampusRooms.Where(p => p.noRoom > tmp.NoRoom).ToList();
            foreach(var item in list)
            {
                item.noRoom--;
            }
            _ctx.SaveChanges();
            _roomCtx.AllRooms = GetRoomsByCampus(tmp.IdCampus);
        }
        public ObservableCollection<CampusRoomVM> GetRoomsByCampus(int id)
        {
            ObservableCollection<CampusRoomVM> result = new ObservableCollection<CampusRoomVM>();
            var list = _ctx.CampusRooms.Where(p => p.idCampus == id).ToList();
            if (list is null)
                return null;
            foreach(var item in list)
            {
                result.Add(new CampusRoomVM()
                {
                    Id = item.id,
                    NoRoom = item.noRoom,
                    IsOccupied = item.isOccupied,
                    IdCampus = item.idCampus
                });
            }
            return result;
        }
        public ObservableCollection<CampusRoomVM> GetRoomsAvailableForCampus(int id)
        {
            ObservableCollection<CampusRoomVM> result = new ObservableCollection<CampusRoomVM>();
            var list = _ctx.CampusRooms.Where(p => p.idCampus == id && p.isOccupied==false).ToList();
            if (list is null)
                return null;
            foreach (var item in list)
            {
                result.Add(new CampusRoomVM()
                {
                    Id = item.id,
                    NoRoom = item.noRoom,
                    IsOccupied = item.isOccupied,
                    IdCampus = item.idCampus
                });
            }
            return result;
        }

        public CampusRoomVM GetRoomById(int? id)
        {
            if (id is null)
                return null;
            var room = _ctx.CampusRooms.Where(p => p.id == id).FirstOrDefault();
            if (room is null) return null;
            return new CampusRoomVM()
            {
                Id = room.id,
                NoRoom = room.noRoom,
                IsOccupied = room.isOccupied,
                IdCampus = room.idCampus
            };
        }
    }
}
