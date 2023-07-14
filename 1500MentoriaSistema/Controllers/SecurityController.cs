using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using _1500MentoriaSistema.Models;
using System.Security.Claims;
using System;

namespace _1500MentoriaSistema.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class SecurityController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public SecurityController(UserManager<Person> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole<int>(roleName);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(roleName);
        }

        [HttpGet]
        public IActionResult Assign()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            var model = new AssignRoleViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(string userId, string roleName, string[] claims)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user != null && role != null)
            {
                var oldRoles = await _userManager.GetRolesAsync(user);
                var oldClaims = await _userManager.GetClaimsAsync(user);

                var resultRemoveRoles = await _userManager.RemoveFromRolesAsync(user, oldRoles);
                var resultAddRoles = await _userManager.AddToRoleAsync(user, role.Name);

                if (resultAddRoles.Succeeded && resultRemoveRoles.Succeeded)
                {
                    // Remove claims to the user and role
                    foreach (var claim in oldClaims)
                    {
                        await _userManager.RemoveClaimAsync(user, claim);
                        await _roleManager.RemoveClaimAsync(role, claim);
                    }

                    // Assign claims to the user and role
                    foreach (var claim in claims)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Permission", claim));
                        await _roleManager.AddClaimAsync(role, new Claim("Permission", claim));
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in resultAddRoles.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToAction("Assign");
        }
    }
}
