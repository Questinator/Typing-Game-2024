using System;
using Combat.Controller;
using UnityEngine;

namespace Combat
{
    public class CombatTest : MonoBehaviour
    {
        [SerializeField] private CombatEntity player;
        [SerializeField] private CombatEntity enemy;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;
        
        public void Start()
        {
            Debug.Log("Starting combat");
            CombatLoader.Instance.Load(new CombatInfo(playerPrefab,enemyPrefab,player,enemy), (result) =>
            {
                Debug.Log($"{result.PlayerHp}-{result.EnemyHp},player won {result.PlayerWon}, player ran {result.PlayerRan}");
            });
        }
    }
}