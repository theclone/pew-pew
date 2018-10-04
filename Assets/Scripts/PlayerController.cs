using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string FIRE_BUTTON = "Fire";
    public const string BOMB_BUTTON = "Bomb";
    public const int DEFAULT_BOMB_NUM = 3;
    public const float BOMB_Y_OFFSET = -1;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float fireDelay;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private AudioClip shotSFX;
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private float bombCooldown;
    [SerializeField]
    private AudioClip bombSFX;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private float nextFire;
    private int numBombs;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        numBombs = DEFAULT_BOMB_NUM;
    }

    void Update()
    {
        if (numBombs > 0 && Input.GetButton(BOMB_BUTTON) && Time.time > nextFire)
        {
            nextFire = Time.time + bombCooldown;
            numBombs -= 1;
            Instantiate(bomb, transform.position + new Vector3(0, BOMB_Y_OFFSET, 0), transform.rotation);
            audioSource.PlayOneShot(bombSFX);
        }
        else if (Input.GetButton(FIRE_BUTTON) && Time.time > nextFire)
        {
            nextFire = Time.time + fireDelay;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.PlayOneShot(shotSFX);
        }

    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis(HORIZONTAL);
        float moveVertical = Input.GetAxis(VERTICAL);

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
}
