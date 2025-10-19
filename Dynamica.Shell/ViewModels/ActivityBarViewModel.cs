using System.Collections.ObjectModel;
using System.Reactive;
using Dynamica.Shell.Models;
using ReactiveUI;

namespace Dynamica.Shell.ViewModels;

public sealed class ActivityBarViewModel : ReactiveObject
{
    public ObservableCollection<ActivityItem> Items { get; } = new();

    private string? _selectedId;
    public string? SelectedId
    {
        get => _selectedId;
        private set => this.RaiseAndSetIfChanged(ref _selectedId, value);
    }

    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
    }

    public ReactiveCommand<string, Unit> ItemClick { get; }

    public ActivityBarViewModel()
    {
        // Beispielitems
        Items.Add(new ActivityItem("tags",      "🏷", "Tags"));
        Items.Add(new ActivityItem("search",    "🔍", "Suche"));
        Items.Add(new ActivityItem("settings",  "⚙",  "Einstellungen"));

        // Standard: Icons-only
        IsExpanded = false;

        ItemClick = ReactiveCommand.Create<string>(OnItemClick);
    }

    private void OnItemClick(string id)
    {
        if (SelectedId == id)
        {
            // Gleiches Icon erneut geklickt -> Toggle expand/collapse
            IsExpanded = !IsExpanded;
        }
        else
        {
            // Auswahl wechseln, Breite unverändert lassen
            SelectedId = id;
            foreach (var it in Items)
                it.IsSelected = it.Id == id;
        }
    }
}