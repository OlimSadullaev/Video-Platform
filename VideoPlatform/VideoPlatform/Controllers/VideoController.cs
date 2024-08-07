﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VideoPlatform.DataDB;
using VideoPlatform.Models;

namespace VideoPlatform.Controllers
{
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext context;
        public VideoController(ApplicationDbContext context)
        {
            this.context = context;   
        }

        /*private static readonly List<VideoModel> videos = new List<VideoModel>
        {
        new VideoModel { Id = 1, Title = "Lesson 1", Description = "Description of Lesson 1", Url = "https://www.youtube.com/embed/videoid1" },
        new VideoModel { Id = 2, Title = "Lesson 2", Description = "Description of Lesson 2", Url = "https://www.youtube.com/embed/videoid2" }
        };*/

        public IActionResult Index()
        {
            var videos = context.Videos.ToList();
            return View(videos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VideoModel video)
        {
            if (ModelState.IsValid)
            {
                context.Add(video);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VideoModel video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(video);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await context.Videos.FindAsync(id);
            context.Videos.Remove(video);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return context.Videos.Any(e => e.Id == id);
        }



        /*public IActionResult Details(int id) 
        { 
            var video = videos.Find(v => v.Id == id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }*/
    }
}
