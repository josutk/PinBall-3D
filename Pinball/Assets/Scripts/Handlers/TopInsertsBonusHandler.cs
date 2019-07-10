using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopInsertsBonusHandler : MonoBehaviour
{
    public GameObject[] inserts;

    private ScoreManager scoreManager;

    public bool AreAllInsertsOn 
    { 
        get
        {
            foreach(GameObject obj in inserts)
            {
                bool isLightOn = obj.GetComponent<Light>().enabled;

                if(!isLightOn)
                {
                    return false;
                }
            }

            return true;
        }
    }
    void Start()
    {
        scoreManager = Finder.GetScoreManager();        
    }

    void Update()
    {
        if(AreAllInsertsOn)
        {
            scoreManager.AddScore(5000 / scoreManager.multiplier);

            TurnAllInsertsOff();
        }
    }


    void TurnAllInsertsOff()
    {
        inserts.Select(x => 
        {
            x.GetComponent<Light>().enabled = false;    
            x.GetComponent<Renderer>().materials.Select(y => {
                y.DisableKeyword("_EMISSION");
                return y;
            }).Count();
            return x;
        }).Count();
    }
}
