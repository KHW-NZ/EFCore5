using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EFCore5WebApp.Core.Entities;

namespace EFCore5WebApp.Pages.Contacts
{
    [Authorize(Roles =PageAccessRoles.AllAccess)]
    public class IndexModel : SecuredPageModel
    {
        private readonly EFCore5WebApp.DAL.AppDbContext _context;

        public IndexModel(EFCore5WebApp.DAL.AppDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : base(context, signInManager, userManager)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.Persons.ToListAsync();
        }
    }
}
