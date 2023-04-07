using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static player;
public class Potion : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public float mp;
    void Start()
    {
        
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
            a.Healing(hp);
            a.MPRecovery(mp);
            Destroy(gameObject);
        }
    }
}
