namespace SportAndStepsApps.Helpers;

public class SportParams
{
    private const int MaxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    private int pageSize = 10;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string? SportType { get; set; }
    public int? DistanceFrom { get; set; }
    public int? DistanceTo { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public string OrderBy { get; set; } = "date";
}
