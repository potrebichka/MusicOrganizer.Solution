using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MusicOrganizer.Models;

namespace MusicOrganizer.Controllers
{
  public class ArtistsController : Controller
  {
    [HttpGet("/artists")]
    public ActionResult Index()
    {
      List<Artist> allArtists = Artist.GetAll();
      return View(allArtists);
    }


    [HttpGet("/artists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Artist selectedArtist = Artist.Find(id);
      List<Record> artistRecords = selectedArtist.GetListOfRecords();
      model.Add("artist", selectedArtist);
      model.Add("records", artistRecords);
      return View(model);
    }



    // // This one creates new Items within a given Artist, not new Artists:
    // [HttpPost("/artists/{artistId}/records")]
    // public ActionResult Create(int artistId, string itemDescription)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Artist foundArtist = Artist.Find(artistId);
    //   Record newRecord = new Record(t);
    //   foundArtist.AddRecord(newRecord);
    //   List<Record> artistRecords = foundArtist.Records;
    //   model.Add("Records", artistRecords);
    //   model.Add("artist", foundArtist);
    //   return View("Show", model);
    // }
  }
}