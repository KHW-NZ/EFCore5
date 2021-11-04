using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFCore5WebApp.Core.Entities;
using EFCore5WebApp.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EFCore5WebApp.Pages.Contacts
{
    [Authorize(Roles =PageAccessRoles.AdminOnly)]
    public class CreateModel : SecuredPageModel
    {
        private readonly AppDbContext _context;

        [BindProperty(SupportsGet =true)]
        public List<SelectListItem> States { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<SelectListItem> Countries { get; set; }

        public CreateModel(AppDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : base(context, signInManager, userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Person.Addresses.Add(new Address());

            States = _context.LookUps.Where(x => x.LookUpType == LookUpType.State).Select(x => new SelectListItem { Text = x.Description, Value = x.Code }).ToList();
            Countries = _context.LookUps.Where(x => x.LookUpType == LookUpType.Country).Select(x => new SelectListItem { Text = x.Description, Value = x.Code }).ToList();

            States.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });
            Countries.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });

            return Page();
        }

        [BindProperty(SupportsGet =true)]
        public Person Person { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Persons.Add(Person);
            Person.CreatedOn = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
