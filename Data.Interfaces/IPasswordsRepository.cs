using Domain;


namespace Data.Interfaces;

public interface IPasswordsRepository
{
    int Add(Entry request);
    int Update(Entry request);
    bool Delete(int id);
    
    Entry? Get(int id);
    List<Entry> GetAll(EntryFilter filter);
}