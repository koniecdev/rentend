namespace rentend.Models.ViewModels;

public class SingleCarViewModel{
    public SingleCarViewModel()
    {
        Car = new();
        Image = "";
        Images = new();
        IndexVM = new();
    }
    public IndexViewModel IndexVM { get; set; }
    public Car Car { get; set; }
    public string Image { get; set; }
    public List<string> Images {get;set;}
    public DateTime Since {get;set;} = DateTime.Now.AddDays(1);
    public DateTime Until {get;set;} = DateTime.Now.AddDays(7);
}