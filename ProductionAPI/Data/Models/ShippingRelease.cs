using Microsoft.EntityFrameworkCore;

namespace ProductionAPI.Data.Models;

[Keyless]
public class ShippingRelease
{
    public string? Part_Status { get; set; }
    public string? Skid_Status { get; set; }
    public string? Internal_PUN { get; set; }
    public string? Customer_PUN { get; set; }
    public string? Skid_Serial_Number { get; set; }
    public string? Part_Number { get; set; }
    public string? Customer { get; set; }
    public string? Full_Shipping_Label { get; set; }
    public string? Suspect_Status { get; set; }
    public string? Suspect_Reason { get; set; }
    public string? Skid_QTY { get; set; }

}
