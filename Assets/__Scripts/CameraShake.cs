using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // This class is used to create a camera shaking effect 
    // I followed this tutorial to build this - https://www.youtube.com/watch?v=N24MhfeoUpE
    public Animator cameraAnimator;

    public void Shake()
    {
        cameraAnimator.SetTrigger("Shake");
    }
}
