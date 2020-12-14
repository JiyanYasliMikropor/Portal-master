using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mikroportal.API.Services
{
    public class LoginService : ILoginService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        public LoginService(IConfiguration configuration, IMikroportalUOW mikroportalContext)
        {
            _configuration = configuration;
            context = mikroportalContext;
        }


        public LoginResponse Login(LoginRequest request)
        {
            var userFromRepo = GetUser(request);
            if (userFromRepo == null)
            {
                LoginResponse response = new LoginResponse();
                response.isSuccess = false;
                response.ErrorMessage = "Hatalı kullanıcı adı/şifre";
                return response;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:SecretKey").Value);

                var userRoles = GetUserRoles(request);
                List<RoleTypes> typeList = new List<RoleTypes>();
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

              
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, userFromRepo.UserName));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, userFromRepo.UserId.ToString()));

                foreach (var role in userRoles.RoleTypeList)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));

                    RoleTypes type = new RoleTypes();
                    type.UserId = role.UserId;
                    type.UserName = role.UserName;
                    type.RoleName = role.RoleName;
                    typeList.Add(type);
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Issuer = _configuration.GetSection("JWTSettings:Issuer").Value,
                    Expires = DateTime.Now.AddDays(int.Parse(_configuration.GetSection("JWTSettings:ExpireDays").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };


                //IPrincipal user = new ClaimsPrincipal(claimsIdentity);

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                string fullName = userFromRepo.UserName;
                int userId = userFromRepo.UserId;

                LoginResponse response = new LoginResponse();
                response.isSuccess = true;
                response.UserName = fullName;
                response.UserId = userId;
                response.Token = tokenString;
                response.UserRoles = typeList;
                return response;
            }
        }

        private string CreatePasswordHash(string password)
        {
            var salt = _configuration.GetSection("Salt").Value;
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            var passBytes = provider.ComputeHash(encoding.GetBytes(password + salt));
            password = Convert.ToBase64String(passBytes);
            return password;
        }


        private TblUsers GetUser(LoginRequest loginReq)
        {
            var hashedPass = CreatePasswordHash(loginReq.UserPassword);
            var user = context.TblUsersRepository
                .Get(x => x.UserAccount == loginReq.UserAccount && x.UserPassword == hashedPass)
                .FirstOrDefault();
            return user;
        }

        private RoleTypesResponse GetUserRoles(LoginRequest loginReq)
        {
            RoleTypesResponse resp = new RoleTypesResponse();
            List<RoleTypes> typeList = new List<RoleTypes>();
            var hashedPass = CreatePasswordHash(loginReq.UserPassword);
            var userRoles = from users in context.TblUsersRepository.Get()
                            join uRoles in context.TblUserRolesRepository.Get()
                            on users.UserId equals uRoles.UserId
                            join roles in context.TblRolesRepository.Get()
                            on uRoles.RoleId equals roles.RoleId
                            where users.UserAccount == loginReq.UserAccount && users.UserPassword == hashedPass
                            select new
                            {
                                users.UserId,
                                users.UserName,
                                roles.RoleName
                            };


            if (userRoles != null)
            {
                foreach (var item in userRoles)
                {
                    RoleTypes type = new RoleTypes();
                    type.UserId = item.UserId;
                    type.UserName = item.UserName;
                    type.RoleName = item.RoleName;
                    typeList.Add(type);
                }
                resp.isSuccess = true;
                resp.RoleTypeList = typeList;
            }
            else
                resp.isSuccess = false;

            return resp;
        }
    }
}

