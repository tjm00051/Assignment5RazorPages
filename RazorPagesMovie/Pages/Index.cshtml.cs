using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }
        public IList<Movie> Movies { get; set; } = new List<Movie>();
        public async Task OnGetAsync()
        {
            Movies = await _context.Movie
                .OrderBy(m => m.Rank)
                .ThenBy(m => m.Title)
                .ToListAsync();
        }
    }
}
