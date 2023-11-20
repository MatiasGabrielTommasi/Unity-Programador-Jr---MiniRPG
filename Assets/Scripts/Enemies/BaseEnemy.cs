using Assets.Library;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public bool hasPower = false;
        public bool canHeal = false;
        public Animator animator;
        public Rigidbody rigidbody;
        public LifebarProgress progress;
        [SerializeField] public float life;
        [SerializeField] float playerProximity;
        [SerializeField] float walkingSpeed;
        [SerializeField] public float damage;
        [SerializeField] public GameObject lifebarObject;
        [SerializeField] public GameObject powerGameObject;
        [SerializeField] public GameObject lifeGameObject;
        [SerializeField] string currentAction = string.Empty;
        bool diedActive = false;
        public void BaseStart() => Start();
        public void BaseUpdate() => Update();
        private void Start()
        {
            progress = lifebarObject.GetComponent<LifebarProgress>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            progress.life = life;
        }
        private void Update()
        {
            if (progress.life > 0)
                Move();
            else
                StartCoroutine(Die());
        }
        private void Move()
        {
            MoveTowardsPlayer();
            Attack();
        }
        private void MoveTowardsPlayer()
        {
            Vector3 lookDirection = (PlayerController.Instance.playerPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
            if (CheckAtackProximity())
                ExecuteAction(Actions.Idle);
            else if (CheckPlayerProximity())
            {
                ExecuteAction(Actions.Walk);    
                rigidbody.AddForce(lookDirection * walkingSpeed);
            }
            else
                ExecuteAction(Actions.Idle);
        }
        public bool attack = false;
        public virtual void Attack()
        {
            if(CheckAtackProximity())
            {
                //rigidbody.AddForce(PlayerController.Instance.playerPosition * Time.deltaTime * walkingSpeed);
                ExecuteTrigger((Random.value < 0.5f) ? Actions.Hit : Actions.Hit2);
                attack = true;
            }
        }
        private IEnumerator Die()
        {
            ExecuteTrigger(Actions.Die);
            if (canHeal)
                Instantiate(lifeGameObject, gameObject.transform.position, lifeGameObject.transform.rotation);
            if (hasPower)
                Instantiate(powerGameObject, gameObject.transform.position, lifeGameObject.transform.rotation);

            Destroy(gameObject);
            yield return new WaitForSeconds(0.5f);
        }
        private bool CheckPlayerProximity()
        {
            bool r = true;
            r = Vector3.Distance(PlayerController.Instance.playerPosition, transform.position) <= 5 * playerProximity;
            return r;
        }
        /// <summary>
        /// ver si puedo meter otra animacion en el mientras para otros enemigos y usar herencia
        /// </summary>
        /// <returns></returns>
        public bool CheckAtackProximity() => Vector3.Distance(PlayerController.Instance.playerPosition, transform.position) <= 1;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                if (!attack && collision.contacts[0].otherCollider.CompareTag(Tags.Sword))
                    Hurt();
                else if (attack)
                {
                    attack = false;
                    PlayerController.Instance.Hurt(damage);
                }
            }
        }

        private void Hurt()
        {
            progress.AddLife(-PlayerController.Instance.GetHitDamage());
            ExecuteTrigger(Actions.Hurt);
        }
        void ExecuteAction(string action)
        {
            currentAction = action;
            string[] actions = animator.parameters.Where(a => a.name.EndsWith("_b")).Select(x => x.name).ToArray();

            foreach (string parameter in actions)
                animator.SetBool(parameter, false);

            animator.SetBool(action, true);
        }
        void ExecuteTrigger(string trigger)
        {
            animator.SetBool(currentAction, false);
            animator.SetTrigger(trigger);
            animator.SetBool(currentAction, true);
        }
    }
}