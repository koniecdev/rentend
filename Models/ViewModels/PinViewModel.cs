namespace rentend.Models.ViewModels;

public class PinViewModel{
    public List<Car> Cars { get; set; } = new();
    public Pin pin { get; set; } = new();
}