using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Conditions;

public class LightBehaviour : MonoBehaviour
{

    public string turnOnMethodName;
    public string turnOffMethodName;
    private Condition turnOnCondition;
    private Condition turnOffCondition;

    [HideInInspector]
    public bool onCollision;
    [HideInInspector]
    public bool onTrigger;
    [HideInInspector]
    public bool onCustom;
    [HideInInspector]
    public bool turnOffOnExit;
    [HideInInspector]
    public bool lightSelf;

    public GameObject[] objectsToLight;

    public GameScript game;

    void Start()
    {
        game = Finder.GetGameController();

        GetMethods();
    }

    private void GetMethods()
    {
        if(turnOnMethodName != "")
        {
            turnOnCondition 
                = (Condition) Delegate
                    .CreateDelegate(
                        typeof(Conditions.Condition),
                        typeof(Conditions.LightOnConditions),
                        turnOnMethodName
                    );
        }
        else if(turnOffMethodName != "")
        {
            turnOffCondition = 
                (Condition) Delegate.CreateDelegate(typeof(Conditions.LightOffConditions), this, turnOffMethodName);
        }
    }

    private Light GetLight(GameObject obj)
    {
        Light light = obj.GetComponent<Light>();

        if(light == null)
        {
            light = obj.GetComponentInParent<Light>();
        }

        return light;
    }

    private Material[] GetMaterials(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if(renderer == null)
        {
            renderer = obj.GetComponentInParent<Renderer>();
        }

        return renderer.materials;
    }

    void Update()
    {
        if(onCustom && !onCollision && !onTrigger)
        {
            if(turnOnCondition())
            {
                On();
            }
        }
    }

    void On()
    {
        foreach(GameObject obj in objectsToLight)
        {
            Light light = GetLight(obj);
            Material[] materials = GetMaterials(obj);

            light.enabled = true;
            materials = materials.Select(x => 
                { 
                    x.EnableKeyword("_EMISSION"); 
                    return x; 
                }
            ).ToArray();
        }
    }

    IEnumerator Off()
    {
        foreach(GameObject obj in objectsToLight)
        {
            Light light = GetLight(obj);
            Material[] materials = GetMaterials(obj);

            yield return new WaitForSeconds(0.1f);
            light.enabled = false;
            materials = materials.Select(x => 
                { 
                    x.DisableKeyword("_EMISSION"); 
                    return x; 
                }
            ).ToArray();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(onCollision && CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            On();
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if(onCollision && turnOffOnExit && CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            StartCoroutine("Off");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(onTrigger && CollisionHelper.DidCollideWithSphere(other.tag))
        {
            On();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(onTrigger && turnOffOnExit && CollisionHelper.DidCollideWithSphere(other.tag))
        {
            StartCoroutine("Off");
        }
    }    
}
