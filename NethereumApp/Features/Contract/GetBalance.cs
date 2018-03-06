using FluentValidation;
using MediatR;
using Nethereum.Web3;
using NethereumApp.Infraestructure;
using NethereumApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class GetBalance
    {
        public class Query : IRequest<Result>
        {
            public string WalletAddress { get; set; }
        }

        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                //Validações
                RuleFor(q => q.WalletAddress).NotEmpty().NotNull();
            }
        }

        public class Result
        {
            public decimal Balance { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, Result>
        {
            private readonly IEthereumService ethereumService;

            public Handler(IEthereumService ethereumService)
            {
                this.ethereumService = ethereumService;
            }

            protected override async Task<Result> HandleCore(Query query)
            {
                return new Result()
                {
                    Balance = await ethereumService.GetBalance(query.WalletAddress)
                };
            }
        }
    }
}
