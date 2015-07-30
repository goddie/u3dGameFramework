using System;
using UnityEngine;
using FMOD.Studio;
using System.Collections.Generic;
 
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

	private Dictionary<string,FMOD.Studio.EventInstance> soundList = new Dictionary<string, EventInstance> ();

	/// <summary>
	/// 播放FMOD声音
	/// </summary>
	/// <param name="eventName">Event name.</param>
	/// <param name="volumes">Volumes.</param>
	public void PlayOneShot (string eventName, float volumes)
	{

		FMOD_StudioSystem.instance.PlayOneShot ("event:/" + eventName, Vector3.zero, volumes);
	}

	public void StopSound (string eventName)
	{
		FMOD.Studio.EventInstance soundEvent  = soundList[eventName];

		if (soundEvent!=null) {

//			FMOD_StudioEventEmitter soundEmitter = gameObject.AddComponent<FMOD_StudioEventEmitter>();
//
//
//			soundEmitter.Stop (STOP_MODE.ALLOWFADEOUT);

			soundEvent.stop (STOP_MODE.ALLOWFADEOUT);
		}

	}

	public void PlaySound (string eventName, float volume)
	{
		FMOD.Studio.EventInstance sound = FMOD_StudioSystem.instance.GetEvent ("event:/" + eventName);
		soundList.Add (eventName, sound);
		sound.setVolume (volume);
		sound.start ();
	}

}
 
