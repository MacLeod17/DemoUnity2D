using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform targetTransform;

    void Update()
    {
        transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
    }
}
