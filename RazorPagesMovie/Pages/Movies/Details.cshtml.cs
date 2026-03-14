using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public DetailsModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movie.FindAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}