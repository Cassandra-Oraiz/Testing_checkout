using Backend.Backend.Configuration;
using Backend.Backend.DTOs;
using Backend.Backend.Interface.ServiceInterface;
using Backend.Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Backend.Service
{
    /// <summary>
    /// Service responsible for handling authentication logic such as user login.
    /// </summary>
    /// <remarks>
    /// This service validates user credentials, retrieves claims, 
    /// and generates a JWT token upon successful authentication.
    /// </remarks>
    public class AuthService(UserRepository userRepository, ClaimService claimService, JwtService jwtService) : IAuthService // for primary constructor, will practice more on primary constructor as per release of C# 12
    {
        /// <summary>
        /// Repository used to access user data from the database.
        /// </summary>
        private readonly UserRepository _userRepository  = userRepository;

        /// <summary>
        /// Service used to generate claims for the authenticated user.
        /// </summary>
        private readonly ClaimService _claimService = claimService;

        /// <summary>
        /// Service used to generate JWT tokens.
        /// </summary>
        private readonly JwtService _jwtService = jwtService;

        /// <summary>
        /// Authenticates a user using email/username and password.
        /// </summary>
        /// <param name="login">
        /// DTO containing login credentials (Email/Username and Password).
        /// </param>
        /// <returns>
        /// A <see cref="LoginResult"/> object indicating success or failure,
        /// along with a JWT token if authentication is successful.
        /// </returns>
        /// <remarks>
        /// Process flow:
        /// 1. Retrieve user by email or username.
        /// 2. Validate if user exists.
        /// 3. Verify password using BCrypt hashing.
        /// 4. Generate claims for the user.
        /// 5. Generate JWT token using claims.
        /// 6. Return login result with token.
        /// </remarks>
        public async Task<LoginResult> LoginAsync(LoginUserDto login)
        {
            var user = await _userRepository.GetByEmailOrUsernameAsync(login.Email);

            // User Validation
            if (user == null) return new LoginResult
            {
                isSuccess = false,
                Token = null,
                Detail = "User Cannot Be Found"
            };
            // Password validation
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PassHash))
                return new LoginResult
                {
                    isSuccess = false,
                    Token = null,
                    Detail = "Password Incorrect"
                };

            // collect user's identity
            var claims = await _claimService.GetClaimsAsync(user);

            var token = _jwtService.GenerateToken(claims);

            return new LoginResult
            {
                isSuccess = true,
                Token = token,
                Detail = $"Welcome Back {user.Full_Name}"
            };
        }
    }
}
