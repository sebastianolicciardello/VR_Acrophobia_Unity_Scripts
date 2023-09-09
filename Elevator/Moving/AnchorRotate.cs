using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorRotate : MonoBehaviour
{
    // Reference to the anchor
    [SerializeField] private GameObject anchor;

    // Anchor transform for floor Zero
    public void TransformZero()
    {
        Vector3 newPosition = new Vector3(0.4391427f, 0.8317532f, -0.357666f);
        Quaternion newRotation = Quaternion.Euler(331.5624f, 270f, 270f);
        
        anchor.transform.localPosition = newPosition;
        anchor.transform.localRotation = newRotation;
    }

    // Anchor transform for floor One
    public void TransformOne()
    {
        Vector3 newPosition = new Vector3(0.88f, -0.513353f, -0.3576661f);
        Quaternion newRotation = Quaternion.Euler(299.1841f, 90.00005f, 89.99995f);
        
        anchor.transform.localPosition = newPosition;
        anchor.transform.localRotation = newRotation;
    }
}
