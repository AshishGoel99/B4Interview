using Microsoft.AspNetCore.Mvc;

namespace B4Interview.Pages
{
    public class NewInterviewModel : BaseModel
    {

        public NewInterviewModel(DatabaseContext context) : base(context)
        { }

        public void OnGet()
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            return Page();
        }
    }
}