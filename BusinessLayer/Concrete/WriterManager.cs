using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;
        public WriterManager(IWriterDal writerdal)
        {
            _writerDal = writerdal; 
        }

        public Writer GetByID(int id)
        {
            return _writerDal.GetByID(id);  
        }

        public List<Writer> GetByWriterByID(int id)
        {
            return _writerDal.GetListAll(x => x.WriterID == id);
        }

        public List<Writer> GetList()
        {
            return _writerDal.GetListAll();
        }
        public Writer TGetByFilter(Expression<Func<Writer, bool>> filter)
        {
            return _writerDal.GetByFilter(filter);
        }
        public void TAdd(Writer t)
        {
            _writerDal.Insert(t);
        }

        public void TDelete(Writer t)
        {
            _writerDal.Delete(t);
        }

        public void TUpdate(Writer t)
        {
            _writerDal.Update(t);
        }

    }
}
