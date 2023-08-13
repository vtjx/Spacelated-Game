using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject GameoverMenu;
    [SerializeField]
    private GameObject[] objToHide;
    [SerializeField]
    private GameObject[] enemyToDestroy;
    [SerializeField]
    private GameObject slider;
    private float sliderValue;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private GameObject optionsMenu;
    private Player player;
    [SerializeField]
    private GameObject[] lvl1Enemies;
    [SerializeField]
    private GameObject[] lvl2Enemies;
    [SerializeField]
    private GameObject[] lvl3Enemies;
    private float lvl1spwnRate;
    [HideInInspector]
    public int score;
    private int highscore;
    public TMP_Text scoreTxt;
    public TMP_Text highscoreTxt;
    public TMP_Text postScoreTxt;
    private int powerLvl;
    private int coordX;
    private int coordY;
    private bool gameOver;
    private bool startedlvl2;
    private bool startedlvl3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine("spawnLvl1Enemies");
        audioMixer.GetFloat("volume", out sliderValue);
        slider.GetComponent<Slider>().value = sliderValue;
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume(slider.GetComponent<Slider>().value);
        PauseMenu();
        GameOver();
        Lvl1SpwnRate();
        toString();
        powerLvl = player.powerLvl;
        if (powerLvl >= 4 && !startedlvl2 && !gameOver) 
        {
            StartCoroutine("spawnLvl2Enemies");
            startedlvl2 = true;
        }
        if (powerLvl >= 6 && !startedlvl3 && !gameOver)
        {
            StartCoroutine("spawnLvl3Enemies");
            startedlvl3 = true;
        }
    }

    private IEnumerator spawnLvl1Enemies()
    {
        while (!gameOver)
        {
            var picker = Random.Range(0, lvl1Enemies.Length);
            var randomCoord = Random.Range(0, 4);
            if (randomCoord == 0)
            {
                coordX = Random.Range(-10, 10);
                coordY = 10;
            }
            else if (randomCoord == 1)
            {
                coordX = Random.Range(-10, 10);
                coordY = -10;
            }
            else if (randomCoord == 2)
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            else
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            Instantiate(lvl1Enemies[picker], new Vector2(coordX, coordY), Quaternion.identity);
            yield return new WaitForSeconds(lvl1spwnRate);
        }
    }

    private IEnumerator spawnLvl2Enemies()
    {
        while (powerLvl >= 4)
        {
            var picker = Random.Range(0, lvl2Enemies.Length);
            var randomCoord = Random.Range(0, 4);
            if (randomCoord == 0)
            {
                coordX = Random.Range(-10, 10);
                coordY = 10;
            }
            else if (randomCoord == 1)
            {
                coordX = Random.Range(-10, 10);
                coordY = -10;
            }
            else if (randomCoord == 2)
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            else
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            Instantiate(lvl2Enemies[picker], new Vector2(coordX, coordY), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator spawnLvl3Enemies()
    {
        while (powerLvl >= 6)
        {
            var picker = Random.Range(0, lvl3Enemies.Length);
            var randomCoord = Random.Range(0, 4);
            if (randomCoord == 0)
            {
                coordX = Random.Range(-10, 10);
                coordY = 10;
            }
            else if (randomCoord == 1)
            {
                coordX = Random.Range(-10, 10);
                coordY = -10;
            }
            else if (randomCoord == 2)
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            else
            {
                coordY = Random.Range(-10, 10);
                coordX = 10;
            }
            Instantiate(lvl3Enemies[picker], new Vector2(coordX, coordY), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    private void toString()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        scoreTxt.text = score.ToString();
        postScoreTxt.text = "SCORE\n" + score.ToString();
        highscoreTxt.text = "HIGHSCORE\n" + highscore.ToString();
    }

    private void Lvl1SpwnRate()
    {
        if (powerLvl >= 3)
        {
            lvl1spwnRate = 0.3f;
        }
        else
        {
            lvl1spwnRate = 1;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                optionsMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        if (player.health == 0)
        {
            gameOver = true;
            foreach (var obj in objToHide)
            {
                obj.SetActive(false);
            }
            enemyToDestroy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemyToDestroy)
            {
                Destroy(enemy);
            }
            GameoverMenu.SetActive(true);
        }
    }
}
