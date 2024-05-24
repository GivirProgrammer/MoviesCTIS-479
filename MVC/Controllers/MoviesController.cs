#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        // TODO: Add service injections here
        private readonly IMovieService _movieService;
        private readonly IPublisherService _publisherService;
        private readonly IUserService _userService;

        public MoviesController(IMovieService movieService, IPublisherService publisherService, IUserService userService)
        {
            _movieService = movieService;
            _publisherService = publisherService;
            _userService = userService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.GetList(); // TODO: Add get collection service logic here
            return View("List",movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.GetItem(id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return View("Error", $"Movie with ID {id} not found!");
            }
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["PublisherId"] = new SelectList(_publisherService.Query().ToList(),"Id","Name");
            ViewBag.Users=new MultiSelectList(_userService.GetList(),"Id","UserName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result=_movieService.Add(movie);

                if (result.isSuccessfull)
                {
                    return RedirectToAction(nameof(Details), new { id = movie.Id });
                }
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["PublisherId"] = new SelectList(_publisherService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            MovieModel movie = _movieService.GetItem(id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return View("Error", $"Game with ID {id} not found!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["PublisherId"] = new SelectList(_publisherService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieModel movie)
        {
            if(ModelState.IsValid)
            {
                var result=_movieService.Update(movie);
                if (result.isSuccessfull)
                {
                    return RedirectToAction(nameof(Details), new { id = movie.Id });
                }
                ViewData["PublisherId"] = new SelectList(_publisherService.Query().ToList(), "Id", "Name");
                ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            }
                return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
