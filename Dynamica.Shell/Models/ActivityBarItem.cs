using ReactiveUI;

namespace Dynamica.Shell.Models;

public sealed class ActivityBarItem : ReactiveObject
{
    public string Id { get; }
    public string Icon { get; }
    public string Label { get; }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }

    public ActivityBarItem(string id, string icon, string label)
    {
        Id = id; Icon = icon; Label = label;
    }
}