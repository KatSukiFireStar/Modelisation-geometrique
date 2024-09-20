using System;
using Unity.VisualScripting;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    
    private void Update()
    {
        Vector3 vector = new(0, 0, speed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
                
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            
        }
    }
}