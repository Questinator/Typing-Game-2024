using UnityEngine;
using UnityEngine.UI;

namespace Combat.Controller
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;
        private int hp;
        private int maxHp;


        public void UpdateBar()
        {
            slider.maxValue = maxHp;
            slider.value = hp;
            fill.color = gradient.Evaluate(1 - slider.normalizedValue);
        }
        
        
        public int MaxHp
        {
            get => maxHp;
            set => maxHp = value;
        }

        public int Hp
        {
            get => hp;
            set => hp = value;
        }
    }
}