using System;
using Combat.Controller;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Transform playerPositition;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button runButton;
    [SerializeField] private Button itemButton;

    private CombatLoader loader;
    public void Start()
    {
        loader = CombatLoader.Instance;
        Debug.Log("Star ting");
        Instantiate(loader.Info.PlayerObj, playerPositition);
        Instantiate(loader.Info.EnemyObj, enemyPosition);
        attackButton.onClick.AddListener((() =>
        {
            CombatLoader.Instance.Complete(new CombatResult(true,true,1000,0));
        }));
    }
}
