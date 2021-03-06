﻿using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class UnitOfWork
    {
        public DoctorContext db;
        public UnitOfWork()
        {//double lock pattern :
         ////thread safe olarak db'nin tek bir kez üretilmesini sağlamak. Microsoft sınavında çıkar ama çok sık karşılaşılan bir durum değil
            object whatever = ""; //kilitlemek için kullanılan nesnemiz. tipi adı vs önemli değil
            if (db == null)
            {
                lock (whatever) //bu kitliyken başka hiçbir thread buraya ulaşamaz. bu işlem bitene kadar projede başka hiçbir isteğe cevap verilmez (asenkron işlemleri dahil bekletir)
                {
                    if (db == null)
                        db = new DoctorContext();
                }

            }

            Categories = new BaseRepository<Category>(db);
            Contacts = new BaseRepository<Contact>(db);
            Appointments = new BaseRepository<Appointment>(db);
            Notifications = new BaseRepository<Notification>(db);
            Writes = new BaseRepository<Write>(db);
            WriteComments = new BaseRepository<WriteComment>(db);
            AppointmentStatuss = new BaseRepository<AppointmentStatus>(db);
            //Sliders = new BaseRepository<Slider>(db);

        }

        public static DoctorContext Create()
        {
            return new DoctorContext();
        }
        public bool Complete() //savechanges yapacağımız yer
        {//dosyalarla alakalı tüm işlemlerde / bool tipi döndüren metotlarda try catch kullanmamız gerekir! (garantiye almak için)
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BaseRepository<Category> Categories;
        public BaseRepository<Contact> Contacts;
        public BaseRepository<Appointment> Appointments;
        public BaseRepository<Notification> Notifications;
        public BaseRepository<Write> Writes;
        public BaseRepository<WriteComment> WriteComments;
        public BaseRepository<AppointmentStatus> AppointmentStatuss;
         //public BaseRepository<Slider> Sliders;

    }
}
