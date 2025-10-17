using MudBlazor;

namespace AuctionPortal.Components.Steppers
{
    public class StepperValidator<T>
    {
        private readonly T _model;
        private readonly Dictionary<int, List<(Func<T, bool> rule, string message)>> _rules = new();
        private readonly Dictionary<int, List<string>> _errors = new();

        public StepperValidator(T model)
        {
            _model = model;
        }

        public void AddRule(int stepIndex, Func<T, bool> rule, string message)
        {
            if (!_rules.ContainsKey(stepIndex))
                _rules[stepIndex] = new();

            _rules[stepIndex].Add((rule, message));
        }

        public bool HasError(int stepIndex) =>
            _errors.ContainsKey(stepIndex) && _errors[stepIndex].Any();

        public IEnumerable<string> GetErrorMessages(int stepIndex) =>
            _errors.ContainsKey(stepIndex) ? _errors[stepIndex] : Enumerable.Empty<string>();

        public async Task OnPreviewInteraction(StepperInteractionEventArgs args)
        {
            if (args.Action == StepAction.Complete)
            {
                if (!ValidateStep(args.StepIndex))
                    args.Cancel = true;
            }

            await Task.Yield();
        }

        private bool ValidateStep(int stepIndex)
        {
            _errors[stepIndex] = new List<string>();

            if (_rules.TryGetValue(stepIndex, out var rules))
            {
                foreach (var (rule, message) in rules)
                {
                    if (!rule(_model))
                        _errors[stepIndex].Add(message);
                }
            }

            return !_errors[stepIndex].Any();
        }
    }
}
