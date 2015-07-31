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

	void Update ()
	{
		LoopPlay ();
	}

	private Dictionary<string,FMOD.Studio.EventInstance> soundList = new Dictionary<string, EventInstance> ();
	private Dictionary<string,FMOD.Studio.EventInstance> loopList = new Dictionary<string, EventInstance> ();
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
		FMOD.Studio.EventInstance soundEvent = soundList [eventName];

		if (soundEvent != null) {

//			FMOD_StudioEventEmitter soundEmitter = gameObject.AddComponent<FMOD_StudioEventEmitter>();
//
//
//			soundEmitter.Stop (STOP_MODE.ALLOWFADEOUT);

			soundEvent.stop (STOP_MODE.ALLOWFADEOUT);

			soundList.Remove (eventName);

			loopList.Remove (eventName);
		}

	}

	/// <summary>
	/// 播放声音
	/// </summary>
	/// <param name="eventName">Event name.</param>
	/// <param name="volume">Volume.</param>
	public void PlaySound (string eventName, float volume)
	{
		PlaySound (eventName, volume, false);
	}


	/// <summary>
	/// 播放声音
	/// </summary>
	/// <param name="eventName">Event name.</param>
	/// <param name="volume">Volume.</param>
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	public void PlaySound (string eventName, float volume, bool isLoop)
	{



		FMOD.Studio.EventInstance sound = FMOD_StudioSystem.instance.GetEvent ("event:/" + eventName);
		
		if (!soundList.ContainsKey (eventName)) {
			soundList.Add (eventName, sound);
		}


		if (isLoop && !loopList.ContainsKey (eventName)) {
			
			loopList.Add (eventName, sound);
				
		}

		sound.setVolume (volume);
  
 
		//FMOD_LOOP_NORMAL
		sound.start ();

 
	}

	/// <summary>
	/// 循环需要循环的音乐
	/// </summary>
	public void LoopPlay ()
	{

		List<string> test = new List<string>(loopList.Keys);
		
		
		
		for (int i = 0; i < loopList.Count; i++)
		{

			EventInstance eve = loopList[test[i]];
			
			PLAYBACK_STATE state = PLAYBACK_STATE.PLAYING;
			eve.getPlaybackState (out state);
			if (state == PLAYBACK_STATE.STOPPED) {
				eve.start ();
			}
		}
 

	}

}
 
