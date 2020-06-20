using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using O2.Services.Invoices.Business.Models;
using O2.Services.Invoices.Business.Services;

namespace O2.Services.Invoices.Business.Impl.Services
{
    public class InMemoryInvoiceService : IInvoiceService
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly List<Invoice> _invoices = new List<Invoice>();
        private long _currentInvoice = 0;

        public Task<IReadOnlyCollection<Invoice>> GetAllAsync(CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyCollection<Invoice>>(_invoices.AsReadOnly());
        }

        public async Task<Invoice> GetByIdAsync(long id, CancellationToken ct)
        {
            await Task.Delay(1000, ct);
            var extResult1Task = CallExternalServiceAsync(ct);
            var extResult2Task = CallExternalServiceAsync(ct);
            await Task.WhenAll(extResult1Task, extResult2Task);
            return _invoices.SingleOrDefault(x => x.Id == id);
        }

        public Task<Invoice> UpdateAsync(Invoice invoice, CancellationToken ct)
        {
            var toUpdate = _invoices.SingleOrDefault(x => x.Id == invoice.Id);
            if (toUpdate == null)
            {
                return null;
            }

            // toUpdate.Name = invoice.Name;
            return Task.FromResult(toUpdate);
        }

        public Task<Invoice> AddAsync(Invoice invoice, CancellationToken ct)
        {
            invoice.Id = ++_currentInvoice;
            _invoices.Add(invoice);
            return Task.FromResult(invoice);
        }

        private static async Task<int> CallExternalServiceAsync(CancellationToken ct)
        {
            await Task.Delay(1000, ct);
            return RandomGenerator.Next();
        }
    }
}