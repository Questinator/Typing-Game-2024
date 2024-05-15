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
            SceneManager.LoadScene(sceneToLoad);
            Persistence.Instance.NextPlayerLocation = spawnLocation;
        }
    }
}

public class Persistence
{
    public static Persistence Instance => instance ??= new Persistence();
    private static Persistence instance;

    public Vector3 NextPlayerLocation { get; set; }

    public Persistence()
    {
        NextPlayerLocation = new Vector3(-1000, -1000, -1000);
    }
    
}