using System.Net;
using System.Security.Principal;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce_Customer_Rating_API.Models
{
    public class BasicAuthenticationAttribute : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationAttribute(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            )
        : base(options, logger, encoder, clock)
        {
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }


            // Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header missing.");
            }

            
            Apps user = null;

            try
            {

                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                var userValidate = new UserValidate();

                user = await userValidate.Authenticate(username, password);

                if (!UserValidate.Login(username, password))
                {
                    return AuthenticateResult.Fail("The username or password is not correct.");
                }
            }

            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }              

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);


            return AuthenticateResult.Success(ticket);

        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            return base.HandleChallengeAsync(properties);
        }
    }
}
