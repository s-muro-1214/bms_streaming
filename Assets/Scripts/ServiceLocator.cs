using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _instances = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Initialize()
    {
        ToolInitializer.Init();

        _instances.Clear();
    }

    public static void Register<T>(T instance) where T : class
    {
        if (_instances.ContainsKey(typeof(T)))
        {
            return;
        }

        _instances[typeof(T)] = instance;
    }

    public static void Unregister<T>(T instance) where T : class
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            return;
        }
        if (!Equals(_instances[typeof(T)], instance))
        {
            return;
        }

        _instances.Remove(typeof(T));
    }

    public static bool IsRegistered<T>() where T : class
    {
        return _instances.ContainsKey(typeof(T));
    }

    public static bool IsRegistered<T>(T instance) where T : class
    {
        return _instances.ContainsKey(typeof(T)) && Equals(_instances[typeof(T)], instance);
    }

    public static T GetInstance<T>() where T : class
    {
        if (_instances.ContainsKey(typeof(T)))
        {
            return _instances[typeof(T)] as T;
        }

        return null;
    }

    public static bool TryGetInstance<T>(out T instance) where T : class
    {
        instance = _instances.ContainsKey(typeof(T)) ? _instances[typeof(T)] as T : null;

        return instance != null;
    }
}
