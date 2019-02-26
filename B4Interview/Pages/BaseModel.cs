using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;
using System.Security.Claims;

namespace B4Interview.Pages
{
    public class BaseModel : PageModel
    {
        protected readonly DatabaseContext databaseContext;

        public ViewDataDictionary PagingViewData { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int Index { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        public string UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                return null;
            }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public BaseModel(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        protected Company CreateCompany(string name)
        {
            var company = new Company
            {
                Name = name
            };

            databaseContext.Companies.Add(company);

            var result = databaseContext.SaveChanges();
            return company;
        }


        // below is a basic life-cycle hook
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
        }

        public IQueryable<T> GetPagedData<T>(IOrderedQueryable<T> records)
        {
            if (PageSize == 0)//this is default
            {
                PageSize = 12;
            }

            var remains = records.Count() - (PageSize * (Index + 1));
            var remainingPagesCount = remains > 0 ? remains / PageSize : 0;

            if (remains > 0 && remains % PageSize != 0)
            {
                remainingPagesCount++;
            }

            PagingViewData = new ViewDataDictionary(ViewData)
             {
                 { "Index", Index },
                 { "Size", PageSize },
                 { "RemainingPagesCount", remainingPagesCount },
                 { "Page", RouteData.Values["page"].ToString() }
             };

            return records
                .Skip(Index * PageSize)
                .Take(PageSize);
        }
    }
}
