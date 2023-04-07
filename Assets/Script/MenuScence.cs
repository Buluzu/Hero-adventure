using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static player;
public class MenuScence : MonoBehaviour
{
    private player health;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Setting()
    {

    }    
    public void Exit()
    {
        Application.Quit();
    }
   /* private void Update()
    {
        if(player.instance.HP==0)
        {
            SceneManager.LoadScene(2);
        }
    }*/
   
}
