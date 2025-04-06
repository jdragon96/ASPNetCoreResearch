using Library.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public static User user = new User();

        public UserController(IUserRepository repository, IConfiguration configuration)
        {
            _userRepository = repository;
            _configuration = configuration;
        }

        // GET: api/User
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userRepository.GetAll(u => u.Id);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await _userRepository.Get(u => u.Id == id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var findUser = await _userRepository.Get(u => u.Id == id);
            if (findUser == null)
            {
                return BadRequest();
            }
            _userRepository.Update(user);
            _userRepository.Save();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.Save();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Remove(user);
            _userRepository.Save();
            return NoContent();
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostRegister(User req)
        {
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, req.UserPassword);
            req.UserPassword = hashedPassword;
            _userRepository.Add(req);
            _userRepository.Save();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> PostLogin(LoginRequest req)
        {
            if (req == null || _userRepository == null)
                return BadRequest("Server Error");
            var user = await _userRepository.Get(u => u.UserID == req.UserID);
            if (user == null)
                return BadRequest("Not Valid User");
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.UserPassword, req.UserPassword)
                == PasswordVerificationResult.Failed)
                return BadRequest("Worng Password");
            var token = new LoginResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await CreateAndSaveRefreshTokenAsync(user),
                UserModel = user
            };
            return Ok(token);
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult<LoginResponse?>> PostRefreshToken(RequestRefreshToken req)
        {
            var user = await ValidateRefreshToken(req.UserID, req.RefreshToken);
            if (user == null)
                return Unauthorized();
            var token = new LoginResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await CreateAndSaveRefreshTokenAsync(user),
            };
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserID),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescription = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        }
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<string> CreateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = CreateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddHours(6);
            _userRepository.Update(user);
            _userRepository.Save();
            return refreshToken;
        }

        private async Task<User?> ValidateRefreshToken(string userID, string refreshToken)
        {
            var user = await _userRepository.Get(u => u.UserID == userID);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpireDate <= DateTime.UtcNow)
                return null;
            return user;
        }
    }
}