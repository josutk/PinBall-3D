using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondScreenScript : MonoBehaviour
{
    GameObject secondScreen;

    void Start()
    {
        secondScreen = GameObject.FindGameObjectWithTag(Constants.SECOND_SCREEN_TAG);

        DeactivateChildren(); 
    }

    public void ReactivateChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if(child != null) child.SetActive(true);
        }        
    }

    private void DeactivateChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if(child != null) child.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
