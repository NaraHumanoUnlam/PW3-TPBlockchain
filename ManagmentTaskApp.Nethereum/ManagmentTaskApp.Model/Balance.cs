namespace ManagmentTaskApp.Model
{
    public partial class Balance
    {
        public Balance(string? valorBalance)
        {
            this.Valor = valorBalance;
        }

        public string Valor { get; set; }

    }
}