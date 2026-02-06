namespace v_conf_net.DTOs;

public class AlternateComponentSaveDto
{
    public int ModelId { get; set; }
    public List<AlternateComponentDto> Components { get; set; } = new List<AlternateComponentDto>();
}
