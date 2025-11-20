using System.Collections.ObjectModel;

using Domain;
using Data.InMemory;
using Data.Interfaces;

namespace Models;

public class EntryListViewModel : BaseViewModel
{
    private readonly PasswordsRepository _repository;
    public ObservableCollection<EntryItemViewModel> Entries { get; set; }
    public FiltersViewModel FilterVm { get; }
    
    public EntryListViewModel()
    {
        _repository = new PasswordsRepository();
        Entries = [];
        
        FilterVm = new FiltersViewModel(RefreshList);

        RefreshList(new EntryFilter());
        
    }
    
    private void RefreshList(EntryFilter filter)
    {   
        Entries.Clear();
        foreach (var req in _repository.GetAll(filter))
        {
            Entries.Add(new EntryItemViewModel(req));
        }
        OnPropertyChanged(nameof(Entries));
    }
}