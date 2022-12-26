using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubles : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(gameObject); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); 
    }
}
