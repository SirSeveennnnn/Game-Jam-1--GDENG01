using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = cameraPosition.position;
    }
}
