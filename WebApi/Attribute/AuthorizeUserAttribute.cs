using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence.Context;
using System.Security.Claims;

namespace WebApi.Attribute;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private string Menu { get; set; }

    public AuthorizeUserAttribute(string menu)
    {
        Menu = menu;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

        var hasClaim = context.HttpContext.User.Claims.Any();

        var menuClaim = context.HttpContext.User.Claims
            .Where(claim => claim.Type == ClaimTypes.Anonymous)
            .Select(c => c.Value).ToList();
        var listOfMenu = new List<string>(menuClaim[0].Split(','));

        foreach (var menu in listOfMenu)
        {
            if (hasClaim)
            {
                if (Menu == menu)
                {
                    //var a = context.HttpContext.User.HasClaim(ClaimTypes.Role, "Admin");

                    var roleClaim = context.HttpContext.User.Claims
                        .Where(claim => claim.Type == ClaimTypes.Role)
                        .Select(c => c.Value).ToList();
                    var listOfRoleId = new List<string>(roleClaim[0].Split(','));
                    Console.WriteLine(roleClaim);
                    var listPermission = (from p in dbContext.Permissions
                                          join m in dbContext.Menus on p.MenuId equals m.Id
                                          join ur in dbContext.UserRoles on p.RoleId equals ur.RoleId
                                          join us in dbContext.Users on ur.UserId equals us.UserId
                                          join r in dbContext.Roles on ur.RoleId equals r.Id
                                          where listOfRoleId.Contains(r.RoleName) && m.Name == menu
                                          select new Permission()
                                          {
                                              Id = p.Id,
                                              Name = p.Name,
                                              CanAccess = p.CanAccess,
                                              CanAdd = p.CanAdd,
                                              CanDelete = p.CanDelete,
                                              CanEdit = p.CanEdit,
                                              IsDeleted = p.IsDeleted,
                                              MenuId = m.Id,
                                              RoleId = r.Id
                                          }).ToList();
                    foreach (var item in listPermission)
                    {
                        switch (context.HttpContext.Request.Method)
                        {
                            case "GET":
                                {
                                    if (item.CanAccess)
                                        return;
                                    break;
                                }
                            case "POST":
                                break;
                        }
                    }
                }

                context.Result = new ForbidResult();
                return;
            }

            context.Result = new ChallengeResult();
            return;
        }
    }
}