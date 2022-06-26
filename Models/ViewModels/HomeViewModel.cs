namespace rentend.Models.ViewModels;

public class HomeViewModel{
    public List<Tuple<Car, string>> Cars {get;set;} = new();
    public List<Department> Departments {get;set;} = new();
    public IndexViewModel IndexVM { get; set; } = new();
}