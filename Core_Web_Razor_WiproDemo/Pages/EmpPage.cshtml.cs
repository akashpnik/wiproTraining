using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core_Web_Razor_WiproDemo.Pages
{
    public class EmpPageModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; }
        public string phone { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {
        }
        public void OnPost()
        {
            // These properties are automatically populated from the form
            Message = $"Thanks {Id} {Name}, we received your message!";
        }
    }
}
