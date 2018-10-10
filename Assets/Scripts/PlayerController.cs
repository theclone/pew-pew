using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : Singleton<PlayerController>
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
    public const string FireButton = "Fire";
    public const string BombButton = "Bomb";
    public const float BombYOffset = -1;

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
    [SerializeField]
    private int numBombs;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private float nextFire;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (numBombs > 0 && Input.GetButton(BombButton) && Time.time > nextFire)
        {
            nextFire = Time.time + bombCooldown;
            numBombs -= 1;
            Instantiate(bomb, transform.position + new Vector3(0, BombYOffset, 0), transform.rotation);
            GameController.Instance.UpdateBombIcons();
            audioSource.PlayOneShot(bombSFX);
        }
        else if (Input.GetButton(FireButton) && Time.time > nextFire)
        {
            nextFire = Time.time + fireDelay;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.PlayOneShot(shotSFX);
        }
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis(Horizontal);
        float moveVertical = Input.GetAxis(Vertical);

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

    public void SetNumBombs(int newBombs)
    {
        numBombs = newBombs;
        GameController.Instance.UpdateBombIcons();
    }

    public int BombCount
    {
        get
        {
            return numBombs;
        }
    }
}
