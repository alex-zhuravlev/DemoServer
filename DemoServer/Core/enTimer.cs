using System;

namespace DemoServer.Core
{
    public class enTimer
    {
        public enTimer()
        {

        }

        public enTimer(double fTime)
        {
            Time = fTime;
        }

        public void StartTimer(double fTime = 0.0f)
        {
            if (fTime != 0.0f)
            {
                Time = fTime;
            }

            StartTime = DateTime.UtcNow;

            IsWorking = true;
        }

        public void RestartTimer(double fTime)
        {
            StopTimer();
            StartTimer(fTime);
        }

        public bool IsElapsed()
        {
            return IsElapsed(Time);
        }

        public bool IsElapsed(double fTime)
        {
            if (!IsWorking) return false;

            return (GetElapsedTime() >= fTime);
        }

        public double GetElapsedTime()
        {
            TimeSpan oTimeDiff = DateTime.UtcNow - StartTime;
            return oTimeDiff.TotalMilliseconds;
        }

        public void StopTimer()
        {
            IsWorking = false;
        }

        public DateTime StartTime { get; private set; } = DateTime.UtcNow;

        public double Time { get; private set; } = 0.0f;

        public bool IsWorking { get; private set; } = false;
    }
}
