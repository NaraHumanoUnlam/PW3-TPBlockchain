using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartContractNethereum.Data.DTO;

public partial class SmartContractsContext : DbContext
{
    public SmartContractsContext()
    {
    }

    public SmartContractsContext(DbContextOptions<SmartContractsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NARIPC\\SQLEXPRESS;Database=SmartContracts;User Id=sa;Password=root;Trusted_Connection=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F8977609F");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PrivateKey).HasColumnName("privateKey");
            entity.Property(e => e.PublicKey).HasColumnName("publicKey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3213E83F1F12172C");

            entity.ToTable("Transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountToSend)
                .HasColumnType("numeric(30, 2)")
                .HasColumnName("amountToSend");
            entity.Property(e => e.Balance)
                .HasColumnType("numeric(30, 0)")
                .HasColumnName("balance");
            entity.Property(e => e.GasValue).HasColumnName("gasValue");
            entity.Property(e => e.SenderAddress)
                .HasMaxLength(500)
                .HasColumnName("senderAddress");
            entity.Property(e => e.TotalSupply)
                .HasColumnType("numeric(30, 2)")
                .HasColumnName("totalSupply");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
