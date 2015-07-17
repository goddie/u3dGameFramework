using System;
using System.Collections.Generic;


public interface IEffectContainer : IEffect
{
	List<IEffect> getEffects ();
}
