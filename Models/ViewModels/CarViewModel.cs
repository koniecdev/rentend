namespace rentend.Models.ViewModels;

public class CarViewModel{
    public CarViewModel()
    {
        Brands = new();
        Car = new();
    }
    public List<Brand> Brands { get; set; }
    public Car Car { get; set; }        
}