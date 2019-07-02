using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherScript: MonoBehaviour
{

    private float power;
    public float launchThreshold = 10f;
    public float maxPower = 1000f;
    public Slider powerSlider;
    GameObject sphere;
    bool ballReady;
    private SignalHandlerScript signalHandler;
    // Start is called before the first frame update
    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballReady)
        {

            powerSlider.gameObject.SetActive(true);

        }
        else {

            powerSlider.gameObject.SetActive(false);

        }


        powerSlider.value = power;
        if (sphere != null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (power <= maxPower)
                {
                    power += 250 * Time.deltaTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                Debug.Log($"Power: {power}");
                rb.AddForce(power * Vector3.forward);
            }

            Debug.Log($"Signal handler force {signalHandler.launcher.force}");
            
            if (signalHandler.launcher.force > 0 || Input.GetKeyUp(KeyCode.Space))
            {
                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                float force = signalHandler.launcher.force * launchThreshold;
                Debug.Log($"Força de Lançamento {force}");
                rb.AddForce(49 * Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(CollisionHelper.DidCollideWithSphere(other.tag))
        {
            sphere = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(CollisionHelper.DidCollideWith2DSpere(other.tag))
        {
            sphere = null;
        }
    }
}
    
