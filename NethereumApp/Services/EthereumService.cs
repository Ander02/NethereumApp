﻿using Microsoft.Extensions.Options;
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
using Nethereum.Web3.Accounts;

namespace NethereumApp.Services
{
    public class EthereumService : IEthereumService
    {
        private Web3 web3;
        private string accountAdress;
        private string password;

        public string AccountAddress
        {
            get => this.accountAdress;

            set => this.accountAdress = value;
        }

        public EthereumService(IOptions<EthereumSettings> settings)
        {
            this.web3 = new Web3(settings.Value.GethUrl);
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
            var a = await this.web3.Personal.UnlockAccount.SendRequestAsync(this.accountAdress, this.password, 10 * seconds);
            return a;
        }

        public async Task<string> DeployContract(string abi, string byteCode, int gas)
        {
            return await this.web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, this.accountAdress, new HexBigInteger(gas), 2);
        }

        public async Task<TransactionReceipt> GetTransactionReceipt(string transactionHash)
        {
            return await this.web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        }

        public async Task<HexBigInteger> EstimateGas(string abi, string function, string senderAddress, int value)
        {
            return await this.GetContract(abi).GetFunction(function).EstimateGasAsync(this.AccountAddress, null, null, senderAddress, value);
        }

        public Contract GetContract(string abi)
        {
            return this.web3.Eth.GetContract(abi, this.accountAdress);
        }
    }
}
