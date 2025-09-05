using bGUI.Core.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace bGUI.Services
{
    /// <summary>
    /// Service for pooling and reusing UI elements.
    /// </summary>
    public class PoolingService
    {
        private Dictionary<Type, List<IUIElement>> _pools = new Dictionary<Type, List<IUIElement>>();

        /// <summary>
        /// Tries to get a pooled object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of UI element to get</typeparam>
        /// <param name="element">The output element if found</param>
        /// <returns>True if an element was found, false otherwise</returns>
        public bool TryGetPooledObject<T>([NotNullWhen(true)] out T? element) where T : class, IUIElement
        {
            Type type = typeof(T);
            element = null;

            if (!_pools.TryGetValue(type, out var pool) || pool.Count == 0)
            {
                return false;
            }

            int lastIndex = pool.Count - 1;
            var pooledElement = pool[lastIndex];
            pool.RemoveAt(lastIndex);

            element = pooledElement as T;
            return element != null;
        }

        /// <summary>
        /// Returns an element to the pool.
        /// </summary>
        /// <param name="element">The element to pool</param>
        public void ReturnToPool(IUIElement element)
        {
            if (element == null)
            {
                return;
            }

            Type type = element.GetType();

            if (!_pools.TryGetValue(type, out var pool))
            {
                pool = new List<IUIElement>();
                _pools[type] = pool;
            }

            // Ensure the object is not already in the pool
            if (!pool.Contains(element))
            {
                pool.Add(element);
            }
        }

        /// <summary>
        /// Clears all pools.
        /// </summary>
        public void ClearPools()
        {
            foreach (var pool in _pools.Values)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    pool[i].Destroy();
                }
                pool.Clear();
            }
            _pools.Clear();
        }

        /// <summary>
        /// Clears a specific pool.
        /// </summary>
        /// <typeparam name="T">The type of UI element to clear</typeparam>
        public void ClearPool<T>() where T : class, IUIElement
        {
            Type type = typeof(T);

            if (_pools.TryGetValue(type, out var pool))
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    pool[i].Destroy();
                }
                pool.Clear();
                _pools.Remove(type);
            }
        }
    }
}
