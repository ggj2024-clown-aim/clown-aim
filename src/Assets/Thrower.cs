using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    [Header("References")]
    public GameObject projectile;
    public Transform throwStartPoint;
    public Camera cam;

    [Header("Throwing")]
    public float throwForce;
    public float forwardThrowForce = 10f;

    GameObject lastInstance;
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 throwDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DragThrowPie();
       
    }

    /// <summary>
    /// Spawns and throws a new cake with a horizontal and vertical force.
    /// </summary>
    private void Throw(Quaternion aimDirection)
    {
        if (lastInstance)
        {
            Destroy(lastInstance);
        }
        GameObject cake = Instantiate(projectile, throwStartPoint.position, transform.rotation);
        
        Vector3 force = aimDirection * throwStartPoint.forward * throwForce;
        Rigidbody cakeRb = cake.GetComponent<Rigidbody>();
        cakeRb.AddForce(force, ForceMode.Impulse);
        //lastInstance = cake;
    }

    void DragThrowPie()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        if (Input.GetMouseButtonDown(0))
        {

            startPoint = cam.ScreenToWorldPoint(mousePosition);
            startPoint.z = 15;
            Debug.Log("start: " + startPoint);


        }
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = cam.ScreenToWorldPoint(mousePosition);
            endPoint.z = 15;
            Debug.Log("end: "+ endPoint);
            throwDirection = startPoint- endPoint;
            throwDirection.z = forwardThrowForce;
            throwDirection = throwDirection.normalized;
            Debug.Log(throwDirection);
            Quaternion aimDirection = Quaternion.FromToRotation(transform.forward, throwDirection);
            Throw(aimDirection);
        }

    }
}
