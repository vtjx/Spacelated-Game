using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    [SerializeField]
    private int speed;
    [SerializeField]
    private float timetoDeath;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Launch Projectile
        transform.position += transform.up * speed * Time.deltaTime;

        //Destroy after certain time
        timetoDeath -= Time.deltaTime;
        if (timetoDeath <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
