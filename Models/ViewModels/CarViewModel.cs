namespace rentend.Models.ViewModels;

public class CarViewModel{
    public List<Brand> Brands { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public Car Car { get; set; } = new();
}