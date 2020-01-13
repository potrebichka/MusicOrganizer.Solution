using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MusicOrganizer.Models;
using System;

namespace MusicOrganizer.Controllers
{
    public class RecordsController : Controller
    {
        [HttpGet("/records")]
        public ActionResult Index()
        {
            List<Record> allRecords = Record.GetAll();
            return View(allRecords);
        }

        [HttpGet("/records/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/records")]
        public ActionResult Create(string title, string name)
        {
            if(title == null || name == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                int result = Artist.FindArtist(name);
                Artist myArtist;
                if (result == -1)
                {
                    myArtist = new Artist(name);
                    myArtist.Save();
                }
                else 
                {
                    myArtist = Artist.GetArtist(result);
                }
                
                Record myRecord = new Record(title, myArtist);
                myArtist.AddRecord(myRecord);
                return RedirectToAction("Index");
            }
        }

        [HttpPost("/records/delete")]
        public ActionResult DeleteAll()
        {
            Record.ClearAll();
            return View();
        }

        [HttpGet("/records/{id}")]
        public ActionResult Show(int id)
        {
            Record foundRecord = Record.Find(id);
            return View(foundRecord);
        }

    }
}
