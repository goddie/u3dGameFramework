using System;

/// <summary>
/// 计时器控制器
/// </summary>
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
	private static TimerManager instance = null;
	
	public static TimerManager SharedInstance {
		get {
			if (instance == null) {
				instance = MainComponentManager.AddMainComponent<TimerManager> ();
			}
			return instance;
		}
	}

	private List<CooldownTimer> list = new List<CooldownTimer> ();

	void Update ()
	{
		for (int i = 0; i < list.Count; i++) {
			list [i].Update (Time.time);
		}
	}

	void OnDestory ()
	{

	}

	public void AddTimer (CooldownTimer timer)
	{
		list.Add (timer);
	}

	public void RemoveTimer (CooldownTimer timer)
	{
		if (list.Contains (timer)) {
			list.Remove (timer);
		}
	}

	public CooldownTimer CreateTimer (float second,TimerEventHandler action)
	{

		//Debug.Log("CreateTimer");

		CooldownTimer cooldownTimer = new CooldownTimer (second);
		cooldownTimer.Tick = action;
		AddTimer (cooldownTimer);

		return cooldownTimer;
	}


}
 
