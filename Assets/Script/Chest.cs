using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator anime;
    //public BoxCollider2D b2d;
    // Start is called before the first frame update
    void Start()
    {
        anime.GetComponent<Animator>();
        //b2d.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player a = collision.GetComponent<player>();
        if (a != null)
        {
            anime.SetTrigger("Open");
           
            LevelScence.instance.b.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            //b2d.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
