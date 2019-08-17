using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class ObjectsPool<T> where T : Component
    {
        private static bool _isOrphansRootCreated;
        private static Transform _orphansRoot;
        private static Transform OrphansRoot => _isOrphansRootCreated ? _orphansRoot : (_orphansRoot = CreateOrphansRoot());
        
        private readonly Stack<T> _objects = new Stack<T>();
        private readonly Func<T> _createFunc;
        private readonly Action<T> _resetFunc;

        public ObjectsPool(Func<T> createFunc, Action<T> resetFunc)
        {
            _createFunc = createFunc;
            _resetFunc = resetFunc;
        }

        public T Borrow()
        {
            return _objects.Count > 0 ? _objects.Pop() : _createFunc();
        }

        public void Release(T value)
        {
            _resetFunc(value);
            _objects.Push(value);
            value.transform.SetParent(OrphansRoot);
        }

        private static Transform CreateOrphansRoot()
        {
            var orphansGameObject = new GameObject("OrphansRoot");
            orphansGameObject.SetActive(false);
            Object.DontDestroyOnLoad(orphansGameObject);
            return orphansGameObject.transform;
        }
    }
}