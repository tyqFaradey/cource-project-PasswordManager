using Domain;
using Data.Interfaces;

namespace Data.InMemory;

public class PasswordsRepository : IPasswordsRepository
{
    private readonly List<Entry> _entriesList = [];
    private int _nextId = 1;


    public PasswordsRepository()
    {
        Add(new Entry {
            Id = 1,
            Name = "Google",
            Username = "mylogin",
            Password = "123456",
            Email = "email@gmail.com",
            URL = "https://google.com",
            Category = 1,
            AddingDate = DateTime.Now
        });
        
        Add(new Entry {
            Id = 2,
            Name = "GitHub",
            Username = "dallix",
            Password = "qwerty",
            Email = "git@gmail.com",
            URL = "https://github.com",
            Category = 2,
            AddingDate = DateTime.Now
        });
    }
    
    public int Add(Entry? entry)
    {
        if (entry == null) return -1;

        entry.Id = _nextId++;
        _entriesList.Add(entry);
        
        return entry.Id;
    }
    
    public int Update(Entry? entry)
    {
        if (entry == null) return -1;
        var existing = _entriesList.FirstOrDefault(r => r.Id == entry.Id);
        if (existing == null) return -1;
        
        existing.Name = entry.Name;
        existing.Username = entry.Username;
        existing.Password = entry.Password;
        existing.Email = entry.Email;
        existing.Category = entry.Category;
        existing.URL = entry.URL;
        existing.AddingDate = entry.AddingDate;

        return existing.Id;
    }
    
    public Entry? Get(int id)
    {
        return _entriesList.FirstOrDefault(r => r.Id == id);
    }
    
    public bool Delete(int id)
    {
        var existing = Get(id);
        if (existing == null) return false;
        
        return _entriesList.Remove(existing);
    }
    
    public List<Entry> GetAll(EntryFilter filter)
    {
        var result = _entriesList.AsEnumerable();

        // ---------------- ФИЛЬТРАЦИЯ ----------------

        if (!string.IsNullOrWhiteSpace(filter.Email))
            result = result.Where(r => r.Email == filter.Email);

        if (!string.IsNullOrWhiteSpace(filter.Url))
            result = result.Where(r => r.URL == filter.Url);

        if (filter.Category.HasValue)
            result = result.Where(r => r.Category == filter.Category);

        if (filter.AddingTime.HasValue)
            result = result.Where(r => r.AddingDate == filter.AddingTime.Value);

        // ---------------- ПОИСК ----------------

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            string search = filter.Search.ToLower();

            result = result.Where(r =>
                    (r.Name?.ToLower().Contains(search) ?? false) ||
                    (r.Username?.ToLower().Contains(search) ?? false) ||
                    (r.Email?.ToLower().Contains(search) ?? false)
            );
        }

        return result.ToList();
    }

}