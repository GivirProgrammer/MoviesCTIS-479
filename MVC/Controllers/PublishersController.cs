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
using DataAccess.Results.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class PublishersController : Controller
    {
        // TODO: Add service injections here

        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            List<PublisherModel> publisherList = _publisherService.Query().ToList(); // TODO: Add get collection service logic here
            return View(publisherList);
        }

        // GET: Publishers/Details/5
        public IActionResult Details(int id)
        {
            PublisherModel publisher = _publisherService.Query().SingleOrDefault(p=>p.Id==id); // TODO: Add get item service logic here
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {

            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherModel publisher)
        {
            ResultBase result = _publisherService.Add(publisher);
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public IActionResult Edit(int id)
        {
            PublisherModel publisher = _publisherService.Query().SingleOrDefault(p => p.Id == id); // TODO: Add get item service logic here
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                ResultBase result = _publisherService.Update(publisher);
                if (result.isSuccessfull)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public IActionResult Delete(int id)
        {
            PublisherModel publisher = _publisherService.Query().SingleOrDefault(p=>p.Id==id); // TODO: Add get item service logic here
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ResultBase result=_publisherService.Delete(id);
            TempData["Message"]=result.Message;
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
