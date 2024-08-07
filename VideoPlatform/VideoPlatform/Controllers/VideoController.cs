using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VideoPlatform.Models;

namespace VideoPlatform.Controllers
{
    public class VideoController : Controller
    {
        private static readonly List<VideoModel> videos = new List<VideoModel>
        {
        new VideoModel { Id = 1, Title = "Lesson 1", Description = "Description of Lesson 1", Url = "https://www.youtube.com/embed/videoid1" },
        new VideoModel { Id = 2, Title = "Lesson 2", Description = "Description of Lesson 2", Url = "https://www.youtube.com/embed/videoid2" }
        // Add more videos here
    };
        public IActionResult Index()
        {
            return View(videos);
        }

        public IActionResult Details(int id) 
        { 
            var video = videos.Find(v => v.Id == id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }
    }
}
