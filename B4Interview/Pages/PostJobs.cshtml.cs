using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace B4Interview.Pages
{
    public class PostJobsModel : BaseModel
    {
        public PostJobsModel(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }
            public string Department { get; set; }
            [Required]
            public string Location { get; set; }
            [Required]
            public string Skills { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public string Company { get; set; }
            public string Experience { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = UserId;

            var company = databaseContext.Companies
                .Where(c => c.Name.ToUpper().Contains(Input.Company) || c.Identifier.ToUpper().Contains(Input.Company));

            int companyId = 0;
            if (company.Any())
            {
                companyId = company.Single().Id;
            }
            else
            {
                companyId = CreateCompany(Input.Company).Id;
            }

            var skills = new List<Skill>();

            var postedSkills = Input.Skills.Split(',').Select(s => s.Trim());

            var skillInDb = databaseContext.Skills.Where(s => postedSkills.Select(sk => sk.ToUpper()).Contains(s.Name.ToUpper()));
            if (skillInDb.Any())
            {
                skills.AddRange(skillInDb);
            }

            foreach (var skill in postedSkills)
            {
                if (skills.Any(s => s.Name.ToUpper() == skill.ToUpper()))
                {
                    continue;
                }

                skills.Add(new Skill
                {
                    Name = skill
                });
            }

            var newJob = new Job()
            {
                CompanyId = companyId,
                Description = Input.Description,
                Location = Input.Location,
                PostedOn = DateTime.Now,
                Skills = skills,
                Experience = Input.Experience,
                ReferrerId = userId,
                Title = Input.Title
            };

            databaseContext.Jobs.Add(newJob);
            databaseContext.SaveChanges();

            StatusMessage = "Job saved Successfully";
            return Page();
        }
    }
}