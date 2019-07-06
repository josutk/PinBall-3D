using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript : MonoBehaviour{
    public float initialPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrenght = 10000f;
    public float flipperDamper = 150f;

    public bool isLeft = false;
    
    private HingeJoint hinge;

    private SignalHandlerScript signalHandler;

    private AudioSource audioSource;

    public AudioClip flipperDown;
    public AudioClip flipperUp;


    private void Start() 
    {
        hinge = GetComponent<HingeJoint>();

        hinge.useSpring = true;

        signalHandler = GameObject
                        .FindGameObjectWithTag(Constants.SIGNAL_HANDLER_TAG)
                        .GetComponent<SignalHandlerScript>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrenght;
        spring.damper = flipperDamper;

        if(isLeft)
        {
            if (signalHandler.buttons.leftButton && !signalHandler.freeze) 
            {
                spring.targetPosition = pressedPosition;

                if(!signalHandler.previousButtons.leftButton)
                {
                    audioSource.clip = flipperUp;
                    audioSource.Play();
                }
            }
            else 
            {
                spring.targetPosition = initialPosition;
              
                if(signalHandler.previousButtons.leftButton)
                {
                    audioSource.clip = flipperDown;
                    audioSource.Play();
                }
            }    
        }
        else
        {
            if (signalHandler.buttons.rightButton && !signalHandler.freeze) 
            {
                spring.targetPosition = pressedPosition;
                
                if(!signalHandler.previousButtons.rightButton)
                {
                    audioSource.clip = flipperUp;
                    audioSource.Play();
                }
            }
            else {
                spring.targetPosition = initialPosition;

                if(signalHandler.previousButtons.rightButton)
                {
                    audioSource.clip = flipperDown;
                    audioSource.Play();
                }
            }
        }
        

        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
