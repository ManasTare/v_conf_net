namespace v_conf_net.DTOs;

public class ModelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public int MinQty { get; set; }
    public string? ImagePath { get; set; }
}
