using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2.Services.Invoices.Business.Models;
using O2.Services.Invoices.Data.Entities;

namespace O2.Services.Invoices.Data.Configurations
{
    public class InvoiceEntityConfiguration: IEntityTypeConfiguration<InvoiceEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .UseSqlServerIdentityColumn();
        }
    }
}