using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectsPool<T> where T : Component
    {
        private readonly Stack<T> _objects = new Stack<T>();
        private readonly Func<T> _createFunc;
        private readonly Action<T> _resetFunc;
        private readonly Transform _orphansRoot;

        public ObjectsPool(Func<T> createFunc, Action<T> resetFunc)
        {
            _createFunc = createFunc;
            _resetFunc = resetFunc;
            var orphansGameObject = GameObject.Find("OrphansRoot") ?? new GameObject("OrphansRoot");
            orphansGameObject.SetActive(false);
            _orphansRoot = orphansGameObject.transform;
        }

        public T Borrow()
        {
            return _objects.Count > 0 ? _objects.Pop() : _createFunc();
        }

        public void Release(T value)
        {
            _resetFunc(value);
            _objects.Push(value);
            value.transform.SetParent(_orphansRoot);
        }
    }
}