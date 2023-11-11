using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;
using SmartContractNethereum.Data.EF;
using SmartContractNethereum.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private readonly ITransaccionServicio _trasaccionServicio;

        public NethereumServicio( ITransaccionServicio transaccionServicio )
        {
            _trasaccionServicio = transaccionServicio;
        }
       
        public async Task TransactionEther(string _privateKey, string _receiverAdress, decimal cantidadTokens)
        {

            Transaction transaccion = new Transaction();
            var privateKey = _privateKey;
            var chainId = 1337; 
            var account = new Account(privateKey, chainId);
     
            //se crea una instancia con NETHEREUM
            var web3 = new Web3(account, "http://localhost:7545");
            web3.TransactionManager.UseLegacyAsDefault = false;

            try
            {
                var fromAddress =account.Address;
                var toAddress = _receiverAdress;

                // El límite de gas y el precio del gas
                var gasLimit = new HexBigInteger(21000); // Este es un valor típico para una transferencia estándar
                var gasPrice = new HexBigInteger(WeiToGWei(20)); // Precio en Gwei

                // Monto a enviar en Ether
                var amountToSend = cantidadTokens;

                // Conversión de Ether a Wei (1 Ether = 10^18 Wei)
                var amountWei = Web3.Convert.ToWei(amountToSend);

                // Construir y enviar la transacción
                var transactionInput = new Nethereum.RPC.Eth.DTOs.TransactionInput
                {
                    From = fromAddress,
                    To = toAddress,
                    Value = new HexBigInteger(amountWei),
                    Gas = gasLimit,
                    GasPrice = gasPrice
                };

                var transactionHash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(transactionInput);
                var balance = await web3.Eth.GetBalance.SendRequestAsync(_receiverAdress);
                var balance2 = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
                transaccion.ReceiverBalance = Web3.Convert.FromWei(balance.Value);
                transaccion.SenderBalance = Web3.Convert.FromWei(balance2.Value);
                transaccion.SenderAddress = account.Address;
                transaccion.RecieverAddress = _receiverAdress;
                transaccion.QuantityTokens = cantidadTokens;

                _trasaccionServicio.Crear(transaccion);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Listar","Ops! Ocurrio un error en tu transacción verifica tus fondos.");
            }

            
           
        }

        public static BigInteger WeiToGWei(int gwei)
        {
            return gwei * (BigInteger)Math.Pow(10, 9);
        }
    }
}
