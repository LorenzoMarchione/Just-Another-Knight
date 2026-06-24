using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    //dictionary to save all necessary components between scenes
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();
    //take an object and save it in services with type of object as key
    public static void Register<T>(T service) => services[typeof(T)] = service;
    //return object that matches type of T, else return null
    public static T Get<T>() => services.TryGetValue(typeof(T), out var service) ? (T)service : default;
}
