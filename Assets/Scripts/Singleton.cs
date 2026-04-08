using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    // public static T Instance { get { return instance; } }
    // public static T Instance => instance;   // C# 6.0

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<T>();  // Lazy Initialization - Find the instance in the scene when first accessed
            
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; // Exit if another instance already exists
        }
        else
        {
            instance = (T)this; // Set the instance to this object
        }

        if (transform.parent == null)
        {
            DontDestroyOnLoad(gameObject); // Make the singleton persist across scenes
        }
    }
}
