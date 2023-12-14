using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Repairs
{
    public class ShowRepairModel : PageModel
    {
        private IRepairRepository _repairRepository;
        public ShowRepairModel(IRepairRepository repairRepository)
        {
            _repairRepository = repairRepository;
        }
        public Repair Repair { get; set; }
        public IActionResult OnGet(int id)
        {
            Repair = _repairRepository.GetRepair(id);
            return Page();
        }
    }
}
