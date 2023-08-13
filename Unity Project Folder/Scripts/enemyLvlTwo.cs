using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyLvlTwo : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private GameMaster gm;
    [SerializeField]
    private GameObject powerUpObj;
    [SerializeField]
    private GameObject healthObj;
    [SerializeField]
    private GameObject shieldObj;
    [SerializeField]
    private Slider slider;
    private Canvas canvas;
    private int health;
    private int powerLvl;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        canvas = transform.GetChild(0).GetComponent<Canvas>();
        health = 16;
        slider.maxValue = health;
        slider.value = health;
        powerLvl = playerScript.powerLvl;
        if (powerLvl > 4)
        {
            powerLvl = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Movment
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime);

        //Rotation
        var dir = transform.position - player.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //Death
        if (health <= 0)
        {
            var dropRate = Random.Range(0, 101);
            if (dropRate <= 10)
            {
                if (player.GetComponent<Player>().health < 3)
                {
                    Instantiate(healthObj, transform.position, Quaternion.identity);
                }
                else if (player.GetComponent<Player>().shield < 3 && player.GetComponent<Player>().powerLvl == 6)
                {
                    Instantiate(shieldObj, transform.position, Quaternion.identity);
                }
                else if (player.GetComponent<Player>().powerLvl < 6)
                {
                    Instantiate(powerUpObj, transform.position, Quaternion.identity);
                }
            }
            gm.score += 30;
            Destroy(gameObject);
        }

        //HealthBar
        canvas.transform.rotation = Quaternion.identity;
        slider.transform.localPosition = new Vector3(0, -40, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 2;
        slider.value -= 2;
    }
}
