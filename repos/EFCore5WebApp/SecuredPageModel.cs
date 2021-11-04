using EFCore5WebApp.DAL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFCore5WebApp
{
    public class SecuredPageModel : PageModel, IAsyncPageFilter
    {
        private AppDbContext _context;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public bool IsAdminUser { get; set; }
        public bool IsViewOnlyUser { get; set; }
        public bool IsAllAllowedUser => IsAdminUser || IsViewOnlyUser;

        public SecuredPageModel(AppDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            bool isSignedIn = _signInManager.IsSignedIn(User);
            bool isAdminUser = isSignedIn && await _userManager.IsInRoleAsync(user, Roles.AdminRoleName);
            bool isViewOnlyUser = isSignedIn && await _userManager.IsInRoleAsync(user, Roles.ViewOnlyRoleName);

            await next.Invoke();
        }
    }
}
