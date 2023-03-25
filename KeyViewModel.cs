using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KeyboardDoubleClickFinder.Annotations;

namespace KeyboardDoubleClickFinder;

public class KeyViewModel : INotifyPropertyChanged, IComparable<KeyViewModel>
{
    public Key Key { get; set; }

    public long DoublePrevDelta { get; private set; }
    public long PrevDelta { get; private set; }
    public long Delta { get; private set; }

    public long MinDelta { get; private set; } = 1000;
    public bool IsLittleDelta { get; set; } = false;

    private Stopwatch _sw;

    public KeyViewModel()
    {
        _sw = Stopwatch.StartNew();
    }

    public void Pressed()
    {
        DoublePrevDelta = PrevDelta;
        PrevDelta = Delta;
        Delta = _sw.ElapsedMilliseconds;
        if (Delta < 100)
            IsLittleDelta = true;
        if (MinDelta > Delta) MinDelta = Delta;
        _sw.Restart();
    }

    public void Reset()
    {
        DoublePrevDelta = PrevDelta = Delta = 0;
        IsLittleDelta = false;
        MinDelta = 1000;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public int CompareTo(KeyViewModel? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Key.CompareTo(other.Key);
    }
}