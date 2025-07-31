using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class UI_StatBar : MonoBehaviour
    {
        //Variable to scale bar size based on stat
        private Slider slider;
        
        //Stamina consumes amount indicator bar

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public virtual void SetStat(int newValue)
        {
            slider.value = newValue;
        }

        public virtual void SetMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
        
    }
}
