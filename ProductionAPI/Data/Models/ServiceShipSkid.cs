using Microsoft.EntityFrameworkCore;
namespace ProductionAPI.Data.Models;

[Keyless]
public class ServiceShipSkid
{
    public string Full_Shipping_Label { get; set; }
    public string Skid_Serial_Number_1 { get; set; }
    public string? Skid_Serial_Number_2 { get; set; }
    public string? Skid_Serial_Number_3 { get; set; }
    public string? Skid_Serial_Number_4 { get; set; } 
    public string? Skid_Serial_Number_5 { get; set; } 
    public string? Skid_Serial_Number_6 { get; set; }
    public string? Skid_Serial_Number_7 { get; set; } 
    public string? Skid_Serial_Number_8 { get; set; }
}
