using SmartContractNethereum.Data.EF;

namespace SmartContractNethereum.Servicios
{
    public interface ITransaccionServicio
    {
        List<Transaction> ListarTransacciones();
    }

    public class TransaccionServicio : ITransaccionServicio
    {
        private readonly SmartContractsContext _context;

        public TransaccionServicio(SmartContractsContext context)
        {
            _context = context;
        }

        public List<Transaction> ListarTransacciones()
        {
           return _context.Transactions.Where(x => x.Id != null).ToList();
        }
    }
}