using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Utils;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(RazorPagesMovieContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            Movie = movie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movieToUpdate = await _context.Movie.FirstOrDefaultAsync(m => m.Id == Movie.Id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            movieToUpdate.Title = Movie.Title;
            movieToUpdate.ReleaseDate = Movie.ReleaseDate;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.Price = Movie.Price;
            movieToUpdate.Rank = Movie.Rank;

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                movieToUpdate.ImageUri = PictureHelper.UploadNewImage(_env, HttpContext.Request.Form.Files[0]);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movie.Any(e => e.Id == Movie.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}