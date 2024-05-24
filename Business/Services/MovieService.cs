using Business.Models;
using Business.Services.Bases;
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
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        ResultBase Add(MovieModel movie);
        ResultBase Update(MovieModel movie);

        ResultBase Delete(int id);

        List<MovieModel> GetList()=>Query().ToList();
        MovieModel GetItem(int id)=>Query().SingleOrDefault(g=>g.Id==id);

    }
    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(Db db) : base(db)
        {
        }

        public ResultBase Add(MovieModel movie)
        {
            
            if (_db.Movies.Any(g => g.Name.ToLower() == movie.Name.ToLower().Trim())) {
                return new ErrorResult("Movie with same name exist");
            }

            Movie entity = new Movie()
            {
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                PublisherId = movie.PublisherId,
                UserMovies = movie.UsersInput?.Select(userInput => new UserMovie()
                {
                    UserId = userInput
                }).ToList()
            };

            _db.Movies.Add(entity);
            _db.SaveChanges();

            movie.Id = entity.Id;

            return new SuccessResult();
        }

        public ResultBase Delete(int id)
        {
            Movie entity = _db.Movies.Include(g => g.UserMovies).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return new ErrorResult("Movie not found!");

            _db.UserMovies.RemoveRange(entity.UserMovies);

            _db.Movies.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Movie deleted successfully.");
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(g => g.Publisher).Include(g => g.UserMovies).ThenInclude(ug => ug.User)
                .OrderByDescending(g => g.ReleaseDate).ThenBy(g => g.Name)
                .Select(g => new MovieModel()
                {
                   
                    Guid = g.Guid,
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate,
                    PublisherId = g.PublisherId,
       

                    
                    PublisherOutput = g.Publisher.Name,



                    ReleaseDateOutput = g.ReleaseDate.HasValue ? g.ReleaseDate.Value.ToString("MM/dd/yyyy") : string.Empty,

                    Users = g.UserMovies.Select(ug => new UserModel()
                    {
                        UserName = ug.User.UserName,
                        Status = ug.User.Status
                        
                    }).ToList(),

                    UsersInput = g.UserMovies.Select(ug => ug.UserId).ToList()
                });
        }

        public ResultBase Update(MovieModel movie)
        {
            if (_db.Movies.Any(g => g.Id != movie.Id && g.Name.ToLower() == movie.Name.ToLower().Trim()))
                return new ErrorResult("Movie with the same name exists!");

            Movie entity = _db.Movies.Include(g => g.UserMovies).SingleOrDefault(g => g.Id == movie.Id);
            if (entity is null)
                return new ErrorResult("Movie not found!");

            _db.UserMovies.RemoveRange(entity.UserMovies);

            entity.Name = movie.Name.Trim();
            entity.ReleaseDate = movie.ReleaseDate;
            entity.PublisherId = movie.PublisherId;
            entity.UserMovies = movie.UsersInput?.Select(userInput => new UserMovie()
            {
                UserId = userInput
            }).ToList();

            _db.Movies.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();

        }
    }
}
