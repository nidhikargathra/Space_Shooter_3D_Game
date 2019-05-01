using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] RectTransform barRectTransform;
        float maxWidth;
       
        private void Awake()
        {
            maxWidth = barRectTransform.rect.width;
        }
        
        public void UpdateTimerDisplay(float timeLeft, float maxTime)
        {
            barRectTransform.sizeDelta = new Vector2(maxWidth * timeLeft/ maxTime, 10f);
        }
    }
}