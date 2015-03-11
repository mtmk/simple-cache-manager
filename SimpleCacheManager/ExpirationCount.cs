using System;

namespace SimpleCacheManager
{
    class ExpirationCount
    {
        private readonly DateTime _timeStamp;
        private readonly TimeSpan _lifeSpan;

        internal ExpirationCount(string key, DateTime utcNow, TimeSpan lifeSpan)
        {
            _timeStamp = utcNow;
            _lifeSpan = lifeSpan;
            Key = key;
        }

        internal string Key { get; private set; }

        internal bool IsOld(DateTime utcNow)
        {
            return (utcNow - _timeStamp) > _lifeSpan;
        }
    }
}