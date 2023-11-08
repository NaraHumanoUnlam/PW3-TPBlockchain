using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmartContractNethereum.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractNethereum.Servicios
{
    public interface INethereumServicio
    {
        Task TransactionEther(string privateKey, string receiverAdress, decimal cantidadTokens);
    }

    public class NethereumServicio : INethereumServicio
    {
        private readonly SmartContractsContext _context; 
        private Transaction transaccion = new Transaction();
        public async Task TransactionEther(string _privateKey, string _receiverAdress, decimal cantidadTokens)
        {
            var privateKey = _privateKey;
            var chainId = 1337; 
            var account = new Account(privateKey, chainId);
            
            //me guardo el adress sender
            transaccion.SenderAddress = account.Address;
            transaccion.RecieverAddress = _receiverAdress;
            transaccion.QuantityTokens = cantidadTokens;    
            //se crea una instancia con NETHEREUM
            var web3 = new Web3(account, "http://localhost:7545");
            web3.TransactionManager.UseLegacyAsDefault = true;          
          
            try
            {

                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync(_receiverAdress, cantidadTokens);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var balance = await web3.Eth.GetBalance.SendRequestAsync(_receiverAdress);
            transaccion.ReceiverBalance = Web3.Convert.FromWei(balance.Value);

            var balance2 = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
           transaccion.SenderBalance = Web3.Convert.FromWei(balance2.Value);

           _context.Transactions.Add(transaccion);
            _context.SaveChanges();
        }
    }
}
