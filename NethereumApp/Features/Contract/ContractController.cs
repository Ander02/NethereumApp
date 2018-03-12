using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    [Route("/[controller]")]
    public class ContractController : Controller
    {
        private IMediator mediator;

        private const string abi = @"[{""constant"":false,""inputs"":[{""name"":""add"",""type"":""uint256""}],""name"":""addCoins"",""outputs"":[{""name"":""b"",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""add"",""type"":""uint256""}],""name"":""subtractCoins"",""outputs"":[{""name"":""b"",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""balance"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""inputs"":[{""name"":""initial"",""type"":""uint256""}],""payable"":false,""type"":""constructor""}]";
        private const string byteCode = "0x6060604052341561000c57fe5b604051602080610185833981016040528080519060200190919050505b806000819055505b505b610143806100426000396000f30060606040526000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680630173e3f41461005157806349fb396614610085578063b69ef8a8146100b9575bfe5b341561005957fe5b61006f60048080359060200190919050506100df565b6040518082815260200191505060405180910390f35b341561008d57fe5b6100a360048080359060200190919050506100f8565b6040518082815260200191505060405180910390f35b34156100c157fe5b6100c9610111565b6040518082815260200191505060405180910390f35b600081600054019050806000819055508090505b919050565b600081600054039050806000819055508090505b919050565b600054815600a165627a7a723058200085d6d7778b3c30ba2e3bf4af4c4811451f7367109c1a9b44916d876cb67c5c0029";
        private const int gas = 21000;

        public ContractController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("/balance/{walletAddress}")]
        public async Task<GetBalance.Result> GetBalance([FromRoute] GetBalance.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpPost]
        [Route("/releaseContract")]
        public async Task<ReleaseContract.Result> ReleaseContract([FromBody] ReleaseContract.Command command)
        {
            command.Abi = abi;
            command.ByteCode = byteCode;
            command.Gas = gas;

            var result = await mediator.Send(command);

            return result;
        }

        [HttpGet]
        [Route("/contract/{id}")]
        public async Task<GetContract.Result> GetContract([FromRoute] GetContract.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpGet]
        [Route("/contractAddress/{id}")]
        public async Task<GetContractAddress.Result> GetContractAddress([FromRoute] GetContractAddress.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpPost]
        [Route("/contract")]
        public async Task<RegisterContractInfo.Result> RegisterContract([FromRoute] RegisterContractInfo.Command command)
        {
            var result = await mediator.Send(command);

            return result;
        }

        [HttpGet]
        [Route("/contract")]
        public async Task<List<SearchAllContractInfo.Result>> SearchAllContractInfo([FromRoute] SearchAllContractInfo.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpPost]
        [Route("/executeContract")]
        public async Task<ExecuteContract.Result> ExecuteContract([FromBody] ExecuteContract.Command command)
        {
            var result = await mediator.Send(command);

            return result;
        }
    }
}
