namespace v_conf_net.DTOs;

public class OptionDto
{
    public int CompId { get; set; }
    public string SubType { get; set; }
    public double Price { get; set; }

    public OptionDto(int compId, string subType, double price)
    {
        CompId = compId;
        SubType = subType;
        Price = price;
    }
}
