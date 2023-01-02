using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HiddenVilla_Web_Api.Helper;
using HiddenVilla_Web_Api.Model;
using HiddenVillaServer;
using HiddenVillaServer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HiddenVilla_Web_Api.Controllers;
[Route("api/[controller]/[action]")] 
[ApiController]
// [Authorize]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly APISettings _apiSettings;
    // GET
    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IOptions<APISettings> options,
        RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _apiSettings = options.Value;
    }
    
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDto)
    {
        if (userRequestDto == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = new IdentityUser(userName: userRequestDto.Name )
        {
            UserName = userRequestDto.Email,
            Email = userRequestDto.Email,
            PhoneNumber = userRequestDto.PhoneNo,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, userRequestDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO
            {
                Errors = errors,
                IsRegisterSuccessfull = false
            });
        }

        var roleresult = await _userManager.AddToRoleAsync(user, SD.Customer);
        if (!roleresult.Succeeded)
        {
            var errors = roleresult.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO
            {
                Errors = errors,
                IsRegisterSuccessfull = false
            });
        }

        return StatusCode(200);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDto)
    {
        var result = await _signInManager.PasswordSignInAsync(authenticationDto.UserName, authenticationDto.Password,false,false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(authenticationDto.UserName);
            if (user == null)
            {
                return Unauthorized(new AuthResponseDTO
                {
                    IsAuthSuccessful = false,
                    Errors = "Invalid Authentications error"
                });
            }

            var signInCredentials = GetSigningCredentials();
            var claims=await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: _apiSettings.ValidIssuer,
                audience:_apiSettings.ValidAudience,
                claims:claims,
                expires:DateTime.Now.AddDays(30),
                signingCredentials:signInCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDTO
            {
                IsAuthSuccessful = true,
                Token = token,
                UserDto = new UserDTO
                {
                    Name = user.Email.ToString().Split("@")[0],
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNo = user.PhoneNumber
                }
            });
        }
        else
        {
            return Unauthorized(new AuthResponseDTO()
            {
                IsAuthSuccessful = false,
                Errors = "Invalid Authentication"
            });
        }

       
    }
    private SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Id", user.Id),
        };
        var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }

        return claims;
    }


}