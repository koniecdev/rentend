namespace rentend.Models.ViewModels;

public class HomeViewModel{
    public List<Tuple<Car, string>> Cars {get;set;} = new();
}