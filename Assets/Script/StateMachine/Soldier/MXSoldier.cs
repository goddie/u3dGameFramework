using System;
public class MXSoldier : HeroSoldier
{
	public MXSoldier ()
	{
	}


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override protected  void InitSound ()
	{
		soundDict.Add (StateId.Attack, "attack_mx");
		soundDict.Add (StateId.Ult, "ult_mx");
		soundDict.Add (StateId.Dead, "dead_mx");
	}

	override protected void InitTimer ()
	{
		attackTimer = TimerManager.SharedInstance.CreateTimer (1.0f, new TimerEventHandler (AttackHandler));
		ultTimer = TimerManager.SharedInstance.CreateTimer (6.0f, new TimerEventHandler (UltHandler));
		
		attackTimer.Start ();
		ultTimer.Start ();
	}

}
