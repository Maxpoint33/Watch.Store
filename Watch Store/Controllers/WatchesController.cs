using Microsoft.AspNetCore.Mvc;
using Watch_Store.Data;
using Watch_Store.Models;
using Watch_Store.Models.Entities;

namespace Watch_Store.Controllers
{
    public class WatchesController : Controller
    {
        public readonly ApplicationDbContext context;
        public WatchesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult All()
        {
            var watches = context.Watches
               .Select(w => new WatchViewModel
               {
                   Id = w.Id,
                   BrandName = w.BrandName,
                   WatchModel = w.WatchModel,
                   ReferenceNumber = w.ReferenceNumber,
                   Price = w.Price,
                   imgUrl = w.imgUrl
               }).ToList();


            return View(watches);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(WatchViewModel model)
        {
            var watch = new Watch
            {
                Id = model.Id,
                BrandName = model.BrandName,
                WatchModel = model.WatchModel,
                ReferenceNumber = model.ReferenceNumber,
                Price = model.Price,
                imgUrl = model.imgUrl,

            };
           await this.context.Watches.AddAsync(watch);
           await this.context.SaveChangesAsync();
            return RedirectToAction("All", "Watches");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var watch = context.Watches.Where(x => x.Id == id)
                .Select(w => new WatchViewModel 
                {
                    Id = id,
                    BrandName = w.BrandName,
                    WatchModel = w.WatchModel,
                    ReferenceNumber = w.ReferenceNumber,
                    Price = w.Price,
                    imgUrl = w.imgUrl,
                }).FirstOrDefault();
            var watchModel = new WatchViewModel()
            {
                Id = watch.Id,
                WatchModel = watch.WatchModel,
                BrandName = watch.BrandName,
                ReferenceNumber = watch.ReferenceNumber,
                Price = watch.Price,
                imgUrl = watch.imgUrl,

            };
            return View(watchModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WatchViewModel model)
        {
            var watch = this.context.Watches.Where(w => w.Id == model.Id).First();

            if (watch != null)
            {
                watch.BrandName = model.BrandName;
                watch.WatchModel = model.WatchModel;
                watch.ReferenceNumber = model.ReferenceNumber;
                watch.Price = model.Price;
                watch.imgUrl = model.imgUrl;
            }
           await this.context.SaveChangesAsync();

            return RedirectToAction("All", "Watches");

        }

        public IActionResult Delete(int id)
        {
            var watch = this.context.Watches.First(w => w.Id == id);

            this.context.Watches.Remove(watch);
            this.context.SaveChanges();
            return RedirectToAction(nameof(All));
        }
    }
}
