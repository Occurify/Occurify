
namespace Occurify.TimelineUtils
{
    internal class SynchronizedTimeline : Timeline
    {
        private readonly object _gate;
        private readonly ITimeline _source;

        public SynchronizedTimeline(ITimeline source, object? gate = null)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            _gate = gate ?? new();
            _source = source;
        }

        public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
        {
            lock (_gate)
            {
                return _source.GetPreviousUtcInstant(utcRelativeTo);
            }
        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            lock (_gate)
            {
                return _source.GetNextUtcInstant(utcRelativeTo);
            }
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            lock (_gate)
            {
                return _source.IsInstant(utcDateTime);
            }
        }
    }
}
