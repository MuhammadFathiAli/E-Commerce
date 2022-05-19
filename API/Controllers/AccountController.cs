using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> _userManager,
            SignInManager<AppUser> _signInManager, ITokenService _tokenService, IMapper _mapper)
        {
           this.userManager = _userManager;
           this.signInManager = _signInManager;
            tokenService = _tokenService;
            mapper = _mapper;
        }
       
       [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            return  new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                role = userManager.GetRolesAsync(user).Result.SingleOrDefault()
            };
        }
       
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }
        
        [Authorize(Roles ="Admin")]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            if (user == null) return BadRequest(new ApiResponse(400,"No Address Provided"));
            return mapper.Map<Address, AddressDto>(user.Address);
        }
        
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Address = mapper.Map<AddressDto, Address>(address);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("problem updating the user");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok( new UserDto() { Email = loginDto.Email, Token = tokenService.CreateToken(user), DisplayName = user.DisplayName, gender = user.gender,image=user.image,
                role = userManager.GetRolesAsync(user).Result.FirstOrDefault() });
        }
       
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse {
                    Errors = new[] { "Email address is in Use!"}});
            }
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email,
                gender = registerDto.gender,
                image = registerDto.image
            };
            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (registerDto.Email.ToLower().Contains("admin"))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDto() { Email = registerDto.Email, DisplayName = registerDto.DisplayName, Token = tokenService.CreateToken(user), gender = registerDto.gender, image= registerDto.image,
                role = userManager.GetRolesAsync(user).Result.FirstOrDefault()};
        }

        //check if authorize for admin only 
        [Authorize]
        [HttpPut("user")]
        public async Task<ActionResult<UserDto>> UpdateUserAsync([FromQuery] string email, [FromBody]UserDto user)
        {
            var targetUser = await userManager.FindByEmailAsync(email);
            if (targetUser == null) return BadRequest(new ApiResponse(400, "Wrong Email"));
            if (email != user.Email) return BadRequest(new ApiResponse(400, "Wrong Email"));
            targetUser.Email = email;
            targetUser.DisplayName = user.DisplayName;
            targetUser.gender = user.gender;
            targetUser.image = user.image;
             await userManager.AddToRoleAsync(targetUser,user.role);
            var result = await userManager.UpdateAsync(targetUser);
            if (!result.Succeeded) return BadRequest("Couldn't be updated");
            return  Ok(user);

        }
    }
}
