using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    [Header("References")]
    public GameObject projectile;
    public Transform throwStartPoint;

    [Header("Throwing")]
    public float throwForce;
    public float pitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Throw();
        }
    }

    /// <summary>
    /// Spawns and throws a new cake with a horizontal and vertical force.
    /// </summary>
    private void Throw()
    {
        GameObject cake = Instantiate(projectile, throwStartPoint.position, transform.rotation);
        Quaternion aimDirection = Quaternion.AngleAxis(pitch, transform.right);
        Vector3 force = aimDirection * throwStartPoint.forward * throwForce;
        Rigidbody cakeRb = cake.GetComponent<Rigidbody>();
        cakeRb.AddForce(force, ForceMode.Impulse);
    }
}
