using System;
using System.Collections.Generic;

namespace SmartContractNethereum.Data.EF;

public partial class Transaction
{
    public int Id { get; set; }

    public string? SenderAddress { get; set; }

    public string? RecieverAddress { get; set; }

    public decimal? SenderBalance { get; set; }

    public decimal? ReceiverBalance { get; set; }

    public decimal? QuantityTokens { get; set; }
}
