using Nethereum.Web3;
using Nethereum.RPC;
using Nethereum.Hex;
using ManagmentTaskApp.Model;
using Nethereum.RPC.Eth.DTOs;

namespace ManagmentTaskApp.Nethereum
{

    public interface IBlockchainServicio
    {
        List<Balance> ObtenerBalances();
    }
    public class BlockchainServicio :  IBlockchainServicio

    {

        public List<Balance> ObtenerBalances()
        {
            
            List<Balance> list = new List<Balance>();
            var valorBalance =GetTaskAsync();
            Balance balance = new Balance(valorBalance.Result);
            list.Add(balance);
            return list;
        }

        async Task<string> GetTaskAsync()
        {

            var web3 = new Web3("http://localhost:7545");
            var contractAddress = "0xfF325E0691e83fdC3abDD94669dAf6750e9FC57c";
            var contractAbi = @"[ {""constant"":true,""inputs"":[],""name"":""getInfo"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";
            var contract = web3.Eth.GetContract(contractAbi, contractAddress);
            var crearTarea = contract.GetFunction("createTask");
            var tituloTarea = new String("Titulo");
            var descripcionTarea = new String("Descripcion");
            var result = await crearTarea.CallAsync<TransactionReceipt>();
            return "se creo";
        }
    }
}