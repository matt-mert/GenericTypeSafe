using System;
using System.Collections.Generic;

namespace MattMert.Common
{
    public interface ITypeSafe
    {
        string Hash { get; }
        void Destroy();
    }

    public abstract class ATypeSafeMain<THub> where THub : ATypeSafeHub, new()
    {
        protected static readonly THub hub = new();

        public static T Get<T>() where T : ITypeSafe, new()
        {
            return hub.Get<T>();
        }

        public static void Destroy<T>() where T : ITypeSafe, new()
        {
            hub.Destroy<T>();
        }

        public static void DestroyByHash(string hash)
        {
            hub.DestroyByHash(hash);
        }

        public static void DestroyAll()
        {
            hub.DestroyAll();
        }
    }

    public abstract class ATypeSafeHub
    {
        protected readonly Dictionary<Type, ITypeSafe> objs = new();

        public T Get<T>() where T : ITypeSafe, new()
        {
            var type = typeof(T);
            if (objs.TryGetValue(type, out var obj))
            {
                return (T)obj;
            }

            return (T)Bind(type);
        }

        public void Destroy<T>() where T : ITypeSafe, new()
        {
            var type = typeof(T);
            if (!objs.TryGetValue(type, out var obj))
                return;

            obj.Destroy();
            objs.Remove(type);
        }

        public void DestroyByHash(string hash)
        {
            var obj = GetObjectByHash(hash);
            obj?.Destroy();
        }

        public void DestroyAll()
        {
            foreach (var obj in objs.Values)
            {
                obj.Destroy();
            }

            objs.Clear();
        }

        private ITypeSafe Bind(Type type)
        {
            if (objs.TryGetValue(type, out var obj)) return obj;
            obj = (ITypeSafe)Activator.CreateInstance(type);
            objs.Add(type, obj);
            return obj;
        }

        private ITypeSafe GetObjectByHash(string hash)
        {
            foreach (var obj in objs.Values)
            {
                if (obj.Hash == hash)
                {
                    return obj;
                }
            }

            return null;
        }
    }

    public abstract class ATypeSafe : ITypeSafe
    {
        private string _hash;

        public string Hash
        {
            get
            {
                if (string.IsNullOrEmpty(_hash))
                {
                    _hash = GetType().ToString();
                }

                return _hash;
            }
        }

        public abstract void Destroy();
    }
}
