namespace Dynamica.Shell.Models;

public class StatusMessages
{
    public sealed record StatusText(string Text);

    /// <summary>
    /// Value: Fortschritt (0..100). Optional Caption für Statuszeile.
    /// </summary>
    public sealed record StatusProgress(double Value, string? Caption = null);

    public sealed record StatusBusy(bool IsBusy);
}