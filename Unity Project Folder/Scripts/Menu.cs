using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject slider;
    private float sliderValue;
    [SerializeField]
    private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.GetFloat("volume", out sliderValue);
        slider.GetComponent<Slider>().value = sliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume(slider.GetComponent<Slider>().value);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void OptionsBtn()
    {
        optionsMenu.SetActive(true);
    }

    public void closeOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
