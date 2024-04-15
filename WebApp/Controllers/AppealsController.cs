using App.Domain;
using App.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AppealsController(IAppeals appeals) : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            var appealss = appeals.GetAppeals().Where(a => !a.IsDone).ToList(); 
            foreach (var appeal in appealss)
            {
                var dtNow = DateTime.Now;
                appeal.Color = appeal.DeadlineDate < dtNow || (appeal.DeadlineDate - dtNow).TotalHours < 1.0
                    ? "color:red"
                    : "color:black";
            }

            AppealsViewModel model = new()
            {
                Appeals = appealss
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AppealsViewModel viewModel)
        {
            var appeal = new Appeal
            {
                Description = viewModel.Description,
                EntryDate = DateTime.Now, 
                DeadlineDate = viewModel.DeadlineDate,
                Color = "color:black"
            };

            appeals.AddAppeal(appeal);

            appeals.SortDeadlineDate();

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult MarkAsDone(Guid appealId)
        {
            var appeal = appeals.GetAppeals().FirstOrDefault(a => a.Id == appealId);
            if (appeal == null) return RedirectToAction("List");

            if (appeal.IsDone) return RedirectToAction("List");
            appeal.IsDone = true;
            appeal.DoneDate = DateTime.Now; // Set the done date when marking as done

            return RedirectToAction("List");
        }


        [HttpPost]
        public IActionResult Remove(Guid appealId)
        {
            var appeal = appeals.GetAppeals().FirstOrDefault(a => a.Id == appealId);
            if (appeal == null) return RedirectToAction("List");
            appeals.DeleteAppeal(appealId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult DoneList()
        {
            var doneAppeals = appeals.GetAppeals().Where(a => a.IsDone).ToList();
            AppealsViewModel model = new()
            {
                Appeals = doneAppeals,
                DoneAppealsCount = doneAppeals.Count 
            };
            return View(model);
        }

        
    }
}