using System;
using System.Collections.Generic;
using System.Linq;

namespace MattMert.GenericSignals
{
    public interface ISignal
    {
        bool IsPaused { get; }
        object SignalId { get; }
        void SetId(object id);
        void PauseSignal();
        void ResumeSignal();
        void PauseListener(object id);
        void ResumeListener(object id);
    }

    public abstract class AGenericSignal<T> : ISignal
    {
        public bool IsPaused { get; private set; }
        public object SignalId { get; private set; }
        protected readonly Dictionary<T, ActionInfo> listeners = new();

        public void SetId(object id)
        {
            SignalId = id;
        }

        public void RemoveAllListeners()
        {
            listeners.Clear();
        }

        public void PauseSignal()
        {
            IsPaused = true;
        }

        public void ResumeSignal()
        {
            IsPaused = false;
        }

        public void PauseListener(object id)
        {
            foreach (var pair in listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = true;
            }
        }

        public void ResumeListener(object id)
        {
            foreach (var pair in listeners.Where(pair => pair.Value.id == id))
            {
                pair.Value.paused = false;
            }
        }

        protected void ForEachOrdered(Action<T> handler)
        {
            var ordered = listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in ordered)
            {
                if (pair.Value.paused)
                    continue;

                handler.Invoke(pair.Key);
            }
        }

        protected void ForEachOrdered(object id, Action<T> handler)
        {
            var ordered = listeners.OrderBy(pair => pair.Value.order);
            foreach (var pair in ordered.Where(pair => pair.Value.id == id))
            {
                if (pair.Value.paused)
                    continue;

                handler.Invoke(pair.Key);
            }
        }
    }

    public class GenericSignal : AGenericSignal<Action>
    {
        public void AddListener(Action listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch()
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke());
        }

        public void Dispatch(object id)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke());
        }
    }

    public class GenericSignal<T> : AGenericSignal<Action<T>>
    {
        public void AddListener(Action<T> listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T> listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch(T arg1)
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke(arg1));
        }

        public void Dispatch(object id, T arg1)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke(arg1));
        }
    }

    public class GenericSignal<T, U> : AGenericSignal<Action<T, U>>
    {
        public void AddListener(Action<T, U> listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U> listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch(T arg1, U arg2)
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke(arg1, arg2));
        }

        public void Dispatch(object id, T arg1, U arg2)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke(arg1, arg2));
        }
    }

    public class GenericSignal<T, U, V> : AGenericSignal<Action<T, U, V>>
    {
        public void AddListener(Action<T, U, V> listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V> listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch(T arg1, U arg2, V arg3)
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke(arg1, arg2, arg3));
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke(arg1, arg2, arg3));
        }
    }

    public class GenericSignal<T, U, V, Y> : AGenericSignal<Action<T, U, V, Y>>
    {
        public void AddListener(Action<T, U, V, Y> listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V, Y> listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch(T arg1, U arg2, V arg3, Y arg4)
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke(arg1, arg2, arg3, arg4));
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3, Y arg4)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke(arg1, arg2, arg3, arg4));
        }
    }

    public class GenericSignal<T, U, V, Y, Z> : AGenericSignal<Action<T, U, V, Y, Z>>
    {
        public void AddListener(Action<T, U, V, Y, Z> listener, int order = 0, object id = null)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);

            listeners.Add(listener, new ActionInfo(order, id));
        }

        public void RemoveListener(Action<T, U, V, Y, Z> listener)
        {
            if (listeners.ContainsKey(listener))
                listeners.Remove(listener);
        }

        public void Dispatch(T arg1, U arg2, V arg3, Y arg4, Z arg5)
        {
            if (IsPaused) return;
            ForEachOrdered(action => action?.Invoke(arg1, arg2, arg3, arg4, arg5));
        }

        public void Dispatch(object id, T arg1, U arg2, V arg3, Y arg4, Z arg5)
        {
            if (IsPaused) return;
            ForEachOrdered(id, action => action?.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
    }

    public class ActionInfo
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
