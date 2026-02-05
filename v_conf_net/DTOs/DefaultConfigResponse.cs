namespace v_conf_net.DTOs;

public class DefaultConfigResponseDto
{
    public int ModelId { get; set; }
    public string ModelName { get; set; } = "";

    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }

    public List<DefaultComponentDto> Components { get; set; } = [];
}
