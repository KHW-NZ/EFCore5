using System;
using System.Linq;
using EFCore5WebApp.DAL;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFCore5WebApp.Pages.Reports
{
    public class PeopleReportModel : PageModel
    {
        private readonly AppDbContext _context;

        public PaginatedList<Person> ReportData { get; set; }        

        public PeopleReportModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10;
            var persons = from p in _context.Persons orderby p.LastName, p.FirstName select p;
            ReportData = await PaginatedList<Person>.CreateAsync(persons, pageIndex ?? 1, pageSize);
        }        
    }
}
