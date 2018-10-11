using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DestroyByContact : MonoBehaviour
{
    public const string BoundaryTag = "Boundary";
    public const string PlayerTag = "Player";
    public const string BombTag = "Bomb";
    public const int MinScoreValue = 0;
    public const int MaxScoreValue = 250;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject playerExplosion;
    [Range(MinScoreValue, MaxScoreValue)]
    [SerializeField]
    private int scoreValue;

    void Start()
    {
        Assert.IsNotNull(explosion);
        Assert.IsNotNull(playerExplosion);
        Assert.IsTrue(scoreValue > 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(BoundaryTag))
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag.Equals(PlayerTag))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.Instance.GameOver();
        }
        else
            GameController.Instance.AddScore(scoreValue);

        if (!other.tag.Equals(BombTag, StringComparison.Ordinal))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
