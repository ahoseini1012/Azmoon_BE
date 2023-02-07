namespace Agricaltech.BL;
public class Attendee
{
    public long ID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Family { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public int StateId { get; set; }
    public string MobileNo { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int TypeId { get; set; }
}