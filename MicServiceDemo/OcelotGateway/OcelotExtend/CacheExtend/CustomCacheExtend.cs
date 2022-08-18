using Ocelot.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OcelotGateway.OcelotExtend.CacheExtend
{
    /// <summary>
    /// 自定义的缓存扩展
    /// </summary>
    public class CustomCacheExtend : IOcelotCache<CachedResponse>
    {
        private class CacheDataModel
        {
            public CachedResponse CachedResponse { get; set; }
            public DateTime Timeout { get; set; }
            public string Region { get; set; }
        }

        private static Dictionary<string, CacheDataModel> CustomCacheExtendDictionary = new
            Dictionary<string, CacheDataModel>();

        public void Add(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            Console.WriteLine($"This is {nameof(CustomCacheExtend)}.{nameof(Add)}");
            //CustomCacheExtendDictionary.Add(key, new CacheDataModel()
            //{
            //    CachedResponse = value,
            //    Region = region,
            //    Timeout = DateTime.Now.Add(ttl)
            //});
            CustomCacheExtendDictionary[key] = new CacheDataModel()
            {
                CachedResponse = value,
                Region = region,
                Timeout = DateTime.Now.Add(ttl)
            };
        }

        public void AddAndDelete(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            Console.WriteLine($"This is {nameof(CustomCacheExtend)}.{nameof(AddAndDelete)}");
            CustomCacheExtendDictionary[key] = new CacheDataModel()
            {
                CachedResponse = value,
                Region = region,
                Timeout = DateTime.Now.Add(ttl)
            };
        }

        public void ClearRegion(string region)
        {
            Console.WriteLine($"This is {nameof(CustomCacheExtend)}.{nameof(ClearRegion)}");
            var keyList = CustomCacheExtendDictionary.Where(kv => kv.Value.Region.Equals(region)).Select(kv => kv.Key);
            foreach (var key in keyList)
            {
                CustomCacheExtendDictionary.Remove(key);
            }
        }

        public CachedResponse Get(string key, string region)
        {
            Console.WriteLine($"This is {nameof(CustomCacheExtend)}.{nameof(Get)}");
            if (CustomCacheExtendDictionary.ContainsKey(key) && CustomCacheExtendDictionary[key] != null
                && CustomCacheExtendDictionary[key].Timeout > DateTime.Now
                && CustomCacheExtendDictionary[key].Region.Equals(region))
            {
                return CustomCacheExtendDictionary[key].CachedResponse;
            }
            else
                return null;
        }
    }
}
