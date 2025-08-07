namespace ExternalTicketSearch.API.Models;

public class TicketDto(string title)
{
    public string Title { get; set; } = title;
    public string? Description { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Medium";
    public string? AssignedToUserId { get; set; }
    public string Category { get; set; } = "General";
}
