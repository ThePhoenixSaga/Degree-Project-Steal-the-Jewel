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
        //Set Cursor to not be visible
        Cursor.visible = true;
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

    public void ReturnToMenu() //Returns to Main Menu
    {
        audioSource.PlayOneShot(soundCursorSelect);
        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(soundCursorHover);
    }

    //

    CursorLockMode wantedMode;

    // Apply requested cursor state
    void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        // Release cursor on escape keypress
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = wantedMode = CursorLockMode.None;

        GUILayout.EndVertical();

        SetCursorState();
    }
}
