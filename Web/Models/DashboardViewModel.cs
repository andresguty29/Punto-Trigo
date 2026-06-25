namespace Web.Models;

public class DashboardViewModel
{
    public DateTime Today { get; init; }
    public string UserInitials { get; init; } = "PT";
    public string CurrentRole { get; init; } = "";
    public string IdTrabajador { get; init; } = "";
    public IReadOnlyList<DashboardModuleViewModel> Modules { get; init; } = [];
}

public class DashboardModuleViewModel
{
    public string Key { get; init; } = string.Empty;
    public IReadOnlyList<string> Roles { get; init; } = [];
    public string Name { get; init; } = string.Empty;
    public string Tag { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Accent { get; init; } = "#0B3D6E";
    public IReadOnlyList<string> Points { get; init; } = [];
    public IReadOnlyList<DashboardMetricViewModel> Metrics { get; init; } = [];
    public IReadOnlyList<DashboardSubviewViewModel> SubViews { get; init; } = [];
    public DashboardTableViewModel Table { get; init; } = new();
    public IReadOnlyList<string> Activity { get; init; } = [];
}

public class DashboardMetricViewModel
{
    public string Label { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public string Trend { get; init; } = string.Empty;
}

public class DashboardSubviewViewModel
{
    public string Key { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
}

public class DashboardTableViewModel
{
    public string Title { get; init; } = string.Empty;
    public IReadOnlyList<string> Columns { get; init; } = [];
    public IReadOnlyList<IReadOnlyList<string>> Rows { get; init; } = [];
    public string? EmptyMessage { get; init; }
    public string? SourceUrl { get; init; }
}