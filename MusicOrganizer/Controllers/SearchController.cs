using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MusicOrganizer.Models;

namespace MusicOrganizer.Controllers
{
  public class SearchController : Controller
  {

    [HttpPost("/search_by_artist")]
    public ActionResult New(string name)
    {
        List<Artist> result = Artist.GetArtistPartial(name);
        return View("Index", result);
    }

    [HttpGet("/search_by_artist/new")]
    public ActionResult New()
    {
        return View();
    }

    [HttpGet("/search_by_artist")]
    public ActionResult Index(List<Artist> artists)
    {

        return View(artists);
    }
  }
}