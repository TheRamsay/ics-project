namespace KlidecekIS.BL.Models;

public record GradeListModel: ModelBase
{
    public double Score { get; set; }
    public required string Note { get; set; }
    
    public static GradeListModel Empty => new GradeListModel()
    {
        Id = Guid.Empty,
        Note = string.Empty
    };
}