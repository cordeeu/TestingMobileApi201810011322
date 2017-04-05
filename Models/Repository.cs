using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MobileApi.Models;
using System.Threading.Tasks;

namespace MobileApi.Models
{
    public class Repository<T> where T : class
    {
        internal MobileApiContext mobileApiContext = new MobileApiContext();
        internal DbSet<T> DbSet;

        public Repository()
        {
            DbSet = mobileApiContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> query = DbSet;
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<PumaTypeImage> GetImages(int resourceId)
        {
            IEnumerable<PumaTypeImage> query = mobileApiContext.PumaTypeImages.Where(x => x.PumaTypeId == resourceId);
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public async Task<PumaTypeImage> GetImage(int imageId)
        {
            return await mobileApiContext.PumaTypeImages.FindAsync(imageId);
        }

    }
}