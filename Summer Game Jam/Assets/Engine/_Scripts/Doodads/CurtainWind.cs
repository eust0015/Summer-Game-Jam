using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CurtainWind : MonoBehaviour
{
    [SerializeField] Rigidbody curtainRigidbody;
    [Range(0f, 2f)] [SerializeField] float windStrength = 0.5f;
    [SerializeField] Vector3 windDirection = Vector3.right;
    [SerializeField] Vector2 windInterval = new Vector2(0.25f, 1.5f);

    Vector3 windForce = Vector3.zero;
	IEnumerator StartWind()
    {
        while (true)
        {
            float randomModifier = Random.Range(0.1f, 1f); // Randomize the wind strength slightly
			windForce = (windDirection.normalized * windStrength) * randomModifier;
			yield return new WaitForSeconds(Random.Range(windInterval.x, windInterval.y)); // Adjust the frequency of wind application as needed
        }
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        StartCoroutine(StartWind());
	}

	private void FixedUpdate()
	{
		curtainRigidbody.AddForce(windForce * windStrength, ForceMode.Force);
	}
}
