using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
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
        Task<bool> UnlockAccount(int seconds);
        Task<string> DeployContract(string abi, string byteCode, int gas);
        Task<TransactionReceipt> GetTransactionReceipt(string transactionHash);
        Task<Contract> GetContract(string abi);

    }
}
