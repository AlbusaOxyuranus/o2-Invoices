using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using O2.Services.Invoices.Business.Models;

namespace O2.Services.Invoices.Business.Services
{
    public interface IInvoiceService
    {
        Task<IReadOnlyCollection<Invoice>> GetAllAsync(CancellationToken ct);
        Task<Invoice> GetByIdAsync(long id, CancellationToken ct);
        Task<Invoice> UpdateAsync(Invoice invoice, CancellationToken ct);
        Task<Invoice> AddAsync(Invoice invoice, CancellationToken ct);
    }
}