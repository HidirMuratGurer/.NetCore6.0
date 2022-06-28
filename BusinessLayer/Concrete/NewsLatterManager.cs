using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsLatterManager : INewsLatterService
    {
        INewsLatterDal _newsLatterDal;
        public NewsLatterManager(INewsLatterDal newsLatterDal)
        {
            _newsLatterDal = newsLatterDal; 
        }
        public NewsLatter GetByID(int id)
        {
            return _newsLatterDal.GetByID(id);
        }

        public List<NewsLatter> GetList()
        {
            return _newsLatterDal.GetListAll();
        }

        public void TAdd(NewsLatter t)
        {
            _newsLatterDal.Insert(t);
        }

        public void TDelete(NewsLatter t)
        {
            _newsLatterDal.Delete(t);
        }

        public void TUpdate(NewsLatter t)
        {
            _newsLatterDal.Update(t);
        }
    }
}
