using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler {

    AudioSource audioSource;
    AudioClip soundCursorHover;
    AudioClip soundCursorSelect;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundCursorHover = (AudioClip)Resources.Load("Sounds/menuHighlightButton"); 
        soundCursorSelect = (AudioClip)Resources.Load("Sounds/menu_selectingButton");
    }

    public void StartGame() //Load first level
    {
        audioSource.PlayOneShot(soundCursorSelect);
        SceneManager.LoadScene("Scenes/level_1", LoadSceneMode.Single);
    }

    public void QuitGame() //Close down application
    {
        audioSource.PlayOneShot(soundCursorSelect);
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(soundCursorHover);
    }
}
