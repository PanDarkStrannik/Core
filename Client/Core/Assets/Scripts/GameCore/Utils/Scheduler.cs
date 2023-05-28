using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameCore.Utils
{
    public class Scheduler
    {
        public event Action<int> OnTick;
        public event Action OnTimerStart;
        public event Action<int> OnStart;
        public event Action<int> OnResume;
        public event Action<int> OnStop;
        public event Action<int> OnPause; 
        public event Action OnFinished; 
        public int RemainingTime { get; private set; }
        public int TickStepMs { get; private set; }

        private CancellationTokenSource _tokenSource;

        private const int MsInSec = 1000;

        public Scheduler(int tickStepMs = MsInSec)
        {
            TickStepMs = tickStepMs;
        }

        public void Start(int timeMs)
        {
            RemainingTime = timeMs;
            _tokenSource = new CancellationTokenSource();
            OnStart?.Invoke(RemainingTime);
            Timer();
        }

        public void Resume()
        {
            OnResume?.Invoke(RemainingTime);
            Timer();
        }

        public void Pause()
        {
            _tokenSource.Cancel();
            _tokenSource = new CancellationTokenSource();
            OnPause?.Invoke(RemainingTime);
        }

        public void Stop()
        {
            if (_tokenSource.IsCancellationRequested)
                return;
            _tokenSource.Cancel();
            OnStop?.Invoke(RemainingTime);
        }

        public async void Timer()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            OnTimerStart?.Invoke();
            while (RemainingTime > 0 && !_tokenSource.IsCancellationRequested)
            {
                await Task.Delay(TickStepMs, _tokenSource.Token);
                RemainingTime -= TickStepMs;
                OnTick?.Invoke(RemainingTime);
            }

            if (RemainingTime <= 0)
                OnFinished?.Invoke();
        }
    }
}
