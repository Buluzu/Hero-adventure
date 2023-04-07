using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
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
            a.star += 1;
            Destroy(gameObject);
        }
    }
}
