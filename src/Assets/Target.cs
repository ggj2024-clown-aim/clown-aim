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
    public HitType expectedHitType = HitType.Head;
    public TMPro.TextMeshProUGUI scoreText;
    public int scoreMultiplierStep = 3;
    public Vector3 defaultPosition = new Vector3(0, 5, 10.8000002f);
    public Quaternion defaultRotation = Quaternion.identity;
    public GameObject particlesHit;
    public AudioClip gaspAudio;
    public AudioClip laughAudio;
    public AudioClip booAudio;
    public Heart heart;

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


    [Header("Player")]
    public int startLives = 5;
    public int restoreLifeComboLength = 5;

    int lives;
    public bool isGameOver = false;
    public int score = 0;
    int hitCounter = 0;
    AudioSource audioSource;

    

    void Start()
    {
        RestartGame();
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartGame();
        }
    }

    void PlayLaugh()
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(laughAudio);
    }

    void PlayGasp() {
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(gaspAudio);
    }

    void PlayBoo() {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(booAudio);
    }

    void ShowParticles(ContactPoint contactPoint) {
        ParticleSystem ps = particlesHit.GetComponent<ParticleSystem>();
        ps.Clear();
        particlesHit.transform.position = contactPoint.point;
        ps.Play();
        AudioSource audio = particlesHit.GetComponent<AudioSource>();
        audio.Play();        
    }

    public void OnHit(HitType hitType, ContactPoint contactPoint)
    {
        ShowParticles(contactPoint);
        if (hitType == expectedHitType)
        {
            PlayLaugh();
            hitCounter += 1;
            Debug.Log(hitCounter);
            score += (int) Mathf.Pow(2,(int)(hitCounter / scoreMultiplierStep));
            SelectNewTarget();
            RestoreHealth();
        } else
        {
            PlayGasp();
            lives -= 1;
            hitCounter = 0;
        }
        if (hitType == HitType.Wheel)
        {
            score -= 1;

        }
        scoreText.text = "SCORE: " + score.ToString();
        scoreText.text += "\nLIVES: " + lives;
        scoreText.text += "\nMULTIPLIER: " + Mathf.Pow(2, hitCounter / scoreMultiplierStep);

        heart.SetHearts(hitCounter % restoreLifeComboLength);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver() {
        PlayBoo();
        scoreText.text = "GAME OVER\nSCORE: " + score.ToString() + "\n Press R to restart.";
        isGameOver = true;
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

    void RestoreHealth()
    {
        if (hitCounter % restoreLifeComboLength == 0)
        {
            lives += 1;
        }
    }

    void RestartGame()
    {
        scoreText.text = "CLOWN AIM\n\nHold MB1 and drag to aim;\nrelease to shoot.\nHit the *RED* body part!";
        score = 0;
        lives = startLives;
        isGameOver = false;
        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
    }
}
