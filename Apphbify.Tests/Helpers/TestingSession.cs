using System.Collections;
using System.Collections.Generic;
using Nancy.Bootstrapper;
using Nancy.Session;

namespace Apphbify.Tests.Helpers
{
    public class TestingSession : ISession
    {
        public static void Enable(IPipelines pipelines, Dictionary<string, object> sessionData)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                ctx.Request.Session = new TestingSession(sessionData);
                return null;
            });
        }

        private Dictionary<string, object> _Data;
        private bool _HasChanged = false;

        public TestingSession(Dictionary<string, object> sessionData)
        {
            _Data = sessionData ?? new Dictionary<string, object>();
        }

        public int Count { get { return _Data.Count; } }

        public object this[string key] { get { return _Data.ContainsKey(key) ? _Data[key] : null; } set { _Data[key] = value; _HasChanged = true; } }

        public void Delete(string key)
        {
            _Data.Remove(key);
            _HasChanged = true;
        }

        public void DeleteAll()
        {
            _Data = new Dictionary<string, object>();
            _HasChanged = true;
        }

        public bool HasChanged
        {
            get { return _HasChanged; }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Data.GetEnumerator();
        }
    }
}