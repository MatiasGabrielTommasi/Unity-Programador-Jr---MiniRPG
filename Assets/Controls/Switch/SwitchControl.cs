using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Assets.Library.Events;
using Unity.VisualScripting;
using Assets.Scripts.UI;

namespace Assets.Controls
{
    public class SwitchControl : MonoBehaviour
    {
        [SerializeField] Sprite on;
        [SerializeField] Sprite onDisabled;
        [SerializeField] Sprite off;
        [SerializeField] Sprite offDisabled;
        [SerializeField] bool toggled;
        [SerializeField] bool isEnabled;
        Image control;
        private void Start()
        {
            control = gameObject.GetComponent<Image>();
        }
        private void Update()
        {
            if(toggled)
                control.sprite = (isEnabled) ? on : onDisabled;
            else
                control.sprite = (isEnabled) ? off : offDisabled;
        }
        public bool IsToggled() => toggled;
        public bool IsEnabled() => isEnabled;
        public void Click()
        {
            if (isEnabled)
            {
                toggled = !toggled;
                UiPlayerController.Instance.SwitchToggled(this);
            }
        }
    }
}