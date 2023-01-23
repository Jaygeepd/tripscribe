using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tripscribe.Api.Authentication;
using Tripscribe.Api.ViewModels.AuthenticationRequest;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController: TripscribeBaseController
{
    private readonly IMapper _mapper;
    private readonly IAuthenticateService _service;
    public AuthenticationController(IMapper mapper, IAuthenticateService service) => 
        (_mapper, _service) = (mapper, service);
    
    [HttpPost]
    [AllowAnonymous]
    public ActionResult<AuthenticationResultViewModel> Authenticate([FromBody] AuthenticationRequestViewModel authenticationRequestViewModel)
    {
        var account =
            _service.Authenticate(authenticationRequestViewModel.Email, authenticationRequestViewModel.Password);

        if (account is null) return Unauthorized();
        
        return new AuthenticationResultViewModel
        {
            AccessToken = GenerateToken(account, 600), RefreshToken = GenerateToken(account, 18000)
        };
    }
    
    [HttpGet]
    public ActionResult<AuthenticationResultViewModel> Refresh([FromServices] IAuthorizedAccountProvider authorizedAccountProvider)
    {

        var account = authorizedAccountProvider.GetLoggedInAccount();
        
        if (account is null) return Unauthorized();
        
        return new AuthenticationResultViewModel
        {
            AccessToken = GenerateToken(account, 600), RefreshToken = GenerateToken(account, 18000)
        };
    }
    
    private string GenerateToken(AccountDTO account, int expirationTimeInMinutes)
    {
        var secretKey = Encoding.UTF8.GetBytes("JWTMySonTheDayYouWereBorn");
        var securityKey = new SymmetricSecurityKey(secretKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var expiryTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
             
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = expiryTime,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
         
        var tokenString = tokenHandler.WriteToken(jwtToken);
        return tokenString;
    }
}