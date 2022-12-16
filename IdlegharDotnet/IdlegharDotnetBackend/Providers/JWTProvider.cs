namespace IdlegharDotnetBackend;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JWTProvider : IAuthProvider
{
    private string SecretKey { get; set; }
    private int ExpireMinutes { get; set; }
    private string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

    public JWTProvider(string secretKey, int expireMinutes = 10080)
    {
        SecretKey = secretKey;
        if (secretKey.Length < (32))
        {
            throw new ArgumentException($"The key must have at least {32} characters");
        }
        ExpireMinutes = expireMinutes;
    }

    public string GenerateToken(User user)
    {
        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetUserClaims(user)),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(this.ExpireMinutes)),
            SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), Algorithm)
        };

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = jwtSecurityTokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    public string? ParseTokenEmail(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        try
        {
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
        byte[] symmetricKey = Convert.FromBase64String(SecretKey);
        return new SymmetricSecurityKey(symmetricKey);
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