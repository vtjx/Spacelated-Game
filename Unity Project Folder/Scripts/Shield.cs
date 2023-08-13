using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private GameObject[] shieldBars;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.shield >= 4)
        {
            player.shield = 3;
        }
        switch (player.shield)
        {
            case 1:
                shieldBars[0].SetActive(true);
                shieldBars[1].SetActive(false);
                shieldBars[2].SetActive(false);
                break;
            case 2:
                shieldBars[0].SetActive(true);
                shieldBars[1].SetActive(true);
                shieldBars[2].SetActive(false);
                break;
            case 3:
                shieldBars[0].SetActive(true);
                shieldBars[1].SetActive(true);
                shieldBars[2].SetActive(true);
                break;
            default:
                foreach (var shield in shieldBars)
                {
                    shield.SetActive(false);
                }
                break;
        }
    }
}
