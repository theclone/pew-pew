using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private const string LogPrefix = "[Singleton] ";
	private const string GetInstanceWhenQuittingMessage = 
		" type Instance already destroyed on application quit. " +
		"Won't create again - returning null.";
	private const string MultipleInstanceError = 
		"Something went really wrong " +
		" - there should never be more than 1 singleton!" +
		" Reopening the scene might fix it.";
	private const string SingletonObjectPrefix = "(Singleton) ";
	private const string SingletonCreatedMessage = 
		" was created with DontDestroyOnLoad, as an instance was needed.";
	private const string SingletonAlreadyCreatedMessage =
		"Using instance already created: ";

	private static T _instance;

	private static object _lock = new object();

	public static T Instance
	{
		get
		{
			if (applicationIsQuitting)
			{
				Debug.LogWarning(LogPrefix + typeof(T) + GetInstanceWhenQuittingMessage);
				return null;
			}

			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));

					if (FindObjectsOfType(typeof(T)).Length > 1)
					{
						Debug.LogError(LogPrefix + MultipleInstanceError);
						return _instance;
					}
				} 

				if (_instance == null)
				{
					GameObject singleton = new GameObject();
					_instance = singleton.AddComponent<T>();
					singleton.name = SingletonObjectPrefix + typeof(T).ToString();

					DontDestroyOnLoad(singleton);
					
					Debug.Log(LogPrefix + singleton.name + SingletonCreatedMessage);
				} else {
					Debug.Log(LogPrefix + SingletonAlreadyCreatedMessage + _instance.gameObject.name);
				}
				
				return _instance;
			}
		}
	}

	private static bool applicationIsQuitting = false;
	// When Unity quits, it destroys objects in a random order.
	// In principle, a Singleton is only destroyed when application quits.
	// If any script calls Instance after it have been destroyed, 
	//   it will create a buggy ghost object that will stay on the Editor scene
	//   even after stopping playing the Application. Really bad!
	// So, this was made to be sure we're not creating that buggy ghost object.
	public void OnDestroy () {
		applicationIsQuitting = true;
	}
}
