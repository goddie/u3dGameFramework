using System;
using UnityEngine;


 
/// <summary>
/// 组件管理器
/// MainComponentManager在场景中创建一个空的GameObject名字为Main
/// </summary>
public class MainComponentManager
{
	/// <summary>
	/// 持久化 GameObject
	/// </summary>
	GameObject main;

	private static MainComponentManager instance;

	public static void CreateInstance ()
	{
		if (instance == null) {
			instance = new MainComponentManager ();
			GameObject go = GameObject.Find ("Main");
			if (go == null) {
				go = new GameObject ("Main");
				instance.main = go;
				// important: make game object persistent:
				UnityEngine.Object.DontDestroyOnLoad (go);
			}
			// trigger instantiation of other singletons
			// Component c = MenuManager.SharedInstance;
			// ...

			//Component stage = StageManager.SharedInstance;

			Component ultraSpell = UltraSpellManager.SharedInstance;
			Component audio = AudioManager.SharedInstance;
		}
	}



	public static MainComponentManager SharedInstance {
		get {
			if (instance == null) {
				CreateInstance ();
			}
			return instance;
		}
	}

	public static T AddMainComponent <T> () where T : UnityEngine.Component
	{
		T t = SharedInstance.main.GetComponent<T> ();
		if (t != null) {
			return t;
		}
		return SharedInstance.main.AddComponent <T> ();
	}
}

 
