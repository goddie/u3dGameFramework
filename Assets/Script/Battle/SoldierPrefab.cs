using System;
using UnityEngine;


/// <summary>
/// 兵种物件
/// </summary>
public class SoldierPrefab : MonoBehaviour
{
	private BattleAgent battleAgent;

	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
		}
	}


}