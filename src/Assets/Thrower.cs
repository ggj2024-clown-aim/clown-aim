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
    public Transform aimLine;
    public Target target;

    [Header("Throwing")]
    public float throwForce;
    public float forwardThrowForce = 10f;

    GameObject lastInstance;
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 throwDirection;
    Quaternion aimDirection;
    bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DragThrowPie();
       if (target.isGameOver)
        {
            Destroy(gameObject);
        }
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
            isAiming = true;
        }
        if (Input.GetMouseButton(0))
        {
            endPoint = cam.ScreenToWorldPoint(mousePosition);
            endPoint.z = 15;
            throwDirection = startPoint - endPoint;
            throwDirection.z = forwardThrowForce;
            throwDirection = throwDirection.normalized;
            aimDirection = Quaternion.FromToRotation(transform.forward, throwDirection);
            DrawAimAssist();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Throw(aimDirection);
            isAiming = false;
            aimDirection = Quaternion.identity;
            HideAimAssist();
        }

    }

    void DrawAimAssist()
    {
        aimLine.SetPositionAndRotation(throwStartPoint.position, aimDirection);
    }

    void HideAimAssist()
    {
        aimLine.position = new Vector3(1000, 1000, 1000);
    }
}
