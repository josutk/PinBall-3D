using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherScript: MonoBehaviour
{

    float power;
    public float maxPower = 1000f;
    public Slider powerSlider;
    List<Rigidbody> ballList;
    bool ballReady;
    // Start is called before the first frame update
    void Start()
    {

        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
        ballList = new List<Rigidbody>();

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
        if (ballList.Count > 0)
        {
            ballReady = true;

            if (Input.GetKey(KeyCode.Space))
            {
                if (power <= maxPower)
                {
                    power += 250 * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                foreach (Rigidbody r in ballList)
                {
                    r.AddForce(power * Vector3.forward);
                }
            }
        }
        else {

            ballReady = false;
            power = 0f;

        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Sphere")) {
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());

            }
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sphere"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
            power = 0f;

        }
    }
}
    
