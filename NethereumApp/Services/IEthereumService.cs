using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Services
{
    public interface IEthereumService
    {
        string AccountAddress { get; set; }

        Task<decimal> GetBalance(string address);
        Task<bool> ReleaseContract(string name, string abi, string byteCode, int gas);
        Task<string> TryGetContractAddress(string name);
        Task<Contract> GetContract(string name);
    }
}
