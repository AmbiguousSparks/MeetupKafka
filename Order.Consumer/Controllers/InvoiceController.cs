using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Order.Application.Requests;
using Order.Consumer.Models;
using Order.Domain.Models;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Controllers
{
    [EnableCors("DEFAULT")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediatr;
        public InvoiceController(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediatr = mediator;
        }
        [HttpGet, Route("api/Product/GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(new ResponseBuilder<IList<Invoice>>().Build(await _invoiceRepository.GetAllDecided(cancellationToken)));
            }catch(OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        [HttpGet, Route("api/Product/GetAllPending"), Authorize]
        public async Task<IActionResult> GetAllPending(CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(new ResponseBuilder<IList<Invoice>>().Build(await _invoiceRepository.GetAllPending(cancellationToken)));
            }catch(OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        [HttpGet, Route("api/Product/Get")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                Guid guidId = Guid.Empty;
                if (!Guid.TryParse(id, out guidId))
                    throw new ArgumentException("Id is not valid!");
                return Ok(new ResponseBuilder<Invoice>().Build(await _invoiceRepository.GetById(guidId, cancellationToken)));
            }
            catch (OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        [HttpPost, Route("api/Product/UpdateStatus"), Authorize]
        public async Task<IActionResult> UpdateStatusInvoice([FromBody] UpdateInvoiceStatusRequest inStatusUpdate, CancellationToken cancellationToken = default)
        {
            try
            {
                var invoice = await _mediatr.Send(inStatusUpdate, cancellationToken);
                return Ok(new ResponseBuilder<Invoice>().Build(invoice));
            }
            catch (OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }
    }
}
