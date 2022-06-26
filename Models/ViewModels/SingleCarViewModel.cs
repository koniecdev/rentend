namespace rentend.Models.ViewModels;

public class SingleCarViewModel{
    public SingleCarViewModel()
    {
        Car = new();
        Image = "";
        Images = new();
    }
    public Car Car { get; set; }
    public string Image { get; set; }
    public List<string> Images {get;set;}
    public DateTime Since {get;set;}
    public DateTime Until {get;set;}
}