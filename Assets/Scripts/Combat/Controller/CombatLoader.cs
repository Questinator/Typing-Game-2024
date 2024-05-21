using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Combat.Controller
{
    public class CombatLoader
    {
        private static string combatScene = "Combat";
        
        
        public static CombatLoader Instance => instance ??= new CombatLoader();

        private CombatLoader()
        {
            
        }
        
        public CombatInfo Info => info;

        public CombatResult Result => result;

        private static CombatLoader instance;

        private CombatResult result;
        private CombatInfo info;
        
        private GameObject[] unloaded;
        private Scene battleScene;
        private OnBattleComplete onNextComplete;
        public CombatInfo GetInfo()
        {
            if (info == null)
            {
                throw new Exception("No data in Combat Info, are you doing something wrong? ");
            }
            return info;
        }
        public CombatInfo GetResult()
        {
            if (info == null)
            {
                throw new Exception("No data in Combat Info, are you doing something wrong? ");
            }
            return info;
        }

        /// <summary>
        /// Starts a battle
        /// Hides everything in the current scene, and loads a combat scene
        /// Once the combat scene completes it destroys the combat scene and reenables all objects and calls onComplete
        /// </summary>
        /// <param name="info">Data for the battle</param>
        /// <param name="onComplete">What to do when the battle finishes</param>
        public void Load(CombatInfo info, OnBattleComplete onComplete)
        {
            // Unload all objects
            unloaded = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject toUnload in unloaded)
            {
                toUnload.SetActive(false);
            }
            SceneManager.LoadScene(combatScene, LoadSceneMode.Additive);
            battleScene = SceneManager.GetSceneByName(combatScene);
            this.info = info;
            onNextComplete = onComplete;
        }
        /// <summary>
        /// Finishes a battle
        /// </summary>
        /// <param name="result">Result of the battle</param>
        public void Complete(CombatResult result)
        {
            AsyncOperation res = SceneManager.UnloadSceneAsync(battleScene);

            if (res != null)
                res.completed += _ =>
                {
                    this.result = result;

                    foreach (GameObject toLoad in unloaded)
                    {
                        toLoad.SetActive(true); // Potentially could cause issues will fix if it comes up
                    }

                    onNextComplete(result);
                };
        }
        
    }
    /// <summary>
    /// Used to subscribe callbacks
    /// </summary>
    public delegate void OnBattleComplete(CombatResult result);
    /// <summary>
    /// Result of combat
    /// </summary>
    public class CombatResult
    {
        private bool playerWon;
        private bool playerRan;
        private int playerHp;
        private int enemyHp;

        public CombatResult(bool playerWon, bool playerRan, int playerHp, int enemyHp)
        {
            this.playerWon = playerWon;
            this.playerRan = playerRan;
            this.playerHp = playerHp;
            this.enemyHp = enemyHp;
        }
        public bool PlayerWon => playerWon;
        public bool PlayerRan => playerRan;
        public int PlayerHp => playerHp;
        public int EnemyHp => enemyHp;
    }
    /// <summary>
    /// Data for starting combat
    /// </summary>
    public class CombatInfo
    {
        private GameObject playerObj;
        private GameObject enemyObj;
        private CombatEntity player;
        private CombatEntity enemy;

        public CombatInfo(GameObject playerObj, GameObject enemyObj, CombatEntity player, CombatEntity enemy)
        {
            this.playerObj = playerObj;
            this.enemyObj = enemyObj;
            this.player = player.Clone();
            this.player.name = player.name;
            this.enemy = enemy.Clone();
            this.enemy.name = enemy.name;
        }

        public GameObject PlayerObj => playerObj;

        public GameObject EnemyObj => enemyObj;

        public CombatEntity Player => player;

        public CombatEntity Enemy => enemy;
    }
}