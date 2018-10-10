using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public const string BoundaryTag = "Boundary";
    public const string PlayerTag = "Player";
    public const string BombTag = "Bomb";

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject playerExplosion;
    [SerializeField]
    private int scoreValue;

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

        if (!(other.tag.Equals(BombTag)))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
