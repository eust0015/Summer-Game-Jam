using UnityEngine;

public class Clothes : Item, IInteractable
{

	public new void OnFocus()
	{
		base.OnFocus();
	}

	public new void OnUnfocus()
	{
		base.OnUnfocus();
	}

	public override void Use()
	{
		
	}
}
