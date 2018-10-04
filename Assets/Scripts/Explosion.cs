using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	[SerializeField]
	private float explosionSize;
	[SerializeField]
	private float visualExpansionRate;
	[SerializeField]
	private float colliderExpansionRate;

	private SphereCollider sphereCollider;
	private Vector3 expansionVector;

	// Use this for initialization
	void Start () {
		sphereCollider = GetComponent<SphereCollider>();
		expansionVector = new Vector3(visualExpansionRate, visualExpansionRate, visualExpansionRate);
	}
	
	// Update is called once per frame
	void Update () {
		if (sphereCollider.radius < explosionSize) {
			transform.localScale += expansionVector;
			sphereCollider.radius += colliderExpansionRate; // collider increases with scale, but not enough to match visuals
		}
	}
}
