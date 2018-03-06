using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    [Route("/[controller]")]
    public class ContractInfoController : Controller
    {
        private IMediator mediator;

        public ContractInfoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<SearchAllContractInfo.Result>> SearchManyEquipments([FromQuery] SearchAllContractInfo.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [Route("1")]
        [HttpGet]
        public async Task<GetBalance.Result> GetBalance([FromQuery] GetBalance.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

    }
}
