using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CakeCollider : MonoBehaviour
{

    public Target target;
    public Target.HitType hitType;

    public void Update()
    {
        
        if (target.CurrentTarget() == hitType)
        {
            // set color to red
            GetComponent<MeshRenderer>().material.color = Color.red;
        } else
        {
            // set color to white
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pie"))
        {
            Debug.Log(hitType);
            target.OnHit(hitType);
            collision.gameObject.tag = "InactivePie";
        }
    }

}
