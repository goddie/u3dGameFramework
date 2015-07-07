using System;
using UnityEngine;
 
public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;

	public static AudioManager SharedInstance {
		get {
			if (instance == null) {
				instance = MainComponentManager.AddMainComponent<AudioManager> ();
			}
			return instance;
		}
	}
}
 
