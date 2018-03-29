using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotifyClone.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Results = " ";
            return View();
        }

        [HttpGet]
        [Route("GetArtist/{search}")]
        public IActionResult GetArtist(string search)
        {
            
            object ArtistInfo = new JObject();
            WebRequest.GetArtist(search, ApiResponse =>
                {
                    ArtistInfo = ApiResponse;
                    System.Console.WriteLine("=============ArtistInfo 1===", ArtistInfo);
                    ViewBag.Results = ArtistInfo;
                    
                }
            ).Wait();
            // System.Console.WriteLine("=============ArtistInfo 2===", ArtistInfo);
            
            return View("ArtistResult");
        }


        [HttpGet]
        [Route("GetAlbum/{search}")]
        public IActionResult GetAlbum(string search)
        {
            
            object AlbumInfo = new JObject();
            WebRequest.GetAlbum(search, ApiResponse =>
                {
                    AlbumInfo = ApiResponse;
                    System.Console.WriteLine("=============AlbumInfo 1===", AlbumInfo);
                    ViewBag.Results = AlbumInfo;
                    
                }
            ).Wait();
        
            return View("AlbumResult");
        }
    }
}
