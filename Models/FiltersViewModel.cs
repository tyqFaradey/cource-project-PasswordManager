using System.Collections.ObjectModel;

using Domain;
using Data.InMemory;
using Data.Interfaces;


namespace Models;

public class FiltersViewModel : BaseViewModel
{
    private EntryFilter _filter = new();

    private readonly Action<EntryFilter> _onChanged;

    public FiltersViewModel(Action<EntryFilter> onChanged)
    {
        _onChanged = onChanged;
    }

    public string? Email
    {
        get => _filter.Email;
        set
        {
            _filter = _filter with { Email = value };
            OnPropertyChanged();
            _onChanged(_filter);
        }
    }

    public string? Url
    {
        get => _filter.Url;
        set
        {
            _filter = _filter with { Url = value };
            OnPropertyChanged();
            _onChanged(_filter);
        }
    }

    public int? Category
    {
        get => _filter.Category;
        set
        {
            _filter = _filter with { Category = value };
            OnPropertyChanged();
            _onChanged(_filter);
        }
    }

    public DateTime? AddingTime
    {
        get => _filter.AddingTime;
        set
        {
            _filter = _filter with { AddingTime = value };
            OnPropertyChanged();
            _onChanged(_filter);
        }
    }

    public string? Search
    {
        get => _filter.Search;
        set
        {
            _filter.Search = value;
            OnPropertyChanged();
            _onChanged(_filter);
        }
    }
}