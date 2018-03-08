using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NethereumApp.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Features.Contract
{
    public class SearchAllContractInfo
    {
        public class Query : IRequest<List<Result>>
        {

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
            public int Id { get; set; }
            public string Abi { get; set; }
            public string ByteCode { get; set; }
            public string TransactionHash { get; set; }
            public string ContractAddress { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, List<Result>>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            protected override async Task<List<Result>> HandleCore(Query command)
            {
                return (await db.EthereumContractInfo.ToListAsync()).Select(e => new Result()
                {
                    Id = e.Id,
                    Abi = e.Abi,
                    ByteCode = e.ByteCode,
                    ContractAddress = e.ContractAddress,
                    TransactionHash = e.TransactionHash
                }).ToList();
            }
        }
    }
}
