using Assets.Scripts;
using Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LifebarProgress : MonoBehaviour
    {
        [SerializeField] public float life;
        [SerializeField] Image lifebarProgress;
        [SerializeField] Sprite lifeSpriteSuccess;
        [SerializeField] Sprite lifeSpriteWarning;
        [SerializeField] Sprite lifeSpriteDanger;
        float lifeStart;
        void Start()
        {
            lifeStart = life;
        }
        void Update() => UpdateLifebar();
        public void AddLife(float lifeToAdd) => life += lifeToAdd;
        private void UpdateLifebar()
        {
            if (lifebarProgress == null)
                lifebarProgress = GameObject.FindWithTag("lifebar_progress").GetComponent<Image>();
            float percentageRemainingLife = ((life * 100f) / lifeStart) / 100f;
            lifebarProgress.fillAmount = percentageRemainingLife;
            if (percentageRemainingLife >= 0.65f && lifebarProgress.sprite != lifeSpriteSuccess)
                lifebarProgress.sprite = lifeSpriteSuccess;
            else if (percentageRemainingLife < 0.65f && percentageRemainingLife >= 0.3f && lifebarProgress.sprite != lifeSpriteWarning)
                lifebarProgress.sprite = lifeSpriteWarning;
            else if (percentageRemainingLife < 0.3f && lifebarProgress.sprite != lifeSpriteDanger)
                lifebarProgress.sprite = lifeSpriteDanger;

            lifebarProgress.transform.parent.transform.parent.transform.rotation = Camera.main.transform.rotation;
        }

        public void ResetLife() => life = lifeStart;
    }
}