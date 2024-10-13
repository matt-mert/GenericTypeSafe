using System;
using MattMert.Common;
using UnityEngine;

namespace MattMert.GenericPools
{
    public class Pools : ATypeSafeMain<PoolHub>
    {
    }

    public class PoolHub : ATypeSafeHub
    {
    }

    public abstract class APool<T> : ATypeSafe where T : Component, IPoolObject
    {
        private const int DefaultCapacity = 10;
        private const int DefaultMaxSize = 1000;

        private GenericPool<T> _pool;
        private Transform _root;
        private T _prefab;
        private bool _initialized;

        public override void Destroy()
        {
            _pool.Dispose();
            _pool = null;
        }

        public void Initialize(T prefab, Transform root = null, int prefillAmount = 0,
            int capacity = -1, int maxSize = -1, bool collectionCheck = true)
        {
            if (_initialized)
            {
                Debug.LogError($"Pool of type {typeof(T).Name} is already initialized!");
                return;
            }

            if (!prefab)
                throw new ArgumentNullException(nameof(prefab));

            if (capacity == -1) capacity = DefaultCapacity;
            if (maxSize == -1) maxSize = DefaultMaxSize;

            _prefab = prefab;
            _root = root;
            _pool = new GenericPool<T>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
                root, prefillAmount, capacity, maxSize, collectionCheck);
        }

        public void Initialize(Func<T> createFunc, Action<T> actionOnGet, Action<T> actionOnRelease, Action<T> actionOnDestroy,
            Transform root = null, int prefillAmount = 0, int capacity = -1, int maxSize = -1, bool collectionCheck = true)
        {
            if (_initialized)
            {
                Debug.LogError($"Pool of type {typeof(T).Name} is already initialized!");
                return;
            }

            if (capacity == -1) capacity = DefaultCapacity;
            if (maxSize == -1) maxSize = DefaultMaxSize;

            _root = root;
            _pool = new GenericPool<T>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy,
                root, prefillAmount, capacity, maxSize, collectionCheck);
        }

        public int GetActiveCount()
        {
            if (!CheckIfInitialized()) return -1;
            return _pool.GetActiveCount();
        }

        public int GetInactiveCount()
        {
            if (!CheckIfInitialized()) return -1;
            return _pool.GetInactiveCount();
        }

        public int GetCountAll()
        {
            if (!CheckIfInitialized()) return -1;
            return _pool.GetCountAll();
        }

        public T Get()
        {
            if (!CheckIfInitialized()) return null;
            return _pool.Get();
        }

        public void Release(T obj)
        {
            if (!CheckIfInitialized()) return;
            _pool.Release(obj);
        }

        public void Fill(int amount)
        {
            if (!CheckIfInitialized()) return;
            _pool.Fill(amount);
        }

        public void Clear()
        {
            if (!CheckIfInitialized()) return;
            _pool.Clear();
        }

        public void Dispose()
        {
            if (!CheckIfInitialized()) return;
            _pool.Dispose();
        }

        private bool CheckIfInitialized()
        {
            if (!_initialized)
                Debug.LogError($"Pool of type {typeof(T).Name} is not initialized yet!");

            return _initialized;
        }

        protected virtual T CreateFunc()
        {
            var obj = UnityEngine.Object.Instantiate(_prefab, _root);
            obj.gameObject.SetActive(false);
            return obj;
        }

        protected virtual void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_root);
        }

        protected virtual void ActionOnDestroy(T obj)
        {
            UnityEngine.Object.Destroy(obj.gameObject);
        }
    }
}
