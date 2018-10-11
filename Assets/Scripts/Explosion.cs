using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
	public const float MinExplosionRange = 0f;
	public const float MaxExplosionRange = 20f;
	public const float MinVisualExpansionRange = 0f;
	public const float MaxVisualExpansionRange = 1f;
	public const float MinColliderExpansionRange = 0f;
	public const float MaxColliderExpansionRange = 0.1f;

	[Range(MinExplosionRange, MaxExplosionRange)]
    [SerializeField]
    private float maxExplosionRadius;
	[Range(MinVisualExpansionRange, MaxVisualExpansionRange)]
    [SerializeField]
    private float visualExpansionPerFrame;
	[Range(MinColliderExpansionRange, MaxColliderExpansionRange)]
    [SerializeField]
    private float colliderExpansionPerFrame;

    private SphereCollider sphereCollider;
    private Vector3 expansionVector;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        
		Assert.IsNotNull(sphereCollider);
		Assert.IsTrue(maxExplosionRadius > 0);
        Assert.IsTrue(visualExpansionPerFrame > 0);
        Assert.IsTrue(colliderExpansionPerFrame > 0);

        expansionVector = new Vector3
		(
			visualExpansionPerFrame, 
			visualExpansionPerFrame, 
			visualExpansionPerFrame
		);
    }

    void Update()
    {
        if (sphereCollider.radius < maxExplosionRadius)
        {
            transform.localScale += expansionVector;
			// Collider increases with scale, but not enough to match visuals, 
			// so we add an extra expansion per frame for the collider only.
            sphereCollider.radius += colliderExpansionPerFrame;
        }
    }
}
