using System;
using System.Collections.Generic;
using System.Linq;

namespace MattMert.GenericSignals
{
    public interface ISignal
    {
        void PauseAll();
        void ResumeAll();
        void Pause(object id);
        void Resume(object id);
    }
    
    public class GenericSignal : ISignal
    {
        private readonly Dictionary<Action, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch()
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke();
            }
        }

        public void Dispatch(object id)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke();
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
    
    public class GenericSignal<T> : ISignal
    {
        private readonly Dictionary<Action<T>, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action<T> listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T> listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch(T arg1)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1);
            }
        }

        public void Dispatch(object id, T arg1)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1);
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
    
    public class GenericSignal<T, U> : ISignal
    {
        private readonly Dictionary<Action<T, U>, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action<T, U> listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U> listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch(T arg1, U arg2)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2);
            }
        }

        public void Dispatch(object id, T arg1, U arg2)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2);
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
    
    public class GenericSignal<T, U, V> : ISignal
    {
        private readonly Dictionary<Action<T, U, V>, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action<T, U, V> listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V> listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch(T arg1, U arg2, V arg3)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3);
            }
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3);
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
    
    public class GenericSignal<T, U, V, Y> : ISignal
    {
        private readonly Dictionary<Action<T, U, V, Y>, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action<T, U, V, Y> listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V, Y> listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch(T arg1, U arg2, V arg3, Y arg4)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3, arg4);
            }
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3, Y arg4)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3, arg4);
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
    
    public class GenericSignal<T, U, V, Y, Z> : ISignal
    {
        private readonly Dictionary<Action<T, U, V, Y, Z>, ActionInfo> _listeners = new();
        private bool _isAllPaused;
        
        public void AddListener(Action<T, U, V, Y, Z> listener, int order = 0, object id = null)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
            
            _listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V, Y, Z> listener)
        {
            if (_listeners.ContainsKey(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void Dispatch(T arg1, U arg2, V arg3, Y arg4, Z arg5)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable)
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3, Y arg4, Z arg5)
        {
            if (_isAllPaused) return;
            var orderedEnumerable = _listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in orderedEnumerable.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                {
                    return;
                }
                
                pair.Key?.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void PauseAll()
        {
            _isAllPaused = true;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeAll()
        {
            _isAllPaused = false;
            foreach (var pair in _listeners)
            {
                pair.Value.paused = false;
            }
        }

        public void Pause(object id)
        {
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void Resume(object id)
        {
            _isAllPaused = false;
            foreach (var pair in _listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }
        
        private class ActionInfo
        {
            public readonly int order;
            public readonly object id;
            public bool paused { get; set; }

            public ActionInfo(int order, object id)
            {
                this.order = order;
                this.id = id;
                paused = false;
            }
        }
    }
}