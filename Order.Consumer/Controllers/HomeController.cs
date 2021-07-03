using Microsoft.AspNetCore.Mvc;
using Order.Domain.Models;
using Order.Infra.Mongo;
using Order.Infra.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public HomeController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        [HttpGet, Route("Home/Index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            return Ok(await _invoiceRepository.GetAll());
        }
    }
}
