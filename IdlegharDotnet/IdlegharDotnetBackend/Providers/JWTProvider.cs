using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdlegharDotnetDomain;
using IdlegharDotnetDomain.Providers;
using Microsoft.IdentityModel.Tokens;

namespace IdlegharDotnetBackend.Providers
{
    public class JWTProvider : IAuthProvider
    {
        private string SecretKey { get; set; }
        private int ExpireMinutes { get; set; }
        private string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public JWTProvider(string secretKey, int expireMinutes = 10080)
        {
            SecretKey = secretKey;
            ExpireMinutes = expireMinutes;
        }

        public string GenerateToken(User user)
        {
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetUserClaims(user)),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), Algorithm)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public string? ParseTokenEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                SecurityToken validatedToken;
                var principal = jwtSecurityTokenHandler.ValidateToken(token, GetTokenValidationParameters(), out validatedToken);
                var emailClaim = principal.FindFirst(claim => claim.Type == ClaimTypes.Email);
                if (emailClaim == null) return null;
                return emailClaim.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] bytes = new byte[SecretKey.Length * sizeof(char)];
            Buffer.BlockCopy(SecretKey.ToCharArray(), 0, bytes, 0, bytes.Length);
            return new SymmetricSecurityKey(bytes);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

        private Claim[] GetUserClaims(User user)
        {
            return new Claim[] {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };
        }
    }
}