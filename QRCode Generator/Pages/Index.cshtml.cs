using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QRCode_Generator.Pages
{
    public class IndexModel : PageModel
    {
        public string Texto { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string texto)
        {
            TempData["Image"] = Helper.QRCodeHelper.TextToQrCode(texto);
            return RedirectToPage("Index");
        }
    }
}
