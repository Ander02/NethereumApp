using FluentValidation;
using MediatR;
using NethereumApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class ExecuteContract
    {
        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            public string ContractMethod { get; set; }
            public int Value { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validações
            }
        }

        public class Result
        {
            public string Resp { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly IEthereumService ethereumService;
            private readonly IMediator mediator;

            public Handler(IEthereumService ethereumService, IMediator mediator)
            {
                this.ethereumService = ethereumService;
                this.mediator = mediator;
            }

            protected override async Task<Result> HandleCore(Command command)
            {
                var contractAddress = (await mediator.Send(new GetContractAddress.Query()
                {
                    Id = command.Id
                })).ContractAddress;

                var contract = (await mediator.Send(new GetContract.Query()
                {
                    Id = command.Id
                })).Contract;

                if (contract == null) throw new Exception();

                var method = contract.GetFunction(command.ContractMethod);

                try
                {
                    var result = await method.SendTransactionAsync(ethereumService.AccountAddress, command.Value);

                    return new Result()
                    {
                        Resp = result.ToString()
                    };
                }
                catch (Exception e)
                {
                    return new Result()
                    {
                        Resp = "Erro " + e.Message.ToString()
                    };
                }
            }
        }


    }
}
