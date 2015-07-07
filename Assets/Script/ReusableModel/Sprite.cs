using System;
using UnityEngine;

/// <summary>
/// 控制动画播放的精灵
/// </summary>
public class Sprite : EventDispatcherBase
{

	private Animator animator;
	private AnimatorStateInfo currentBaseStage;
	private StateId stateId;

	private BattleAgent soldier;

	public BattleAgent Soldier {
		get {
			return soldier;
		}
		set {
			soldier = value;
		}
	}

 	
	public Sprite ()
	{
		this.Soldier = soldier;

		animator = soldier.gameObject.GetComponent<Animator> ();
		currentBaseStage = animator.GetCurrentAnimatorStateInfo (0);
		addEventListener (SoldierEvent.ATTACK, soldierAttackHandler);
	}



	void soldierAttackHandler (CEvent e)
	{
		StateId stateId = (StateId)e.data;

		ToggleState (stateId);
	}

	/// <summary>
	/// 动画状态切换
	/// </summary>
	/// <param name="state">State.</param>
	public void ToggleState (StateId stateId)
	{
		SetBool (stateId, true);
	}


	void SetBool (StateId stateId, Boolean value)
	{
		string enumName = Enum.GetName (typeof(StateId), stateId);
		string stateName = enumName.Replace ("State", "").ToLower ();
		Debug.Log (stateName);
		animator.SetBool (stateName, value);
	}


	/// <summary>
	/// 场景坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetStagePosition (float x, float y)
	{
		Debug.Log ("Sprite SetStagePosition");
		Soldier.gameObject.transform.Translate (x, y, 0);

	}


	/// <summary>
	/// 地图坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetMapPosition (float x, float y)
	{
		
	}
}