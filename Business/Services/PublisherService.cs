using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPublisherService
    {
        IQueryable<PublisherModel> Query();
        ResultBase Add(PublisherModel model);
        ResultBase Update(PublisherModel model);
        ResultBase Delete(int id);
    }
    public class PublisherService : IPublisherService
    {
        private readonly Db _db;

        public PublisherService(Db db)
        {
            _db = db;
        }

        public ResultBase Add(PublisherModel model)
        {

            if (_db.Publishers.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Publisher with the same name exists!");
            Publisher entity = new Publisher()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim()
            };

            // Way 1:
            //_db.Publishers.Add(entity);
            // Way 2:
            _db.Add(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public ResultBase Delete(int id)
        {
            Publisher entity = _db.Publishers.Include(r => r.Movies).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return new ErrorResult("Publisher not found!");
            if (entity.Movies is not null && entity.Movies.Any())
                return new ErrorResult("Publisher can't be deleted because it has relational Movies!");

            // Way 1:
            //_db.Publishers.Remove(entity);
            // Way 2:
            _db.Remove(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public IQueryable<PublisherModel> Query()
        {
            return _db.Publishers.Include(p => p.Movies).Select(p => new PublisherModel()
            {
                
                Guid = p.Guid,
                Id = p.Id,
                Name = p.Name,

                Movies = string.Join("<br />", p.Movies.Select(g => g.Name))


            });
        }

        public ResultBase Update(PublisherModel model)
        {
            if (_db.Publishers.Any(p => p.Id != model.Id && p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Publisher with the same name exists!");
            Publisher entity = _db.Publishers.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Publisher not found!");
            entity.Name = model.Name.Trim();

            // Way 1:
            //_db.Publishers.Update(entity);
            // Way 2:
            _db.Update(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }
    }
}
