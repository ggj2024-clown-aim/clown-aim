using UnityEngine;

public class Audience : MonoBehaviour
{
    public Animator spectatorAnim;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DanceNow();
        }
    }
    void DanceNow()
    {
        spectatorAnim.SetTrigger("Dance");
    }
}
