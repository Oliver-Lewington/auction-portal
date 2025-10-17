using MudBlazor;
using System;
using static MudBlazor.CategoryTypes;

namespace AuctionPortal.Components.Themes;

public static class MudblazorThemes
{
    public static MudTheme AtosTheme(bool isDarkMode = false) => new()
    {
        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Inter", "system-ui", "Segoe UI", "Roboto", "Helvetica Neue", "Arial", "sans-serif" },
                FontSize = "0.875rem", // Base font (text-sm)
                LineHeight = "1.5"
            },
            H1 = new H1Typography { FontSize = "2.25rem", FontWeight = "700" },  // text-4xl
            H2 = new H2Typography { FontSize = "1.875rem", FontWeight = "600" }, // text-3xl
            H3 = new H3Typography { FontSize = "1.5rem", FontWeight = "600" },   // text-2xl
            H4 = new H4Typography { FontSize = "1.25rem", FontWeight = "600" },  // text-xl
            H5 = new H5Typography { FontSize = "1.125rem", FontWeight = "500" }, // text-lg
            H6 = new H6Typography { FontSize = "1rem", FontWeight = "500" },     // text-base
            Body1 = new Body1Typography { FontSize = "1.1rem", FontWeight = "400" }, // text-sm
            Body2 = new Body2Typography { FontSize = "0.875rem", FontWeight = "400" },  // text-xs
            Button = new ButtonTypography { FontWeight = "500", TextTransform = "none" },
            Caption = new CaptionTypography { FontSize = "0.75rem", FontWeight = "400" }, // text-xs
            Subtitle1 = new Subtitle1Typography { FontSize = "0.875rem", FontWeight = "500" }, // text-sm semi-bold
            Subtitle2 = new Subtitle2Typography { FontSize = "0.75rem", FontWeight = "500" }  // text-xs semi-bold
        },

        PaletteLight = new PaletteLight
        {
            Primary = "#6366f1",          // Tailwind Indigo-500
            PrimaryContrastText = "#ffffff",
            Secondary = "#ec4899",        // Tailwind Pink-500
            SecondaryContrastText = "#ffffff",
            Background = "#f9fafb",       // bg-gray-50
            Surface = "#ffffff",
            AppbarBackground = "#ffffff",
            AppbarText = "#111827",       // text-gray-900
            DrawerBackground = "#ffffff",
            TextPrimary = "#111827",      // text-gray-900
            TextSecondary = "#6b7280",    // text-gray-600
            ActionDefault = "#6b7280",    // text-gray-500
            Divider = "#e5e7eb",          // border-gray-200
            LinesDefault = "#e5e7eb",
            TableLines = "#e5e7eb",
            Success = "#10b981",          // emerald-500
            Warning = "#f59e0b",          // amber-500
            Error = "#ef4444",            // red-500
            Info = "#3b82f6",             // blue-500
        },

        PaletteDark = new PaletteDark
        {
            Primary = "#6366f1",
            PrimaryContrastText = "#ffffff",
            Secondary = "#ec4899",
            Background = "#1f2937",       // gray-800
            Surface = "#111827",          // gray-900
            AppbarBackground = "#111827",
            AppbarText = "#e5e7eb",
            DrawerBackground = "#1f2937",
            TextPrimary = "#f3f4f6",      // gray-100
            TextSecondary = "#9ca3af",    // gray-400
            ActionDefault = "#9ca3af",
            Divider = "#374151",          // gray-700
            LinesDefault = "#374151",
            TableLines = "#374151",
            Success = "#10b981",
            Warning = "#f59e0b",
            Error = "#ef4444",
            Info = "#3b82f6",
        },

        LayoutProperties = new LayoutProperties
        {
            DrawerWidthLeft = "260px",
            DrawerWidthRight = "300px"
        }

    };

    public static MudTheme DefaultMudTheme(bool isDarkMode = false) => new()
    {
        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Inter", "system-ui", "Segoe UI", "Roboto", "Helvetica Neue", "Arial", "sans-serif" },
                FontSize = "0.875rem",
                LineHeight = "1.5"
            },
            Button = new ButtonTypography { FontWeight = "500", TextTransform = "none" }
        },

        PaletteLight = new PaletteLight
        {
            Primary = "#6366f1",
            Secondary = "#ec4899",
            Background = "#f9fafb",
            Surface = "#ffffff",
            AppbarBackground = "#ffffff",
            AppbarText = "#111827",
            DrawerBackground = "#ffffff",
            TextPrimary = "#111827",
            TextSecondary = "#4b5563",
            Divider = "#e5e7eb",
        },

        PaletteDark = new PaletteDark
        {
            Primary = "#6366f1",
            Secondary = "#ec4899",
            Background = "#111827",
            Surface = "#1f2937",
            AppbarBackground = "#1f2937",
            AppbarText = "#f3f4f6",
            DrawerBackground = "#1f2937",
            TextPrimary = "#f3f4f6",
            TextSecondary = "#9ca3af",
            Divider = "#374151",
        }
    };
}
