using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Async_Playground.Controllers
{
	public class HomeController : Controller
	{
			public async Task<IActionResult> Index()
			{
				return View();
			}

			[HttpPost]
			public async Task<IActionResult> RunSync()
			{
          var stopWatch = new Stopwatch();
					stopWatch.Start();
 					await LongRunningSync();
					//return RedirectToAction("Done", "Home");
					stopWatch.Stop();
					ViewData["ElapsedTime"] = stopWatch.Elapsed.TotalMilliseconds;
					return View();
			}

			[HttpPost]
			public async Task<IActionResult> RunASync()
			{
          var stopWatch = new Stopwatch();
					stopWatch.Start();
 					await LongRunningASync();
					//return RedirectToAction("Done", "Home");
					stopWatch.Stop();
					ViewData["ElapsedTime"] = stopWatch.Elapsed.TotalMilliseconds;
					return View();
			}

			public IActionResult Done()
			{
				return View();
			}


			private async Task LongRunningSync()
			{
				await Task.Run(() => {
					Thread.Sleep(2000);
				});

				await Task.Delay(2000);
				await Task.Delay(2000);
				await Task.Delay(2000);
				await Task.Delay(2000);
				await Task.Delay(2000);

				return;
			}

			private async Task LongRunningASync()
			{
				var task1 = Task.Run(() => {
					Thread.Sleep(2000);
				});

				var task2 = Task.Delay(2000);
				var task3 = Task.Delay(2000);
				var task4 = Task.Delay(2000);
				var task5 = Task.Delay(2000);
				var task6 = Task.Delay(2000);

				await Task.WhenAll(task1, task2, task3, task4, task5);

				return;
			}
	}
}