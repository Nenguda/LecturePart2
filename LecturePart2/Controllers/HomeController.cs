

using LecturePart2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace LecturePart2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<Lecturer> Lecturers = new List<Lecturer>();
        private static List<Claim> Claims = new List<Claim>();
        private static List<ProgramCoordinator> Coordinators = new List<ProgramCoordinator>();
        private static List<ProgramManager> Managers = new List<ProgramManager>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Landing page
        public IActionResult Index()
        {
            return View();
        }

       
        // Privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Register Lecturer
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                Lecturers.Add(lecturer);
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }
            return View(lecturer);
        }

        // Login Lecturer
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var lecturer = Lecturers.SingleOrDefault(l => l.Email == email && l.Password == password);
            if (lecturer != null)
            {
                HttpContext.Session.SetInt32("LecturerId", lecturer.LecturerId);
                return RedirectToAction("LecturerDashboard");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        // Lecturer Dashboard
        public IActionResult LecturerDashboard()
        {
            int? lecturerId = HttpContext.Session.GetInt32("LecturerId");
            if (lecturerId == null) return RedirectToAction("Login");

            var lecturer = Lecturers.SingleOrDefault(l => l.LecturerId == lecturerId.Value);
            if (lecturer != null)
            {
                ViewBag.Claims = Claims.Where(c => c.LecturerId == lecturer.LecturerId).ToList();
                return View(lecturer);
            }
            return RedirectToAction("Login");
        }

        // Apply for Claim
        [HttpGet]
        public IActionResult ApplyForClaim(int id)
        {
            ViewBag.LecturerId = id;
            return View();
        }

        [HttpPost]
        public IActionResult ApplyForClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.SubmissionDate = DateTime.Now;
                claim.Status = "Submitted";
                Claims.Add(claim);
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("LecturerDashboard");
            }
            return View(claim);
        }

        // Program Coordinator Claim Approval
        [HttpGet]
        public IActionResult CoordinatorApproval()
        {
            ViewBag.Claims = Claims.Where(c => c.Status == "Submitted").ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CoordinatorApproval(int claimId, string action)
        {
            var claim = Claims.SingleOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                if (action == "Approve")
                {
                    claim.Status = "Approved by Coordinator";
                    TempData["SuccessMessage"] = "Claim approved by Coordinator!";
                }
                else if (action == "Deny")
                {
                    claim.Status = "Denied by Coordinator";
                    TempData["ErrorMessage"] = "Claim denied by Coordinator!";
                }
            }
            return RedirectToAction("CoordinatorApproval");
        }

        // Program Manager Claim Approval and Payment
        [HttpGet]
        public IActionResult ManagerApproval()
        {
            ViewBag.Claims = Claims.Where(c => c.Status == "Approved by Coordinator").ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ManagerApproval(int claimId, string action)
        {
            var claim = Claims.SingleOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                if (action == "Pay")
                {
                    claim.Status = "Paid";
                    TempData["SuccessMessage"] = "Claim paid!";
                }
            }
            return RedirectToAction("ManagerApproval");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}