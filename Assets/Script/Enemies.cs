using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
    public float health;
    //public float damage;
    private float horizontal;
    private Rigidbody2D rb;
    public static Enemies instance;
    public Animator anime;
    private IEnumerator coroutine;

    private void Awake()
    {
            instance= this;
        anime.GetComponent<Animator>();
    }



    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            anime.SetTrigger("EnemiesDie");
        }
        coroutine = Dead();
        StartCoroutine(coroutine);
    }

    private void FixedUpdate()
    {

        
    }

    
    public void EnemiesTakeDamage(float PlayerDamage)
    {
        health-= PlayerDamage;

    }

    private IEnumerator Dead()
    {
        if (health <= 0)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

    }


}
