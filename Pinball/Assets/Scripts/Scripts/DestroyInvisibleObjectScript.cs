using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvisibleObjectScript : MonoBehaviour {
    public string destroytag = "Sphere";
    public GameObject[] objects;

    private List<ResetBehaviour> behaviours;

    void Start()
    {
        if(objects.Length != 0)
        {
            behaviours = new List<ResetBehaviour>();

            foreach(GameObject gameObject in objects)
            {
                behaviours.Add(gameObject.GetComponent<ResetBehaviour>());
            }
        }
    }


    void OnTriggerEnter(Collider other) {
        if(other.tag == destroytag) {
            Destroy(other.gameObject);

            if(behaviours.Count != 0)
            {
                foreach(ResetBehaviour behaviour in behaviours)
                {
                    behaviour.Reset();
                }
            }
        }        
    }
}
