using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;

namespace blazor_layout;

public class ViewState : INotifyPropertyChanged
{
    public int Value { get; set; } = 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Increment()
    {
        Console.WriteLine($"Increment {Value}");
        Value++;
        OnPropertyChanged();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default)
       => PropertyChanged?.Invoke(this, new(propertyName));
}



public static class CascadingValueNotifying
{
    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false) where T : INotifyPropertyChanged
    {
        var source = new CascadingValueSource<T>(value, isFixed);

        value.PropertyChanged += (sender, args) => source.NotifyChangedAsync();

        return source;
    }
};