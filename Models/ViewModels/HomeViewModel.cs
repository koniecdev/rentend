namespace rentend.Models.ViewModels;

public class HomeViewModel{
    public List<Tuple<Car, string>> Cars {get;set;} = new();
    public List<Departament> Departaments {get;set;} = new();
    public IndexViewModel IndexVM { get; set; } = new();
}