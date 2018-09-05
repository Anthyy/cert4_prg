using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithinBounds : MonoBehaviour
{
    public CameraBounds camBounds;
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = camBounds.GetAdjustedPosition(transform.position);
    }
}
