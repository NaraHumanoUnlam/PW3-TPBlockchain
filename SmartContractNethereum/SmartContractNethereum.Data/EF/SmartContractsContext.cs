using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartContractNethereum.Data.EF;

public partial class SmartContractsContext : DbContext
{
    public SmartContractsContext()
    {
    }

    public SmartContractsContext(DbContextOptions<SmartContractsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NARIPC\\SQLEXPRESS;Database=SmartContracts;User Id=sa;Password=root;Trusted_Connection=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3213E83FAE40A103");

            entity.ToTable("Transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuantityTokens)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("quantityTokens");
            entity.Property(e => e.ReceiverBalance)
                .HasColumnType("numeric(30, 2)")
                .HasColumnName("receiverBalance");
            entity.Property(e => e.RecieverAddress).HasColumnName("recieverAddress");
            entity.Property(e => e.SenderAddress).HasColumnName("senderAddress");
            entity.Property(e => e.SenderBalance)
                .HasColumnType("numeric(30, 2)")
                .HasColumnName("senderBalance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
