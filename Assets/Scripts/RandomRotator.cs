using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotator : MonoBehaviour
{
    public const float TumbleMin = 0f;
    public const float TumbleMax = 10f;

    [Range(TumbleMin, TumbleMax)]
    [SerializeField]
    private float tumbleRange;
    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Assert.IsNotNull(rigidbody);
        Assert.IsTrue(tumbleRange > 0);

        rigidbody.angularVelocity = Random.insideUnitSphere * tumbleRange;
    }
}
