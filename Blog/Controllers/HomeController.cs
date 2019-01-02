using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.Repository;
using Blog.Data.FileManager;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(IRepository repo,IFileManager fileManager)
        {this._repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPost();
            return View(posts);

        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);

        }

        [HttpGet("/Image/{Image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(".")+1);
            return new FileStreamResult(_fileManager.imageStream(image),$"image/{mime}");

        }


    }
}
