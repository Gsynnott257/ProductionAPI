using Microsoft.EntityFrameworkCore;
namespace ProductionAPI.Data.Models;

[Keyless]
public class ShipSkid
{
    public string? Part_Status { get; set; }
    public string? Skid_Status { get; set; }
    public string? Skid_Serial_Number { get; set; }
    public string? Shipping_Label { get; set; }
    public string? Full_Shipping_Label { get; set; }
}
