using System;
using UniRx;

namespace Warlords.Utils
{
    public class IntStopwatch : IDisposable
    {
        private IDisposable _disposable;

        public Subject<TimeSpan> Tick { get; } = new Subject<TimeSpan>();

        public void Start()
        {
            int time = 0;
            
            Tick.OnNext(TimeSpan.FromSeconds(time));
            
            _disposable = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe((l =>
                {
                    time++;
                    Tick.OnNext(TimeSpan.FromSeconds(time));
                }));
        }

        public void Stop()
        {
            _disposable?.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}