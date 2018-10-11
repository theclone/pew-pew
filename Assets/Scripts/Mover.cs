using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    public const float MinSpeed = 0f;
    public const float MaxSpeed = 10f;

    [Range(MinSpeed, MaxSpeed)]
    [SerializeField]
    private float speed;

    private new Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Assert.IsNotNull(rigidbody);
        Assert.IsTrue(speed > 0);

        rigidbody.velocity = -transform.forward * speed;
    }
}
