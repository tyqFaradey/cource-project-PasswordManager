using System;
using System.Windows.Input;

namespace Models;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _can_execute;

    public RelayCommand(Action<object> execute, Func<object, bool> can_execute = null)
    {
        _execute = execute;
        _can_execute = can_execute;
    }

    public bool CanExecute(object parameter) =>
        _can_execute == null || _can_execute(parameter);

    public void Execute(object parameter) => _execute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}