using Educational.Core.Models;
using Educational.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Educational.Core.Dtos.Auth;
using Educational.Core.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Educational.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        RoleManager<IdentityRole> roleManager,
        AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;
        }



        [HttpPost("signup")]
        public async Task<ActionResult<AuthenticateResponse>> SignUp(RegisterModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return BadRequest(new AuthenticateResponse
                    {
                        Success = false,
                        Message = "Passwords do not match"
                    });
                }

                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return BadRequest(new AuthenticateResponse
                    {
                        Success = false,
                        Message = "User with this email already exists"
                    });
                }
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName =model.Name,
                    Role = model.Role ?? "Student",
                    EmailConfirmed = true 
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest(new AuthenticateResponse
                    {
                        Success = false,
                        Message = $"User creation failed: {errors}"
                    });
                }
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                
                await _userManager.AddToRoleAsync(user, model.Role);

                if (model.Role == "Student" && !string.IsNullOrEmpty(model.Email))
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Level = 1,
                    };
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                var token = GenerateJwtTokenAsync(user);

                return Ok(new AuthenticateResponse
                {
                    Success = true,
                    Message = "User registered successfully",
                    Token = await token,
                    UserId = user.Id,
                    Email = user.Email,
                    FullName = user.UserName,
                    Role =user.Role
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new  AuthenticateResponse
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthenticateResponse>> SignIn(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized(new AuthenticateResponse
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    });
                }

                
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault() ?? user.Role;
                    var token = GenerateJwtTokenAsync(user);

                    return Ok(new AuthenticateResponse
                    {
                        Success = true,
                        Message = "Signed in successfully",
                        Token = await token,
                        UserId = user.Id,
                        Email = user.Email,
                        Role = userRole
                    });
                }

               

                return Unauthorized(new AuthenticateResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthenticateResponse
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }





        private async Task<string> GenerateJwtTokenAsync(User user)  // Make it async
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not configured"));

            // FIX: Use await instead of .Result
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("fullName", $"{user.FirstName} {user.LastName}".Trim()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Add unique token ID
             };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("signout")]
        [Authorize]
        public async Task<ActionResult<AuthenticateResponse>> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new AuthenticateResponse
            {
                Success = true,
                Message = "Signed out successfully"
            });
        }
    }
}
