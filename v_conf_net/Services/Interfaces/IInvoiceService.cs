using v_conf_net.DTOs;

namespace v_conf_net.Services.Interfaces;

public interface IInvoiceService
{
    Task<string> GenerateInvoiceAsync(InvoiceRequestDto request);
}
