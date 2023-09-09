using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShadows : MonoBehaviour
{
    void Start()
    {
        // Call the function to disable shadows and lighting for all children of the current object
        DisableShadowsAndLightingForChildren(transform);
    }

    void DisableShadowsAndLightingForChildren(Transform parent)
    {
        // Loop through all children of the given transform
        foreach (Transform child in parent)
        {
            // Get the renderer component of the child object, if it has one
            Renderer renderer = child.GetComponent<Renderer>();

            // If the child has a renderer component and it is enabled, disable shadows, probes, and illumination contributions
            if (renderer != null && renderer.enabled)
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
                renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
                renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            }

            // Recursively call this function to disable shadows and lighting for all children of this child object
            DisableShadowsAndLightingForChildren(child);
        }
    }
}