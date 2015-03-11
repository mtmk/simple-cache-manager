# Simple Cache Manager .Net

[![Build status](https://ci.appveyor.com/api/projects/status/sf5iy7rm6yr130aq?svg=true)](https://ci.appveyor.com/project/mtmk/simple-cache-manager)

This is a very simple cache manager example with expiration. It is a thin wrapper around a dictionary.

```cs
var cacheManager = new CacheManager<string>();

cacheManager.Add("key1", "value1", DateTime.UtcNow, TimeSpan.FromSeconds(300));
cacheManager.Add("key2", "value2", DateTime.UtcNow, TimeSpan.FromSeconds(600));

Assert.True(cacheManager.Contains("key1"));
Assert.True(cacheManager.Contains("key1"));

Assert.Equal("value1", cacheManager.GetData("key1"));
Assert.Equal("value1", cacheManager.GetData("key1"));
```

Enjoy
