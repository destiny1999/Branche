using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("RELOAD");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
    
}
