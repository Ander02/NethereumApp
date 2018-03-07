using FluentValidation;
using MediatR;
using NethereumApp.Infraestructure;
using NethereumApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class GetContract
    {
        public class Query : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                //Validações
            }
        }

        public class Result
        {
            public Nethereum.Contracts.Contract Contract { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, Result>
        {
            private readonly Db db;
            private readonly IEthereumService ethereumService;

            public Handler(Db db, IEthereumService ethereumService)
            {
                this.db = db;
                this.ethereumService = ethereumService;
            }

            protected override async Task<Result> HandleCore(Query query)
            {
                var contractInfo = db.EthereumContractInfo.Where(e => e.Id.Equals(query.Id)).FirstOrDefault();

                if (contractInfo == null) throw new HttpException(404, $"Não foi encontrado um contrato com o id {query.Id} ");

                if (contractInfo.ContractAddress == null) throw new HttpException(404, $"Não foi encontrado um endereço nesse contrato {query.Id} ");

                var unlocked = await ethereumService.UnlockAccount(60);

                if (unlocked)
                {
                    return new Result()
                    {
                        Contract = await ethereumService.GetContract(contractInfo.Abi)
                    };
                }
                return new Result()
                {
                    Contract = null
                };
            }
        }
    }
}