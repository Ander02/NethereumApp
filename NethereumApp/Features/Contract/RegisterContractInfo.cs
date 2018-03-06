using FluentValidation;
using MediatR;
using NethereumApp.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class RegisterContractInfo
    {
        public class Command : IRequest<Result>
        {
            public string Abi { get; set; }
            public string ByteCode { get; set; }
            public string TransactionHash { get; set; }
            public string ContractAddress { get; set; }
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
            public string Abi { get; set; }
            public string ByteCode { get; set; }
            public string TransactionHash { get; set; }
            public string ContractAddress { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<Result> HandleCore(Command command)
            {
                var contractInfo = new Domain.EthereumContractInfo()
                {
                    Abi = command.Abi,
                    ByteCode = command.ByteCode,
                    ContractAddress = command.ContractAddress,
                    TransactionHash = command.TransactionHash
                };

                await db.EthereumContractInfo.AddAsync(contractInfo);
                await db.SaveChangesAsync();

                return new Result()
                {
                    Abi = contractInfo.Abi,
                    ByteCode = contractInfo.ByteCode,
                    ContractAddress = contractInfo.ContractAddress,
                    TransactionHash = contractInfo.TransactionHash
                };
            }
        }
    }
}

