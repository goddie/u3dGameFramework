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

	private AudioSource sound;

	public void Play(string str)
	{


		String res  = "Sound/"+str;

		AudioClip ac = Resources.Load<AudioClip>(str);
		Debug.Log(res);
		Debug.Log(ac);
		return;
		sound.clip = ac ;
		sound.Play();
	}

	/// <summary>
	/// 播放FMOD声音
	/// </summary>
	/// <param name="eventName">Event name.</param>
	/// <param name="volumes">Volumes.</param>
	public void FMODEvent(string eventName,float volumes)
	{
		FMOD_StudioSystem.instance.PlayOneShot("event:/"+eventName,Vector3.zero,volumes);
	}


}
 
