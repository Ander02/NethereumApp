using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NethereumApp.Domain
{
    public class EthereumContractInfo
    {
        public Guid Id { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
        public string TransactionHash { get; set; }
        public string ContractAddress { get; set; }
    }
}
