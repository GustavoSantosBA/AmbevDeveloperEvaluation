using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateSaleResponse>(result);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // GET: api/sales/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var command = new GetSaleCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            var response = _mapper.Map<GetSaleResponse>(result);
            return Ok(response);
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllSalesCommand();
            var result = await _mediator.Send(command);
            var response = _mapper.Map<GetAllSalesResponse>(result);
            return Ok(response);
        }

        // PUT: api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSaleRequest request)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;
            var result = await _mediator.Send(command);
            var response = _mapper.Map<UpdateSaleResponse>(result);
            return Ok(response);
        }

        // DELETE: api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteSaleCommand { Id = id };
            var result = await _mediator.Send(command);
            var response = _mapper.Map<DeleteSaleResponse>(result);
            return Ok(response);
        }
    }
}
