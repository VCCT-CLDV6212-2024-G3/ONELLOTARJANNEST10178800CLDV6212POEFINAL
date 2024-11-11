using Microsoft.AspNetCore.Mvc;
using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models;
using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services;
using System.Diagnostics;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlobService _blobService;
        private readonly TableService _tableService;
        private readonly QueueService _queueService;
        private readonly FileService _fileService;
        private readonly QueueLogService _queueLogService;


        public HomeController(BlobService blobService, TableService tableService, QueueService queueService, FileService fileService, QueueLogService queueLogService)
        {
            _blobService = blobService;
            _tableService = tableService;
            _queueService = queueService;
            _fileService = fileService;
            _queueLogService = queueLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Loyalty()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Function()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadImg(IFormFile file)
        {
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                await _blobService.UploadBlobAsync("product-images", file.FileName, stream);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddUserProfile(UserProfile profile)
        {
            if (ModelState.IsValid)
            {
                await _tableService.AddUserProfileAsync(profile);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _tableService.AddProductAsync(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            if (!string.IsNullOrWhiteSpace(orderId))
            {
                // Send the order processing message to the Azure Queue
                await _queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");

                // Log only the order ID in SQL Server
                await _queueLogService.LogOrderAsync(orderId);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> UploadContract(IFormFile file)
        {
            if (file?.Length > 0)
            {
                using var stream = file.OpenReadStream();
                await _fileService.UploadFileAsync("contracts-logs", file.FileName, stream);
            }

            return RedirectToAction("Index");
        }

    }
}
