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

    [Header("Level 0: Z Rotation")]
    public float zRotationSpeed = 20f;
    public int zRotationSpeedStep = 1;
    public float extraZRotationPercentage = 0.15f;

    [Header("Level 1: Y Rotation")]
    public int yRotationStart = 10;
    public int yRotationSpeedStep = 1;
    public float yRotationSpeed = 15f;
    public float extraYRotationPercentage = 0.15f;

    [Header("Level 2: X Rotation")]
    public int xRotationStart = 20;
    public int xRotationSpeedStep = 1;
    public float xRotationSpeed = 15f;
    public float extraXRotationPercentage = 0.15f;

    [Header("Settings")]
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
        float ySpeed = 0f;
        float xSpeed = 0f;
        if (score >= yRotationStart)
        {
            ySpeed = yRotationSpeed * (extraYRotationPercentage * (score - yRotationStart) / yRotationSpeedStep);
        }
        if (score >= xRotationStart)
        {
            xSpeed = xRotationSpeed * (extraXRotationPercentage * (score - xRotationStart) / xRotationSpeedStep);
        }
        transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
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
