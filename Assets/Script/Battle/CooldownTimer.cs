using System;

/// <summary>
/// 冷却计时器
/// </summary>
using UnityEngine;


public class CooldownTimer
{

	public CooldownTimer (float second)
	{
		nextActive = 0;
		Duration = second;
		IsActive = false;
	}

	public TimerEventHandler Tick;

	/// <summary>
	/// 下一次激活时间
	/// </summary>
	private float nextActive;
 

	/// <summary>
	/// 计时器名称
	/// </summary>
	/// <value>The name.</value>
	public String Name {
		get; 
		set;
	}

	/// <summary>
	/// 是否可用
	/// </summary>
	/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
	public Boolean IsActive { 
		get; 
		set;
	}


	/// <summary>
	/// 时长 秒钟
	/// </summary>
	/// <value>The time count.</value>
	public float Duration {
		get;
		set;
	}


	/// <summary>
	/// Update the specified castTime.
	/// </summary>
	/// <param name="time">游戏运行到现在的时间</param>
	public void Update (float time)
	{

		if (IsActive) {

			//Debug.Log("time:"+time+" nextActive:"+nextActive);

			if (time > nextActive) {
				nextActive =  time + Duration;
				//IsActive must set false before tick() , cause if u want to restart in the tick() , IsActive would be reset to fasle .
				//IsActive = false;
				Tick.Invoke ();
			}
		}
	}

	public void Stop ()
	{
		IsActive = false;
	}

	/// <summary>
	/// 开启
	/// </summary>
	public void Start ()
	{
		IsActive = true;
	}


	/// <summary>
	/// 继续
	/// </summary>
	public void Continue ()
	{
		IsActive = true;
	}
	
	/// <summary>
	/// 重启动
	/// </summary>
	public void Restart ()
	{
		IsActive = true;
		nextActive = 0.0f;
	}
	
	/// <summary>
	/// 重设时长
	/// </summary>
	/// <param name="second">时长</param>
	public void ResetTimeCount (float second)
	{
		Duration = second;
	}

}

public delegate void TimerEventHandler ();
