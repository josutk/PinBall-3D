using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    private Light light;
    private Material material;

    void Start()
    {
        light = GetComponent<Light>();

        if(light == null)
        {
            light = GetComponentInParent<Light>();
        }

        Renderer renderer = GetComponent<Renderer>();

        if(renderer == null)
        {
            renderer = GetComponentInParent<Renderer>();
        }

        material = renderer.material;
    }

    void OnCollisionEnter(Collision other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            On();
        }
    }

    void On()
    {
        light.enabled = true;
        material.EnableKeyword("_EMISSION");
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(0.1f);
        light.enabled = false;
        material.DisableKeyword("_EMISSION");
    }

    void OnCollisionExit(Collision other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            StartCoroutine("Off");
        }
    }
}
