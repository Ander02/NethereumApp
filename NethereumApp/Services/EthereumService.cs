using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumApp.Domain;
using NethereumApp.Infraestructure;
using NethereumApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            this.web3 = new Web3(Constants.GETH_URL);
            this.AccountAddress = settings.Value.EthereumAccount;
            this.password = settings.Value.EthereumPassword;
        }

        public async Task<decimal> GetBalance(string address)
        {
            var balance = await this.web3.Eth.GetBalance.SendRequestAsync(address);
            return Web3.Convert.FromWei(balance.Value, 18);
        }

        public Task<bool> ReleaseContract(string name, string abi, string byteCode, int gas)
        {
            throw new NotImplementedException();
        }

        public Task<string> TryGetContractAddress(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Contract> GetContract(string name)
        {
            throw new NotImplementedException();
        }
    }
}
