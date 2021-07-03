using Microsoft.AspNetCore.Mvc;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        [HttpGet, Route("Invoice/GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await _invoiceRepository.GetAll(cancellationToken));
        }

        [HttpGet, Route("Invoice/GetAllPending")]
        public async Task<IActionResult> GetAllPending(CancellationToken cancellationToken = default)
        {
            return Ok(await _invoiceRepository.GetAllPending(cancellationToken));
        }

        [HttpGet, Route("Invoice/Get")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken = default)
        {
            return Ok(await _invoiceRepository.GetById(Guid.Parse(id), cancellationToken));
        }
    }
}
