using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCylindre : MonoBehaviour
{
    [SerializeField] 
    private int radius, hauteur, nbMeridiens;
    
    void Start()
    {
        float theta = 360 / nbMeridiens;
        float topY = hauteur / 2;
        float bottomY = -hauteur / 2;

        for (float i = 0; i < 360; i += theta)
        {
            
        }
    }

}
