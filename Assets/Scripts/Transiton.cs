using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiton : MonoBehaviour
{
    [SerializeField] private String sceneToLoad;
    [SerializeField] private Vector3 spawnLocation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Persistence.Instance.NextPlayerLocation = spawnLocation;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

public class Persistence
{
    public static Persistence Instance => instance ??= new Persistence();
    private static Persistence instance;

    public Vector3 NextPlayerLocation { get; set; } = UseSceneDefault;
    public static readonly Vector3 UseSceneDefault = new Vector3(-1000, -1000, -1000);
}