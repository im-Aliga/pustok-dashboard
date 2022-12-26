using DemoApplication.Areas.Client.ViewModels.Account;
using DemoApplication.Areas.Client.ViewModels.Account.Address;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;



namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {
          

            return View();
        }
        

        [HttpGet("orders", Name = "client-account-orders")]
        public IActionResult Orders()
        {
           

            return View();
        }

        [HttpGet("download", Name = "client-account-download")]
        public IActionResult Download()
        {


            return View();
        }

        [HttpGet("address", Name = "client-account-address")]
        public IActionResult Address()
        {
            var user = _userService.CurrentUser;

            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-add-address");
            }

            var model = new ItemViewModel
            {
                AcseptorName = address.AcseptorName,
                AcseptorLastname = address.AcseptorLastname,
                PhoneNumber = address.PhoneNumber,
                AdressName = address.AdressName,

            };

            return View(model);
        }


        [HttpGet("add-address", Name = "client-account-add-address")]
        public IActionResult AddAddress()
        {

            return View();
        }

        [HttpPost("add-address", Name = "client-account-add-address")]
        public async Task<IActionResult> AddAddress(AddAdressViewModel model)
        {
            var user = _userService.CurrentUser;

            var address = new Adress
            {
                UserId = user.Id,
                AcseptorName = model.AcseptorName,
                AcseptorLastname = model.AcseptorLastname,
                PhoneNumber = model.PhoneNumber,
                AdressName = model.AdressName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,


            };

            await _dataContext.Addresses.AddAsync(address);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("client-account-address");
        }

        [HttpGet("upate-address", Name = "client-account-update-address")]
        public IActionResult UpdateAddress()
        {
            var user = _userService.CurrentUser;

            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-add-address");
            }

            var model = new UpdateAdressViewModel
            {
                AcseptorName = address.AcseptorName,
                AcseptorLastname = address.AcseptorLastname,
                PhoneNumber = address.PhoneNumber,
                AdressName = address.AdressName,

            };

            return View(model);
        }

        [HttpPost("upate-address", Name = "client-account-update-address")]
        public async Task<IActionResult> UpdateAddress(UpdateAdressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.CurrentUser;

            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-add-address");
            }

            address.AcseptorName = model.AcseptorName;
            address.AcseptorLastname = model.AcseptorLastname;
            address.PhoneNumber = model.PhoneNumber;
            address.AdressName = model.AdressName;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-address");

        }



        [HttpGet("payment", Name = "client-account-payment")]
        public IActionResult Payment()
        {
            return View();
        }


        [HttpGet("details", Name = "client-account-details")]
        public async Task< IActionResult> DetailsAsync()
        {
            var user= _userService.CurrentUser; //kamil

            var model = new UpdateDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }
        [HttpPost("details", Name = "client-account-details")]
        public async Task<IActionResult> DetailsAsync(UpdateDetailsViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = _userService.CurrentUser; 

            if(user==null)
            {
                return NotFound();
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("client-account-details");
        }

        [HttpGet("password", Name = "client-account-change-password")]
        public  async Task<IActionResult> ChangePasswordAsync()
        {
            return View();
        }

        [HttpPost("password", Name = "client-account-change-password")]
        public async Task<IActionResult> ChangePasswordAsync(UpdatePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.CurrentUser;
            if( !BC.Verify(model.CurrentPassword,user.Password))
            {
                ModelState.AddModelError(string.Empty, "password dont match");
                return View(model);
            }
            user.Password = BC.HashPassword(model.Password);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("client-account-change-password");
        }


    }
}
