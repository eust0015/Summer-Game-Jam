using UnityEngine;

public class Drink : Item
{
	public override void Use()
	{
		Debug.Log($"You drink the {base.name}");
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}