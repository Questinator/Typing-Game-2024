using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Controller;
using Keyboard;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Transform optionsSection;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button runButton;
    [SerializeField] private Button itemButton;
    [SerializeField] private Button spellButton;
    [SerializeField] private Transform spellArea;
    [SerializeField] private TMP_Text logArea;
    [SerializeField]
    private HealthBarManager playerHealth;
    [SerializeField]
    private HealthBarManager enemyHealth;

    [SerializeField] private Typing typingBox;
    
    private CombatLoader loader;
    private CombatController controller;
    public void Start()
    {
        loader = CombatLoader.Instance;
        controller = new CombatController(new List<CombatEntity> {loader.Info.Player,loader.Info.Enemy});

        spellArea.gameObject.SetActive(false);
        foreach (Spell spell in loader.Info.Player.Spells)
        {
            Button newButton = Instantiate(spellButton, spellArea);
            newButton.gameObject.name = spell.name;
            newButton.transform.GetChild(0).GetComponent<TMP_Text>().SetText(spell.name);
            newButton.onClick.AddListener(() =>
            {
                StartCoroutine(DoAttack(spell, 100, 100));
            });
        }
        Instantiate(loader.Info.PlayerObj, playerPosition);
        Instantiate(loader.Info.EnemyObj, enemyPosition);
        attackButton.onClick.AddListener(() =>
        {
            spellArea.gameObject.SetActive(true);
            optionsSection.gameObject.SetActive(false);
        });
        runButton.onClick.AddListener(() =>
        {
            CombatLoader.Instance.Complete(new CombatResult(false,true,loader.Info.Player.Health,loader.Info.Enemy.Health));
        });
    }

    public void Update()
    {
        playerHealth.MaxHp = loader.Info.Player.MaxHealth;
        playerHealth.Hp = loader.Info.Player.Health;
        enemyHealth.MaxHp = loader.Info.Enemy.MaxHealth;
        enemyHealth.Hp = loader.Info.Enemy.Health;
        playerHealth.UpdateBar();
        enemyHealth.UpdateBar();
    }

    IEnumerator DoAttack(Spell spell, int accuracy, int speed)
    {
        spellArea.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        CombatController.SpellResult result = controller.DoPlayerTurn(spell, accuracy, speed);
        logArea.SetText(result.damage > 0
            ? $"You cast {result.spell} and it did {result.damage} damage"
            : $"You failed to cast {result.spell}");
        if (CheckIfEnding()) yield break;
        yield return new WaitForSeconds(2);
        result = controller.DoAITurn();
        logArea.SetText(result.damage > 0
            ? $"{loader.Info.Enemy.name} cast {result.spell} and it did {result.damage} damage"
            : $"{loader.Info.Enemy.name} failed to cast {result.spell}");
        optionsSection.gameObject.SetActive(true);
        CheckIfEnding();
    }

    private bool CheckIfEnding()
    {
        if (loader.Info.Player.Health <= 0)
        {
            StartCoroutine(PlayerLost());
            return true;
        }
        
        if (loader.Info.Enemy.Health <= 0) {
                StartCoroutine(PlayerWon());
                return true;
        }

        return false;

        IEnumerator PlayerWon()
        {
            yield return new WaitForSeconds(2);

            logArea.SetText("You won :D");
            yield return new WaitForSeconds(2);

            CombatLoader.Instance.Complete(new CombatResult(true, false, loader.Info.Player.Health,
                loader.Info.Enemy.Health));
        }
        
        IEnumerator PlayerLost()
        {
            yield return new WaitForSeconds(2);
            logArea.SetText("You lost :D");
            yield return new WaitForSeconds(2);

            CombatLoader.Instance.Complete(new CombatResult(false, false, loader.Info.Player.Health,
                loader.Info.Enemy.Health));
        }
    }
}
