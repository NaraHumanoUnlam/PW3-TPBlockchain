using SmartContractNethereum.Data.EF;

namespace SmartContractNethereum.Servicios
{
    public interface ITransaccionServicio
    {
        List<Transaction> ListarTransacciones();

        void Crear(Transaction transaction);
    }

    public class TransaccionServicio : ITransaccionServicio
    {
        private readonly SmartContractsContext _context;

        public TransaccionServicio(SmartContractsContext context)
        {
            _context = context;
        }

        public void Crear(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<Transaction> ListarTransacciones()
        {
           return _context.Transactions.Where(x => x.Id != null).ToList();
        }
    }
}