using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    

    public enum HitType
    {
        Wheel,
        Head,
        Torso,
        LeftFoot,
        RightFoot,
        LeftHand,
        RightHand
    }

    [Header("Settings")]
    public float zRotationSpeed = 10f;
    public int zRotationSpeedStep = 1;
    public float extraZRotationPercentage = 0.25f;
    public HitType expectedHitType = HitType.Head;
    public TMPro.TextMeshProUGUI scoreText;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float zSpeed = zRotationSpeed * (1 + extraZRotationPercentage * score / zRotationSpeedStep);
        transform.Rotate(new Vector3(0, 0, zSpeed) * Time.deltaTime);
    }

    public void OnHit(HitType hitType)
    {
        if (hitType == HitType.Wheel)
        {
            score -= 1;
        }
        if (hitType == expectedHitType)
        {
            score += 1;
            SelectNewTarget();
        }
        scoreText.text = score.ToString();
    }

    public HitType CurrentTarget()
    {
        return expectedHitType;
    }

    void SelectNewTarget()
    {
        HitType oldType = expectedHitType;
        int newType = UnityEngine.Random.Range(1, 7);
        if (newType == (int) oldType)
        {
            newType = Mathf.Clamp((newType + 1) % 6, 1, 6);
        }
        expectedHitType = (HitType)newType;
    }

    
}
