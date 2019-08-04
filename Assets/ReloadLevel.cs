using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     

     
     
    }
     public void RestartGame() {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
         }
    // Update is called once per frame
    void Update()
    {
        
    }
}
