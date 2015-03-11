using System;

namespace SimpleCacheManager
{
    class ExpirationCount
    {
        private readonly DateTime _utcNowTimeStamp;
        private readonly TimeSpan _lifeSpan;

        internal ExpirationCount(string key, DateTime utcNowTimeStamp, TimeSpan lifeSpan)
        {
            _utcNowTimeStamp = utcNowTimeStamp;
            _lifeSpan = lifeSpan;
            Key = key;
        }

        internal string Key { get; private set; }

        internal bool IsOld(DateTime utcNow)
        {
            return (utcNow - _utcNowTimeStamp) > _lifeSpan;
        }
    }
}