using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField, Tooltip("Set the camera that the sprite has to look at")]
    private Camera PlayerCam;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(PlayerCam.transform);
    }
}
