using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly MusicDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, MusicDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    user.DateJoined = System.DateTime.Today;
                    //_logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public IActionResult Logoff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [Authorize]
        public IActionResult Playlists()
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.SingleOrDefault(a => a.User.UserName == user);
            if (playlist == null)
            {
                playlist = new Playlist();
                playlist.User.UserName = user;
                _context.Playlists.Add(playlist);
                _context.SaveChanges();
                playlist = _context.Playlists.SingleOrDefault(a => a.User.UserName == user);
            }
            var list = new SelectList(_context.Albums, "AlbumID", "Title");
            ViewBag.AlbumList = list;
            return View(playlist);
        }
        [HttpPost]
        public IActionResult Playlists(Album album)
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.SingleOrDefault(a => a.User.UserName == user);
            var list = new SelectList(_context.Albums, "AlbumID", "Title");
            ViewBag.AlbumList = list;
            return View("Playlists");
        }
        public IActionResult CreatePlaylist()
        {
            ViewBag.AlbumList = new SelectList(_context.Albums, "AlbumID", "Title");
            return View();
        }
    }
}