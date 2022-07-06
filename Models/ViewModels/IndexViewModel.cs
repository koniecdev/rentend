namespace rentend.Models.ViewModels;

public class IndexViewModel{
    public int DepartmentId { get; set; } = 1;
    public DateTime RentSince { get; set; } = DateTime.Now.AddDays(1);
    public DateTime RentTo { get; set; } = DateTime.Now.AddDays(7);
    public List<Department> Departments {get;set;} = new();
}