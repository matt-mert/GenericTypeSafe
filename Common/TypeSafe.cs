using System;
using System.Collections.Generic;

namespace MattMert.Common
{
    public interface ITypeSafe
    {
        string Hash { get; }
        void Destroy();
    }
    
    public static class TypeSafe
    {
        private static readonly TypeSafeHub _hub = new();

        public static T Get<T>() where T : ITypeSafe, new()
        {
            return _hub.Get<T>();
        }

        public static void Destroy<T>() where T : ITypeSafe, new()
        {
            _hub.Destroy<T>();
        }

        public static void DestroyObjectByHash(string hash)
        {
            _hub.DestroyObjectByHash(hash);
        }

        public static void DestroyAllObjects()
        {
            _hub.DestroyAllObjects();
        }
    }

    public class TypeSafeHub
    {
        private readonly Dictionary<Type, ITypeSafe> _objs = new();
        
        public T Get<T>() where T : ITypeSafe, new()
        {
            var type = typeof(T);
            
            if (_objs.TryGetValue(type, out var obj))
            {
                return (T)obj;
            }
            
            return (T)Bind(type);
        }

        public void Destroy<T>() where T : ITypeSafe, new()
        {
            var type = typeof(T);
            if (!_objs.TryGetValue(type, out var obj))
                return;
            
            obj.Destroy();
            _objs.Remove(type);
        }

        public void DestroyObjectByHash(string hash)
        {
            var obj = GetObjectByHash(hash);
            obj?.Destroy();
        }

        public void DestroyAllObjects()
        {
            foreach (var obj in _objs.Values)
            {
                obj.Destroy();
            }
            
            _objs.Clear();
        }
        
        private ITypeSafe Bind(Type type)
        {
            if (_objs.TryGetValue(type, out var obj)) return default;
            obj = (ITypeSafe)Activator.CreateInstance(type);
            _objs.Add(type, obj);
            return obj;
        }

        private ITypeSafe GetObjectByHash(string hash)
        {
            foreach (var obj in _objs.Values)
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