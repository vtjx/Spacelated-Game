using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthBars;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health >= 4)
        {
            player.health = 3;
        }
        switch (player.health)
        {
            case 1:
                healthBars[0].SetActive(true);
                healthBars[1].SetActive(false);
                healthBars[2].SetActive(false);
                break;
            case 2:
                healthBars[0].SetActive(true);
                healthBars[0].SetActive(true);
                healthBars[0].SetActive(false);
                break;
            case 3:
                healthBars[0].SetActive(true);
                healthBars[0].SetActive(true);
                healthBars[0].SetActive(true);
                break;
            default:
                foreach (var health in healthBars)
                {
                    health.SetActive(false);
                }
                break;
        }
    }
}
