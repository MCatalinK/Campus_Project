using Campus_APP.Helpers;
using Campus_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Campus_APP.Models.Actions
{
    class InvoiceActions:BaseVM
    {
        private readonly CampusDbEntities _ctx = new CampusDbEntities();
        private readonly InvoiceVM _invoiceCtx;
        public InvoiceActions(InvoiceVM invoiceCtx)
        {
            _invoiceCtx = invoiceCtx;
        }
        public void AddInvoices(int id,int nr)
        {
            var listInvoice = _ctx.Invoices.Where(p => p.idStudent == id 
            && p.deadendDate.Month <= DateTime.Now.Month 
            && p.deadendDate.Month>=DateTime.Now.Month-nr
            && p.deadendDate.Year == DateTime.Now.Year
            && p.isPayed==false)
                .ToList();
            if (listInvoice.Count==0)
            {
                var item = _ctx.Students.Where(p => p.id == id).FirstOrDefault();
                if (item is null)
                    return;
                
                var student = new StudentVM()
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

                for (int i = DateTime.Now.Month - nr + 1; i <= DateTime.Now.Month; i++)
                {
                    int days = DateTime.DaysInMonth(DateTime.Now.Year,i);
                    string deDate = days.ToString() + "/" + i + "/" + DateTime.Now.Year;
                    _ctx.Invoices.Add(new Invoice()
                    {
                        datePayed = null,
                        deadendDate = DateTime.Parse(deDate),
                        total = student.Campus.Tax,
                        isPayed = false,
                        idStudent = student.Id,
                    });
                }
                _ctx.SaveChanges();
                _invoiceCtx.AllInvoices = AllInvoices(id);
            }
        }

        public void InvoiceLogic(int id)
        {
            var item = _ctx.Students.Where(p => p.id == id).FirstOrDefault();
            if (item is null)
            {
                return;
            }
            var student = new StudentVM()
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
            decimal rate = 0;
            switch (student.StudentType.Name)
            {
                case "normalTax":
                    rate = 1;
                    break;
                case "doubleTax":
                    rate = 2;
                    break;
                case "semiTax":
                    rate = 0.5M;
                    break;
            }
            if(DateTime.Now.Month<8 && DateTime.Now.Month>9)
            {
                if(_ctx.Invoices.Where(p=>p.deadendDate.Month==DateTime.Now.Month && p.deadendDate.Year==DateTime.Now.Year).FirstOrDefault() is null)
                {
                    int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    string deDate = days.ToString() + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    _ctx.Invoices.Add(new Invoice()
                    {
                        datePayed = null,
                        deadendDate = DateTime.Parse(deDate),
                        total = student.Campus.Tax,
                        isPayed = false,
                        idStudent = student.Id,
                    });
                }
            }


            var listUnpayed = _ctx.Invoices.Where(p => p.idStudent == id && p.isPayed == false)
                .OrderBy(p => p.deadendDate)
                .ToList();
            if (listUnpayed.Count == 1 && listUnpayed.First().deadendDate.Month == DateTime.Now.Month)
                return;
            
            if (listUnpayed.Count >2)
            {
                var stud = _ctx.Students.Where(p => p.id == id).FirstOrDefault();
                var room = _ctx.CampusRooms.Where(p => p.id == stud.idRoom).FirstOrDefault();
                room.isOccupied = false;
                stud.idRoom = null;
                stud.isExmatriculated = true;
            }
            else if(listUnpayed.Count<3 && listUnpayed.Count>1)
            {
                foreach (var invoice in listUnpayed)
                {
                    invoice.total = student.Campus.Tax*rate;
                    int month = invoice.deadendDate.Month + 1;
                    int nrDays = 0;
                    for (int i=month;i<=DateTime.Now.Month;i++)
                    {
                        nrDays+= DateTime.DaysInMonth(DateTime.Now.Year,i);
                    }
                    
                    nrDays += DateTime.Now.Day;
                    invoice.total += (nrDays *student.Campus.Tax) / 1000;
                }
            }
            _ctx.SaveChanges();
        }

        public void PayTax(int id)
        {
            var listUnpayed = _ctx.Invoices.Where(p => p.idStudent == id && p.isPayed == false)
                .OrderBy(p => p.deadendDate)
                .ToList();

            if(listUnpayed is null)
            {
                MessageBox.Show("All the taxes are paid!");
                return;
            }
            var item = _ctx.Students.Where(p => p.id == id).FirstOrDefault();
            var student = new StudentVM()
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
            List<string> months = new List<string>();
            decimal totalToPay = 0;
            foreach ( var obj in listUnpayed)
            {
                obj.isPayed = true;
                obj.datePayed = DateTime.Now;
                totalToPay += obj.total;
                months.Add(MonthConverter.NoToMonthConverter(obj.deadendDate.Month));
            }

            _ctx.SaveChanges();
            CreatePDF.GeneratePDF(student, months, totalToPay);
            MessageBox.Show("The payment was complete!");
            _invoiceCtx.AllInvoices = AllInvoices(id);
        }
       
        public ObservableCollection<InvoiceVM> AllInvoices(int id)
        {
            InvoiceLogic(id);
            ObservableCollection<InvoiceVM> result = new ObservableCollection<InvoiceVM>();
            var list = _ctx.Invoices.Where(p=>p.idStudent==id)
                .ToList();
            foreach (var item in list)
            {
                result.Add(new InvoiceVM()
                {
                    Id = item.id,
                    DeadendDate=item.deadendDate,
                    DatePayed=item.datePayed,
                    Total=item.total,
                    IsPayed=item.isPayed,
                    IdStudent=item.idStudent
                });
            }
            return result;
        }
    }
}
