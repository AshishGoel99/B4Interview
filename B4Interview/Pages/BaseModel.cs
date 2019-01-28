﻿using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;

namespace B4Interview.Pages
{
    public class BaseModel : PageModel
    {
        protected readonly DatabaseContext databaseContext;

        protected string UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                    return User.FindFirstValue(ClaimTypes.NameIdentifier);
                return null;
            }
        }

        public BaseModel(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        protected Company CreateCompany(string name)
        {
            var newId = databaseContext.Companies.Max(c => c.Id) + 1;
            var company = new Company
            {
                Id = newId,
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
    }
}
