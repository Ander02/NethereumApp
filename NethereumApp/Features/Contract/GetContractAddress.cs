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
    public class GetContractAddress
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
            public string ContractAddress { get; set; }
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

                if (!String.IsNullOrEmpty(contractInfo.ContractAddress))
                {
                    return new Result()
                    {
                        ContractAddress = contractInfo.ContractAddress
                    };
                }
                else
                {
                    var unlocked = await ethereumService.UnlockAccount(60);

                    if (unlocked)
                    {
                        var receipt = await ethereumService.GetTransactionReceipt(contractInfo.TransactionHash);

                        if (receipt != null)
                        {
                            contractInfo.ContractAddress = receipt.ContractAddress;
                            await db.SaveChangesAsync();

                            return new Result()
                            {
                                ContractAddress = contractInfo.ContractAddress
                            };
                        }
                    }
                }
                throw new HttpException(400, $"Não foi possível recuperar contractAddress");
            }
        }
    }
}
