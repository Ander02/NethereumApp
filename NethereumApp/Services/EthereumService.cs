using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumApp.Domain;
using NethereumApp.Infraestructure;
using NethereumApp.Util;
using System;
using Nethereum.Hex.HexTypes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;

namespace NethereumApp.Services
{
    public class EthereumService : IEthereumService
    {
        private Web3 web3;
        private string accountAdress;
        private string password;

        public Web3 Web3
        {
            get => this.web3;
        }

        public string AccountAddress
        {
            get => this.accountAdress;

            set => this.accountAdress = value;
        }

        public EthereumService(IOptions<EthereumSettings> settings)
        {
            this.web3 = new Web3(Constants.GETH_URL);
            this.AccountAddress = settings.Value.EthereumAccount;
            this.password = settings.Value.EthereumPassword;
        }

        public async Task<decimal> GetBalance(string address)
        {
            var balance = await this.web3.Eth.GetBalance.SendRequestAsync(address);
            return Web3.Convert.FromWei(balance.Value, 18);
        }

        public async Task<bool> UnlockAccount(int seconds)
        {
            return await this.web3.Personal.UnlockAccount.SendRequestAsync(this.accountAdress, this.password, seconds);
        }

        public async Task<string> DeployContract(string abi, string byteCode, int gas)
        {
            return await this.web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, this.accountAdress, new HexBigInteger(gas), 2);
        }

        public async Task<TransactionReceipt> GetTransactionReceipt(string transactionHash)
        {
            return await this.web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        }

        public Contract GetContract(string abi)
        {
            return this.web3.Eth.GetContract(abi, this.accountAdress);
        }

    }
}
