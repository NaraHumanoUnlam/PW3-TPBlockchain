using System;
using System.Collections.Generic;

namespace SmartContractNethereum.Data.DTO;

public partial class Transaction
{
    public int Id { get; set; }

    public decimal? TotalSupply { get; set; }

    public string? SenderAddress { get; set; }

    public decimal? AmountToSend { get; set; }

    public string? GasValue { get; set; }

    public decimal? Balance { get; set; }
}
