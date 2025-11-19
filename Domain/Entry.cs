namespace Domain;

public class Entry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Category {  get; set; }
    public string URL { get; set; }
    public DateTime AddingDate { get; set; }
}