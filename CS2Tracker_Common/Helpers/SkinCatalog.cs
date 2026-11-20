using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CS2Tracker_Common.Helpers
{
    public static class SkinCatalog
    {
        private static readonly Lazy<HashSet<string>> _baseNames = new(() =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "CS2_skins_names.json");
            var names = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(path))
                ?? new List<string>();
            return names.ToHashSet(StringComparer.OrdinalIgnoreCase);
        });

        public static IReadOnlySet<string> Names => _baseNames.Value;
    }
}
