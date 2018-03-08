using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NethereumApp.Infraestructure;
using NethereumApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class ReleaseContract
    {
        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            public string Abi { get; set; }
            public string ByteCode { get; set; }
            public int Gas { get; set; }
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
            public bool Released { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db db;
            private readonly IEthereumService ethereumService;

            public Handler(Db db, IEthereumService ethereumService)
            {
                this.db = db;
                this.ethereumService = ethereumService;
            }

            protected override async Task<Result> HandleCore(Command command)
            {
                var contractInfo = await db.EthereumContractInfo.Where(e => e.Id.Equals(command.Id)).FirstOrDefaultAsync();

                if (contractInfo != null) throw new Exception("Contract alread exists");

                try
                {
                    var unlocked = await ethereumService.UnlockAccount(60);

                    if (unlocked)
                    {
                        var transactionHash = await ethereumService.DeployContract(command.Abi, command.ByteCode, command.Gas);

                        await db.EthereumContractInfo.AddAsync(new Domain.EthereumContractInfo()
                        {
                            Abi = command.Abi,
                            ByteCode = command.ByteCode,
                            TransactionHash = transactionHash
                        });

                        await db.SaveChangesAsync();

                        return new Result()
                        {
                            Released = true
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                    return new Result()
                    {
                        Released = false
                    };
                }

                return new Result()
                {
                    Released = true
                };
            }
        }
    }
}
