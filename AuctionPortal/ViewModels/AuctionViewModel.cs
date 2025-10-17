namespace AuctionPortal.ViewModels
{
    public class AuctionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool LiveFlag { get; set; }

        // Core computed values
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }

        // Optional display tracking
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductViewModel> Products { get; set; } = new();

        private DateTime _startDate = DateTime.Today.AddDays(1);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value.Date;
                UpdateStartDateTime();
            }
        }

        private TimeSpan _startTime = DateTime.UtcNow.TimeOfDay;
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                UpdateStartDateTime();
            }
        }

        private DateTime _endDate = DateTime.Today.AddDays(2);
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value.Date;
                UpdateEndDateTime();
            }
        }

        private TimeSpan _endTime = DateTime.UtcNow.AddHours(1).TimeOfDay;
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                UpdateEndDateTime();
            }
        }

        public AuctionViewModel()
        {
            UpdateStartDateTime();
            UpdateEndDateTime();
        }

        private void UpdateStartDateTime()
        {
            StartDateTime = StartDate.Date.Add(StartTime);
        }

        private void UpdateEndDateTime()
        {
            EndDateTime = EndDate.Date.Add(EndTime);
        }
    }
}
