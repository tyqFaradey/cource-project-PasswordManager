using System.Windows;
using System.Windows.Input;

using Domain;

namespace Models;

public class EntryItemViewModel : BaseViewModel
{
    public Entry Entry { get; }

    public EntryItemViewModel(Entry entry) => this.Entry = entry;

    public string Name => Entry.Name;
    public string Username => Entry.Username;
    public string Password => Entry.Password;
    public string Email => Entry.Email;
    public string Url => Entry.URL;
    public int Category => Entry.Category;
    public DateTime AddingDate => Entry.AddingDate;

    public ICommand CopyUsernameCommand => new RelayCommand(_ => Clipboard.SetText(Entry.Username));
    public ICommand CopyPasswordCommand => new RelayCommand(_ => Clipboard.SetText(Entry.Password));
    public ICommand OpenUrlCommand => new RelayCommand(_ =>
    {
        if (!string.IsNullOrWhiteSpace(Entry.URL))
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = Entry.URL,
                UseShellExecute = true
            });
    });
}