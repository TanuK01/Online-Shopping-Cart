using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using eShoppingZoneAPI.DTO;
using eShoppingZoneAPI.Errors;
using eShoppingZoneAPI.Extensions;
using Infrastructure.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Claims;

namespace eShoppingZoneAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

            

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
                        
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindByUserClaimsPrincipleWithAddressAsync(User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindByUserClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("Problem updating the user.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            

            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new [] { "Email Address is in use." } });
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);


            if (!result.Succeeded) {
                return BadRequest(new ApiResponse(400));
            }
            else
            {
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("subhansshukla73@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(registerDto.Email));
                mail.Subject = "Welcome to eShoppingZone " + registerDto.DisplayName;
                mail.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = registerDto.Email + "Has been sucessfully registered. We wish you happy Shopping." };

                using var SMTP = new SmtpClient();
                SMTP.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                SMTP.Authenticate("subhans.ss.73.ss@gmail.com", "rygyfxcexaooxzqt");
                SMTP.Send(mail);
                SMTP.Disconnect(true);
            }
            

            

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };

             
                        
        }

        /*public async Task<ViewResult> EmailNotify()
        {
            EmailServiceOptions options = new EmailServiceOptions
            {
                ToEmails = new List<string> { "test@gmail.com"}
            };

            await _emailService.SendTestEmail(options);

        }
        */
    }
}
