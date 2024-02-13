using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.Data.TrelloModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using TrelloClone.Data.DTOs;

namespace TrelloClone.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LoginController: ControllerBase

{
     private readonly LoginService loginService;
     private IConfiguration config;

     public LoginController(LoginService loginService, IConfiguration config)
     {
         this.loginService = loginService;
         this.config = config;
     }

     [HttpPost("authenticate")]
     public async Task<IActionResult> Login(LoginDto loginDto)
     {
          var user = await loginService.GetUser(loginDto);

          if(user is null)
               return BadRequest(new {message="Invalid email or password"});
     
          string jwtToken = GenerateToken(user);
          // return token
          return Ok(new {token = jwtToken});
     


     }
     
     private string GenerateToken(Account user)
     {

          var claims = new[]
          {
               new Claim(ClaimTypes.Name, user.Username),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim("AType", user.Atype)
          };

#pragma warning disable CS8604 // Possible null reference argument.
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
#pragma warning restore CS8604 // Possible null reference argument.
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

          var securityToken = new JwtSecurityToken(
                              claims: claims,
                              expires: DateTime.Now.AddMinutes(60),
                              signingCredentials: creds
          );

          string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

          return token;
     }
     
}