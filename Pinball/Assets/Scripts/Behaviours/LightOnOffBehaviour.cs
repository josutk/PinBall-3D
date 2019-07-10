using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightOnOffBehaviour : MonoBehaviour
{
    
    public GameObject objectToLight;

    public bool lightSelf = true;
    public bool lightOnTrigger = false;
    public bool lightOnCollision = true;

    void Start()
    {
        if(lightSelf)
        {
            objectToLight = gameObject;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(lightOnCollision)
        {
            TurnLight(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(lightOnTrigger)
        {
            TurnLight(other.gameObject);
        }
    }

    void TurnLight(GameObject other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.tag))
        {
            if(IsOn)
            {
                Off();
            }
            else
            {
                On();
            }
        }
    }


    void On()
    {
        Light light = GetLight();
        Material[] materials = GetMaterials();
        
        light.enabled = true;
        materials = materials.Select(x => 
            { 
                x.EnableKeyword("_EMISSION"); 
                return x; 
            }
        ).ToArray();
    }

    void Off()
    {
        Light light = GetLight();
        Material[] materials = GetMaterials();
        
        light.enabled = false;
        
        materials = materials.Select(x => 
            { 
                x.DisableKeyword("_EMISSION"); 
                return x; 
            }
        ).ToArray();
    }

    private Light GetLight()
    {
        Light light = objectToLight.GetComponent<Light>();

        if(light == null)
        {
            light = objectToLight.GetComponentInParent<Light>();
        }

        return light;
    }

    private Material[] GetMaterials()
    {
        Renderer renderer = objectToLight.GetComponent<Renderer>();

        if(renderer == null)
        {
            renderer = objectToLight.GetComponentInParent<Renderer>();
        }

        return renderer.materials;
    }

    public bool IsOn { 
        get
        {
            bool lightEnabled = objectToLight.GetComponent<Light>().enabled;

            return lightEnabled;
        }
    }
}
