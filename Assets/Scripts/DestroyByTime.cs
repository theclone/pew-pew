using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DestroyByTime : MonoBehaviour
{
    public const float MinLifetime = 0f;
    public const float MaxLifetime = 10f;

    [Range(MinLifetime, MaxLifetime)]
    [SerializeField]
    private float lifetime;

    void Start()
    {
        Assert.IsTrue(lifetime > 0);
        Destroy(gameObject, lifetime);
    }
}
