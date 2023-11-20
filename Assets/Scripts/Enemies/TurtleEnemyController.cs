using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class TurtleEnemyController : BaseEnemy
    {
        [SerializeField] bool jumped;
        private void Awake()
        {
            damage = 10;
        }
        private void Start()
        {
            base.BaseStart();
        }
        private void Update()
        {
            base.BaseUpdate();
        }
    }
}