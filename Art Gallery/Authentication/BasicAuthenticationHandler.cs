using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Art_Gallery.Models;
using Art_Gallery.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Art_Gallery.Authentication;


public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{

    private readonly IUserDataAccess _userRepo;
    
    public
        BasicAuthenticationHandler(IUserDataAccess userRepo,  IOptionsMonitor<AuthenticationSchemeOptions>
            options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) :
        base(options, logger, encoder, clock)

    {

        _userRepo = userRepo;

    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        
        base.Response.Headers.Add("WWW-Authenticate", @"Basic realm=""Access to the Art Gallery controller.""");
        var authHeader = Request.Headers["Authorization"].ToString();
        

        if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
        {

            string token = authHeader.Substring("Basic ".Length).Trim();
            var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialsAsEncodedString.Split(new[] {':'});

           
            var email = credentials[0];
            var password = credentials[1];
            var user = _userRepo.GetUsers().FirstOrDefault(x => x.Email == email);

            
            if (user == null)
            {
                   
                Response.StatusCode = 401;
                return Task.FromResult(AuthenticateResult.Fail($"User with that email doesn't exist."));

            }
            
            
            //user.Email = email;
            var hasher = new PasswordHasher<User>();
            //user.PasswordHash = hasher.HashPassword(user, password);
            
            
            var pwVerificationResult = hasher.VerifyHashedPassword(user,user.PasswordHash, password);
            
            if (pwVerificationResult == PasswordVerificationResult.Success)
            {
                //var login = new LoginModel(email, password);
                    
                    
                var claims = new []
                {
                     
                    new Claim(ClaimTypes.Role, user.Role)
                };


                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var authTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(authTicket));
            }
            
            Response.StatusCode = 401;
            return Task.FromResult(AuthenticateResult.Fail($"User with that email or password doesn't exist."));

                
        }
        
            
        Response.StatusCode = 401;
        return Task.FromResult(AuthenticateResult.Fail("User doesn't exist."));
                
                
            
    }

            
  

}