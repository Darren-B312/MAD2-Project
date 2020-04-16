using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // https://www.youtube.com/watch?v=N24MhfeoUpE
    public Animator cameraAnimator;

    public void Shake()
    {
        cameraAnimator.SetTrigger("Shake");
    }
}
