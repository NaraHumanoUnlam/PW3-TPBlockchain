using Nethereum.Model;
using Nethereum.Web3;
using SmartContractNethereum.Data.DTO;
using Nethereum.Accounts;
using System.Threading.Tasks;
using System.Numerics;

namespace SmartContractNethereum.Servicios 
{
   public interface ISmartContractServicio
    {
        List<Transaction> ListarTransacciones();
        List<Data.DTO.Account> ListarCuentas();
        Data.DTO.Account ObtenerCuenta(int id);

        void CrearTransaccion(Transaction transaccion, Data.DTO.Account account);
    }

    public class SmartContractServicio : ISmartContractServicio
    {
        private readonly SmartContractsContext _context;

        public SmartContractServicio(SmartContractsContext context)
        {
            _context = context;
        }
        public void CrearTransaccion(Transaction transaccion, Data.DTO.Account account)
        {
           
           Data.DTO.Transaction nueva =  AdministrarSmartContract(account,transaccion,_context).Result;

            _context.Transactions.Add(nueva);
            _context.SaveChanges();
        }

        public List<Transaction> ListarTransacciones()
        {
            List<Transaction> list = _context.Transactions.Where(x => x.Id != null).ToList();

            return list;
        }

        public Data.DTO.Account ObtenerCuenta(int id)
        {
           return _context.Accounts.Where(x=> x.Id == id).ToArray()[0];
        }

        public static async Task<Data.DTO.Transaction> AdministrarSmartContract(Data.DTO.Account account, Data.DTO.Transaction transaction, SmartContractsContext context)
        {
            
            if (account != null)
            {
                var url = "http://testchain.nethereum.com:8545";
                var privateKey = account.PrivateKey;
                var chainId = 444444444500; //Nethereum test chain, chainId
                var cuenta = new Nethereum.Web3.Accounts.Account(privateKey, chainId);
                var web3 = new Web3(cuenta, url);
                // web3.TransactionManager.UseLegacyAsDefault = true;

                //This is the contract bytecode (compile executable) and Abi
                var contractByteCode =
                    "0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056";
                var abi =
                    @"[{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":""supply"",""type"":""uint256""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":""success"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""balance"",""type"":""uint256""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""},{""name"":""_spender"",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":""remaining"",""type"":""uint256""}],""type"":""function""},{""inputs"":[{""name"":""_initialAmount"",""type"":""uint256""}],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_from"",""type"":""address""},{""indexed"":true,""name"":""_to"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_owner"",""type"":""address""},{""indexed"":true,""name"":""_spender"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]";


                // **** DEPLOYING THE SMART CONTRACT
                // The solidity smart contract constructor for this standard ERC20 smart contract is as follows:
                //function Standard_Token(uint256 _initialAmount) 
                //{         balances[msg.sender] = _initialAmount;         
                //         _totalSupply = _initialAmount;     
                //}
                //This means we need to supply a parameter to a constructor on deployment

                var totalSupply = transaction.TotalSupply;
                var senderAddress = cuenta.Address;
                //When working with untyped smart contract defintions the parameters are passed as part of a params object array, and recognised and map using the abi.
                var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi,
                    contractByteCode, senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger(900000), null, totalSupply);
                //Using our contract address we can interact with the contract as follows:
                var contract = web3.Eth.GetContract(abi, receipt.ContractAddress);

                //Using the contract we can retrieve the functions using their name.
                var transferFunction = contract.GetFunction("transfer");
                var balanceFunction = contract.GetFunction("balanceOf");

                var newAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe";

                //Using a CallAsyc we can query the smart contract for values
                var balance = await balanceFunction.CallAsync<int>(newAddress);
                var amountToSend = transaction.AmountToSend;
                //Sending transactions will commit the information to the chain, before submission we need to estimate the gas (cost of the transaction)
                //In a similar way any parameters required by the function are included at the end of the method in the same order as per the solidity function.
                var gas = await transferFunction.EstimateGasAsync(senderAddress, null, null, newAddress, amountToSend);
                var receiptAmountSend =
                    await transferFunction.SendTransactionAndWaitForReceiptAsync(senderAddress, gas, null, null, newAddress,
                        amountToSend);
                
                balance = await balanceFunction.CallAsync<int>(newAddress);

                Transaction nuevaTransacction = context.Transactions.FirstOrDefault(t => t.Id==transaction.Id) ?? transaction;
                nuevaTransacction.Balance = balance;
                nuevaTransacction.SenderAddress = senderAddress;
                nuevaTransacction.GasValue = gas.ToString();
                return nuevaTransacction;
            }

            return new Transaction();
        }

        public List<Data.DTO.Account> ListarCuentas()
        {
            return _context.Accounts.Where(x => x.Id != null).ToList();
        }
    }
   
}
