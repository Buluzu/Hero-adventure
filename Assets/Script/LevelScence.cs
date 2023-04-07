using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScence : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField] private player playerValue;
    public Image a;
    public Image b;
    public static LevelScence instance;

    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        a.gameObject.SetActive(true);

    }    

    public void Continue()
    {
        Time.timeScale = 1;
        a.gameObject.SetActive(false);
    }


    public void buffMP()
    {
        Time.timeScale = 1;
        player.instance.MaxMP+=200;
        player.instance.MP += 200;
        b.gameObject.SetActive(false);
    }

    public void buffDamage()
    {
        Time.timeScale = 1;
        player.instance.damage += 100;
        player.instance.SlashDamage += 300;
        b.gameObject.SetActive(false);

    }

    public void buffHP()
    {
        Time.timeScale = 1;
        player.instance.MaxHP += 750;
        player.instance.HP += 750;
        b.gameObject.SetActive(false);

    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        coroutine = Dead();
        StartCoroutine(coroutine);     
        
    }

    private IEnumerator Dead()
    {
        if (player.instance.HP == 0)
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(4);

        }
        
    }


}
