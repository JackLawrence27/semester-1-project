using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float lockPosition = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(lockPosition, lockPosition, lockPosition);
    }
}
