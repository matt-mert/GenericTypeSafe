using System;
using UnityEngine;
using UnityEngine.Pool;

namespace MattMert.GenericPools
{
    public interface IPoolObject
    {
        void OnGet();
        void OnRelease();
    }

    public class GenericPool<T> where T : Component, IPoolObject
    {
        private const int defaultCapacity = 10;
        private const int defaultMaxSize = 1000;

        private ObjectPool<T> _pool;
        private Transform _root;
        private T _prefab;

        public GenericPool(T prefab, Transform root = null, int prefillAmount = 0,
            int capacity = -1, int maxSize = -1, bool collectionCheck = true)
        {
            if (!prefab)
                throw new ArgumentNullException(nameof(prefab));

            if (capacity == -1) capacity = defaultCapacity;
            if (maxSize == -1) maxSize = defaultMaxSize;

            _prefab = prefab;
            _root = root ? new GameObject($"Pool_{prefab.name}").transform : root;
            _pool = new ObjectPool<T>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
                collectionCheck, capacity, maxSize);

            if (prefillAmount > 0)
                Fill(prefillAmount);
        }

        public GenericPool(Func<T> createFunc, Action<T> actionOnGet, Action<T> actionOnRelease,
            Action<T> actionOnDestroy, Transform root = null, int prefillAmount = 0,
            int capacity = -1, int maxSize = -1, bool collectionCheck = true)
        {
            _root = root ? new GameObject($"Pool_{typeof(T).Name}").transform : root;
            _pool = new ObjectPool<T>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy,
                collectionCheck, capacity, maxSize);

            if (prefillAmount > 0)
                Fill(prefillAmount);
        }

        public int GetActiveCount() => _pool.CountActive;

        public int GetInactiveCount() => _pool.CountInactive;

        public int GetCountAll() => _pool.CountAll;

        public T Get()
        {
            var obj = _pool.Get();
            obj.OnGet();
            return obj;
        }

        public void Release(T obj)
        {
            obj.OnRelease();
            _pool.Release(obj);
        }

        public void Fill(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var t = _pool.Get();
                _pool.Release(t);
            }
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public void Dispose()
        {
            _pool.Dispose();
        }

        private T CreateFunc()
        {
            var obj = UnityEngine.Object.Instantiate(_prefab, _root);
            obj.gameObject.SetActive(false);
            return obj;
        }

        private void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_root);
        }

        private void ActionOnDestroy(T obj)
        {
            UnityEngine.Object.Destroy(obj.gameObject);
        }
    }
}
