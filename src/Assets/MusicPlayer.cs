using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Target;
using static UnityEngine.GraphicsBuffer;




public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pie"))
        {
            collision.gameObject.tag = "InactivePie";
            if (audioSource.isPlaying)
            {

                audioSource.Pause();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}
