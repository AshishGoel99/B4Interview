using B4Interview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B4.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly DatabaseContext _context;

        public ReviewModel(DatabaseContext context)
        {
            _context = context;
        }

        public IList<Review> Reviews { get; set; }
        public async Task OnGetAsync()
        {
            Reviews = await _context.Reviews
                .Where(r => r.Company.Identifier == Search || r.Company.Name == Search)
                .Include(r => r.Tags).ToListAsync();
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public Review Review { get; set; }

        #region Create
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        #endregion

        #region Update
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkResult();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        #endregion
    }
}
