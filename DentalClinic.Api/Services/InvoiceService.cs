using DentalClinic.Api.Data.DBContext;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Invoices;
using DentalClinic.Api.Entities;
using Microsoft.EntityFrameworkCore;
using static DentalClinic.Api.DTOs.Common.PaginationParams;

namespace DentalClinic.Api.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public Invoice CreateInvoice(int patientId, List<InvoiceItemCreateDto> items)
        {
            var invoice = new Invoice { PatientId = patientId };
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            foreach (var item in items)
            {
                // گرفتن DentalService برای Fee درست
                var service = _context.DentalServices.FirstOrDefault(s => s.Id == item.DentalServiceId);
                if (service == null) 
                    throw new Exception("خدمات دندانپزشکی مورد نظر یافت نشد!");


                var invoiceItem = new InvoiceItem
                {
                    InvoiceId = invoice.Id,
                    DentalServiceId = item.DentalServiceId,
                    Price = item.Price
                };
                _context.InvoiceItems.Add(invoiceItem);
            }

            _context.SaveChanges();
            return invoice;
        }

        public Invoice? GetInvoice(int invoiceId)
        {
            return _context.Invoices
                .Include(x => x.Items)
                .Include(x => x.Payments)
                .FirstOrDefault(x => x.Id == invoiceId);
        }

        public PagedResult<Invoice> GetInvoicesByDentist(
            int dentistId, 
            PaginationParams @params)
        {
            var query = _context.Invoices
                .Include(i => i.Items)
                .Include(i => i.Payments)
                .Where(i => i.Patient.DentistId == dentistId)
                .OrderByDescending(i => i.CreatedOn);

            var total = query.Count();

            var items = query
                .Skip((@params.PageNumber - 1) * @params.PageSize)
                .Take(@params.PageSize)
                .ToList();

            return new PagedResult<Invoice>
            {
                TotalCount = total,
                Items = items
            };
        }

        public InvoiceItem AddServiceToInvoice(
            int invoiceId,
            int dentalServiceId,
            decimal price)
        {
            var invoice = _context.Invoices
                .Include(x => x.Items)
                .FirstOrDefault(x => x.Id == invoiceId);

            if (invoice == null)
                throw new Exception("Invoice not found");

            var item = new InvoiceItem
            {
                InvoiceId = invoiceId,
                DentalServiceId = dentalServiceId,
                Price = price,
                Quantity = 1
            };

            _context.InvoiceItems.Add(item);
            _context.SaveChanges();

            return item;
        }

    }
}
