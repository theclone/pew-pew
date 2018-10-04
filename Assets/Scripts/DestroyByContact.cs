using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject playerExplosion;
    [SerializeField]
    private int scoreValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			GameController.instance.GameOver();
        }
        else
            GameController.instance.AddScore(scoreValue);
        
        if (other.tag != "Bomb")
            Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
