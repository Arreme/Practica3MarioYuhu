using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyContainer 
{
        static Dictionary<Type, System.Object> dependencies = new Dictionary<Type, System.Object>();
        public static T GetDependency<T>()
        {
            if (!dependencies.ContainsKey(typeof(T)))
            {
                Debug.LogError("Cannot find: " + typeof(T).ToString() + ".");
                return default; 
            }
            return (T)dependencies[typeof(T)];
        }
        public static void AddDependency<T>(System.Object obj)
        {
            if (dependencies.ContainsKey(typeof(T)))
            {
                dependencies.Remove(typeof(T));
            }
            dependencies.Add(typeof(T), obj);
        }
    
}
