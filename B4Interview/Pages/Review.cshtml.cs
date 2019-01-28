using B4Interview;
using B4Interview.DataLayer.Models;
using B4Interview.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace B4.Pages
{
    public class ReviewModel : BaseModel
    {
        private string userId;

        public ReviewModel(DatabaseContext context) : base(context) { }

        public IList<Review> Reviews { get; set; }
        public async Task OnGetAsync()
        {
            Reviews = await databaseContext.Reviews
                .Where(r => r.Company.Identifier == Search || r.Company.Name == Search)
                .Include(r => r.Tags)
                .Include(r => r.Author)
                .Include(r => r.Votes)
                .ToListAsync();
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public ReviewData Input { get; set; }

        public class ReviewData
        {
            public string CompanySearch { get; set; }
            [Required]
            public string Title { get; set; }
            [Required]
            public string AuthorInfo { get; set; }
            public string Location { get; set; }
            [Required]
            public string Description { get; set; }
            public string Pros { get; set; }
            public string Cons { get; set; }
            public string Tags { get; set; }
            public float Rating { get; set; }
            public bool Anonymous { get; set; }
        }

        #region Create
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = UserId;
            var result = databaseContext.Companies.
                Where(c => c.Name == Input.CompanySearch || c.Identifier == Input.CompanySearch);

            Company company = null;
            if (!result.Any())
            {
                company = CreateCompany(Input.CompanySearch);
            }
            else
                company = result.Single();

            var tags = new List<Tag>();
            tags.AddRange(Input.Tags
                .Split(new string[] { "," }, StringSplitOptions.None)
                .Select(tag => new Tag
                {
                    Name = tag.Trim()
                }));

            databaseContext.Reviews.Add(new Review
            {
                AutherInfo = Input.AuthorInfo + ", " + Input.Location,
                AuthorId = userId,
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                Pros = Input.Pros,
                Cons = Input.Cons,
                Rating = Input.Rating,
                Tags = tags,
                Anonymous = Input.Anonymous
            });

            databaseContext.SaveChanges();

            company.Rating = databaseContext.Reviews.Select(r => r.Rating).Average();
            company.ReviewsCount = databaseContext.Reviews.Count();

            databaseContext.SaveChanges();

            return new RedirectToPageResult("Review", new { search = Search });
        }
        #endregion

        #region UpVote
        public IActionResult OnGetUpVote(int id)
        {
            userId = UserId;
            var review = databaseContext.Reviews
                .Include(r => r.Votes)
                .First(r => r.Id == id);

            review.UpVote++;
            review.Votes.Add(new Vote
            {
                UpVote = true,
                ReviewId = id,
                VoterId = userId
            });

            databaseContext.SaveChanges();

            return new OkResult();
        }
        #endregion

        #region DownVote
        public IActionResult OnGetDownVote(int id)
        {
            userId = UserId;
            var review = databaseContext.Reviews
                .Include(r => r.Votes)
                .First(r => r.Id == id);

            review.DownVote++;
            review.Votes.Add(new Vote
            {
                UpVote = false,
                ReviewId = id,
                VoterId = userId
            });

            databaseContext.SaveChanges();

            return new OkResult();
        }
        #endregion

        #region Update
        //public async Task<IActionResult> OnPostUpdateAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Attach(Review).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReviewExists(Review.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return new OkResult();
        //}

        private bool ReviewExists(int id)
        {
            return databaseContext.Reviews.Any(e => e.Id == id);
        }
        #endregion

        #region Delete
        //public async Task<IActionResult> OnPostDeleteAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Reviews.Add(Review);
        //    await _context.SaveChangesAsync();

        //    return new OkResult();
        //}
        #endregion
    }

}
