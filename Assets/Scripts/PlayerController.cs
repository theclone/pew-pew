using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
	public const string FIRE = "Fire1";

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

    private new Rigidbody rigidbody;
	private float nextFire;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
		if (Input.GetButton(FIRE) && Time.time > nextFire) {
			nextFire = Time.time + fireDelay;
			GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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
