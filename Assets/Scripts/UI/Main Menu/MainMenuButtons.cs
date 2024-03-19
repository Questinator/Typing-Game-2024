using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private string gameScene;

    public void OnPlayClick()
    {
        Debug.Log("Loading the game now!");
        SceneManager.LoadScene(gameScene);
    }

    public void OnQuitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }
}
