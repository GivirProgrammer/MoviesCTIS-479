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
    public interface IUserService
    {
        IQueryable<UserModel> Query();
        ResultBase Add(UserModel model);
        ResultBase Update(UserModel model);
        ResultBase Delete(int id);
        List<UserModel> GetList() => Query().ToList();
        UserModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
    }
    public class UserService : ServiceBase, IUserService
    {
        public UserService(Db db) : base(db)
        {
        }

        public ResultBase Add(UserModel model)
        {
            if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
                return new ErrorResult("Active user with the same user name exists!");

            User entity = new User()
            {
                IsActive = model.IsActive,
                Password = model.Password.Trim(),

                RoleId = model.RoleId.Value,

                Status = model.Status,
                UserName = model.UserName.Trim()

                // TODO: Games
            };

            _db.Users.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult("User added successfully.");

        }

        public ResultBase Delete(int id)
        {
            User entity = _db.Users.Include(u => u.UserMovies).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return new ErrorResult("User not found!");

            _db.UserMovies.RemoveRange(entity.UserMovies);
            _db.Users.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("User deleted successfully.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel()
            {
                Guid = u.Guid,
                Id = u.Id,
                IsActive = u.IsActive,
                Password = u.Password,
                RoleId = u.RoleId,
                Status = u.Status,
                UserName = u.UserName,

                IsActiveOutput = u.IsActive ? "Yes" : "No",
                RoleOutput=new RoleModel()
                {
                    Guid=u.Role.Guid,
                    Id=u.Role.Id,
                    Name=u.Role.Name,
                }
            });
        }

        public ResultBase Update(UserModel model)
        {
            if (_db.Users.Any(u => u.Id != model.Id && u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
                return new ErrorResult("Active user with the same user name exists!");

            User entity = _db.Users.SingleOrDefault(u => u.Id == model.Id);
            if (entity is null)
                return new ErrorResult("User not found!");

            entity.IsActive = model.IsActive;
            entity.Password = model.Password.Trim();
            entity.RoleId = model.RoleId.Value;
            entity.Status = model.Status;
            entity.UserName = model.UserName.Trim();

            // TODO: Games

            _db.Users.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("User updated successfully.");
        }
    }
}
