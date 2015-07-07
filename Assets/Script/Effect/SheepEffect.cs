using System;
public class SheepEffect : IEffect
{
	public void cast (BattleAgent target)
	{
		//target.gameObject = GameManager.getAnimeObject ("sheep");
	}

	public void reverse ()
	{
		throw new NotImplementedException ();
	}
}