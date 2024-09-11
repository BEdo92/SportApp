namespace SportAndStepsApps.Models;

public class Sports
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RunKm { get; set; }
    public int SwimKm { get; set; }
    public int BikeKm { get; set; }
    public int HikeKm { get; set; }
    public int NumOfSteps { get; set; }
}
