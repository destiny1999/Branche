using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Camera = null;
    public float speed = 1;
    public float top = 14;
    public float down = 7;
    public float left = 10;
    public float right = 10;

    

    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if(Camera.transform.position.y < top)
            {
                Camera.transform.Translate(0, speed*Time.deltaTime, 0);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Camera.transform.position.y > down)
            {
                Camera.transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Camera.transform.position.x > left)
            {
                Camera.transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Camera.transform.position.x < right)
            {
                Camera.transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
        
        

    }

}
