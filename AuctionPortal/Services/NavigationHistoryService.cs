using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;

namespace AuctionPortal.Services
{
    public class NavigationHistoryService : IDisposable
    {
        private readonly NavigationManager _navigationManager;

        public event Action? OnHistoryChanged;

        private readonly List<BreadcrumbItem> _history = new();

        public NavigationHistoryService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.LocationChanged += HandleLocationChanged;
        }

        private void HandleLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            AddPage(e.Location);
        }

        private void AddPage(string uri)
        {
            var relativeUri = _navigationManager.ToBaseRelativePath(uri);
            if (string.IsNullOrEmpty(relativeUri))
                relativeUri = "/";

            string text = relativeUri == "/" ? "Home" : relativeUri.Split('/')[^1].Replace("-", " ").ToUpperInvariant();

            // Avoid duplicates in a row
            if (_history.Count == 0 || _history[^1].Text != text)
            {
                _history.Add(new BreadcrumbItem(text, href: "/" + relativeUri));
                OnHistoryChanged?.Invoke();
            }
        }

        public List<BreadcrumbItem> GetHistory() => new(_history);

        public void Dispose()
        {
            _navigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}
