namespace v_conf_net.DTOs;

public class ComponentDropdownDto
{
    public string ComponentName { get; set; }
    public List<OptionDto> Options { get; set; }

    public ComponentDropdownDto(string componentName, List<OptionDto> options)
    {
        ComponentName = componentName;
        Options = options;
    }
}
