using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Exceptions;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Queries.Login
{
    public class LoginQuery : IRequest<Result<TokenResponse>>
    {
        public string Userame { get; set; }
        public string Password { get; set; }
    }

    public class LoginQueryHandle : IRequestHandler<LoginQuery, Result<TokenResponse>>
    {
        private readonly ApplicationSettings _appSettings;

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IPermissionRepository _permissionRepository;

        public LoginQueryHandle(IOptions<ApplicationSettings> appSettings, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMenuRepository menuRepository, IPermissionRepository permissionRepository)
        {
            _appSettings = appSettings.Value;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<Result<TokenResponse>> Handle(LoginQuery command, CancellationToken cancellationToken)
        {
            var user = (from u in _userRepository.Entities
                        where u.Username == command.Userame
                        select u).FirstOrDefault();

            if (user == null)
            {
                throw new ApiException("Username was invalid");
            }

            var hmac = new HMACSHA512(user.PasswordSalt);
            var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
            var match = compute.SequenceEqual(user.PasswordHash);

            if (!match)
            {
                throw new ApiException("Password was invalid");
            }

            //lay ds role
            var listRoleId = _userRoleRepository.Entities.Where(ur => ur.UserId == user.UserId).Select(ur => ur.RoleId).ToList();
            var listRoleString = (from lrs in _roleRepository.Entities
                                  where listRoleId.Contains(lrs.Id)
                                  select lrs.RoleName).ToList();
            //lay ds menu
            var listMenu = (from m in _menuRepository.Entities
                            join p in _permissionRepository.Entities on m.Id equals p.MenuId
                            join ur in _userRoleRepository.Entities on p.RoleId equals ur.RoleId
                            join r in _roleRepository.Entities on ur.RoleId equals r.Id
                            where listRoleString.Contains(r.RoleName)
                            select m.Name).Distinct().ToList();
            //convert listRole listMenu => string
            var listRoleNameString = string.Join(",", listRoleString);
            var listMenuString = string.Join(",", listMenu);
            //gan vao claim
            Console.WriteLine(listRoleNameString);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Role, listRoleNameString),
                    new Claim(ClaimTypes.Anonymous, listMenuString),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            var tokenResponse = new TokenResponse() { Token = encryptedToken };
            return await Result<TokenResponse>.SuccessAsync(tokenResponse, "Login success !");
        }
    }
}