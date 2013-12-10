using Apollo.DataAccess;
using Apollo.DataAccess.Entities;

namespace Apollo.Core.Cache
{
    public class CacheManager
    {
        private static CacheManager instance;
        private static bool isInitialized;

        public static CacheManager Instance
        {
            get { return CacheManager.instance ?? (CacheManager.instance = new CacheManager()); }
        }
        TraceEventCache traceEventCache;

        public TraceEventCache TraceEventCache
        {
            get { return traceEventCache; }
        }

        private CacheManager()
        {
            isInitialized = false;
        }

        public static void Initialize()
        {
            if (isInitialized)
                return;

            Instance.traceEventCache = new TraceEventCache();
            Instance.TraceEventCache.AddRange(DataConnector.ListDataEntities<TraceEventData>(Query.Create("SELECT * FROM MessageType")));
            isInitialized = true;
        }
    }
}
