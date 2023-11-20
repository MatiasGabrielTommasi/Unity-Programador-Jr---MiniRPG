using Assets.Controls;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiPlayerController : MonoBehaviour
    {
        public bool enableSwitch1;
        public bool enableSwitch2;
        public static UiPlayerController Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        void Start()
        {

        }
        void Update()
        {

        }
        public void SwitchToggled(SwitchControl sw)
        {
            switch (sw.name)
            {
                case "sw1":

                    break;
                case "sw2":
                    break;
            }
        }
    }
}