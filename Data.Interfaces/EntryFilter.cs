using Domain;

namespace Data.Interfaces;

public record EntryFilter
{ 
    public string? Email { get; init; }
    public int? Category { get;  init; }
    public string? Url { get; init; }   
    public DateTime? AddingTime { get; init; }
    public string? Search { get; set; }
}