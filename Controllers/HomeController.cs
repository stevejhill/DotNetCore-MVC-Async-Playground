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
 				await RunDelay2SecondsMethod1();
				await RunDelay2SecondsMethod2();
				await RunDelay2SecondsMethod1();
				await RunDelay2SecondsMethod2(); 
				stopWatch.Stop();
				ViewData["ElapsedTime"] = stopWatch.Elapsed.TotalMilliseconds;
				return View();
			}

			[HttpPost]
			public async Task<IActionResult> RunASync()
			{
          		var stopWatch = new Stopwatch();
				stopWatch.Start();
 				await Task.WhenAll(RunDelay2SecondsMethod1(),RunDelay2SecondsMethod2(),RunDelay2SecondsMethod1(),RunDelay2SecondsMethod2());
				stopWatch.Stop();
				ViewData["ElapsedTime"] = stopWatch.Elapsed.TotalMilliseconds;
				return View();
			}

			public IActionResult Done()
			{
				return View();
			}
			

			private Task RunDelay2SecondsMethod1()
			{
				return Task.Delay(2000);
			}

			private Task RunDelay2SecondsMethod2()
			{
				return Task.Run(() => {
					Thread.Sleep(2000);
					});
			}
	}
}