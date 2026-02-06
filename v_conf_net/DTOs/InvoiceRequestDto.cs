namespace v_conf_net.DTOs;

public class InvoiceRequestDto
{
    public int UserId { get; set; }
    public int ModelId { get; set; }
    public int Qty { get; set; }
    public string? CustomerDetail { get; set; }
}
