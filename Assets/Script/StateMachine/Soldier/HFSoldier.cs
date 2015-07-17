using System;

/// <summary>
/// 黑风
/// </summary>
public class HFSoldier : EnemySoldier
{

	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override protected void InitSound()
	{
		soundDict.Add(StateId.Attack,"attack_hf");
		soundDict.Add(StateId.Dead,"dead_hf");
	}
}
