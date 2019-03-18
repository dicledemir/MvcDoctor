using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseRepository<T> where T : class, IEntity
    {
        private DoctorContext _db;
        public BaseRepository(DoctorContext db)
        {
            _db = db;
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> Search(Func<T, bool> query)
        {
            return _db.Set<T>().Where(query).ToList();
        }

        public T GetOne(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public bool Add(T record)
        {
            try
            {
                _db.Set<T>().Add(record); //savechanges'ı unityofwork' yazdığımız için buraya yazmadık
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public bool Delete(int id) //parametre IEntity entity de olabilir
        {
            //try
            //{
                T t = GetOne(id);
                _db.Set<T>().Remove(t);
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        public bool Update(T newRecord)
        {
            try
            {
                T old = GetOne(newRecord.Id);
                _db.Entry(old).CurrentValues.SetValues(newRecord);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
