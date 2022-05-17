using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Techdome.API.Model;
using static Techdome.API.Model.Members;

namespace Techdome.API.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
		private InlineDatabaseContext DbContext;

		private readonly IConfiguration Configuration;
		public JWTManagerRepository(IConfiguration iconfiguration, InlineDatabaseContext dbContext)
		{
			Configuration = iconfiguration;
			DbContext = dbContext;
		}
		public Tokens Authenticate(Users users)
		{
			Member loggedInMember = DbContext.Config.FirstOrDefault(x => x.EmailId == users.Email && x.Password == users.Password);
			if (loggedInMember == null)
            {
				return null;
            }
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(Configuration["Auth:JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, users.Email),
					new Claim("UserId", loggedInMember.Id.ToString()),
					new Claim("Name", loggedInMember.FirstName),
					new Claim("Role", loggedInMember.Role.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			Tokens finalToken = new Tokens();
			finalToken.Token = tokenHandler.WriteToken(token);
			return finalToken;
			/*return new Tokens { Token = tokenHandler.WriteToken(token) };*/

		}
	}
}
