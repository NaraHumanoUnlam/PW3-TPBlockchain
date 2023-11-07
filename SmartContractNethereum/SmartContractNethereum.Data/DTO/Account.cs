using System;
using System.Collections.Generic;

namespace SmartContractNethereum.Data.DTO;

public partial class Account
{
    public int Id { get; set; }

    public string PrivateKey { get; set; } = null!;

    public string PublicKey { get; set; } = null!;
}
