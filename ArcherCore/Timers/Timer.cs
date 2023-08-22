using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Timers
{
    public struct Timer
    {
        private long _startTime;
        private long _elapsedTime;
        private byte _isRunning;

        public static Timer Create()
        {
            return default;
        }

        public static Timer Initiate()
        {
            var timer = default(Timer);
            timer.Start();
            return timer;
        }

        public bool IsRunning() => _isRunning == 1;
        public long GetElapsedTicks() => IsRunning() ? _elapsedTime + GetDelta() : _elapsedTime;
        public double GetElapsedSeconds() => GetElapsedTicks() / GetFrequency();
        public double GetElapsedMilliseconds() => GetElapsedSeconds() * 1000.0f;
        public double Now() => GetElapsedSeconds();
        private static long GetTimestamp() => Stopwatch.GetTimestamp();
        private static double GetFrequency() => Stopwatch.Frequency;
        private long GetDelta() => GetTimestamp() - _startTime;

        public void Start()
        {
            if (IsRunning()) return;

            _startTime = GetTimestamp();
            _isRunning = 1;
        }

        public void Stop()
        {
            var delta = GetDelta();

            if (!IsRunning()) return;

            _elapsedTime += delta;
            _isRunning = 0;

            if (_elapsedTime < 0)
                _elapsedTime = 0;
        }

        public void Reset()
        {
            _startTime = 0;
            _elapsedTime = 0;
            _isRunning = 0;
        }

        public void Restart()
        {
            _elapsedTime = 0;
            _isRunning = 1;
            _startTime = GetTimestamp();
        }
    }
}
