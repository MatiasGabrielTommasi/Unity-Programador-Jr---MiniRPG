using Assets.Library;
using Assets.Scripts.Scenes;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 playerPosition;
        int powerCount = 0;
        List<Conditions> conditions = new List<Conditions>();
        Animator animator;
        Rigidbody rigidbody;
        LifebarProgress progress;
        [SerializeField] bool jumped;
        [SerializeField] float rotationSpeed;
        [SerializeField] float walkingSpeed;
        [SerializeField] string currentAction = string.Empty;
        [SerializeField] GameObject lifebarObject;
        [SerializeField] GameObject fireball;
        [SerializeField] GameObject bombball;
        [SerializeField] GameObject iceball;
        public static PlayerController Instance;
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
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            progress = lifebarObject.GetComponent<LifebarProgress>();
            gameObject.transform.position = new Vector3(-55.5542f, 21.12f, 58.4976f);
            //StartCoroutine(LifeMinus());
        }

        // Update is called once per frame
        void Update()
        {
            CheckMove();
        }
        public void Hurt(float damage) => progress.AddLife(-damage);
        private void OnCollisionEnter(Collision collision)
        {
            try
            {
                if (collision.gameObject.CompareTag(Tags.Portal))
                    SceneSelectorManager.TravelScene(collision.gameObject.GetComponent<ScenePortalController>().gameScene);
                else if (collision.gameObject.CompareTag(Tags.Floor))
                    jumped = false;
                else if (collision.gameObject.CompareTag(Tags.Heart))
                    progress.ResetLife();
                else if (collision.gameObject.CompareTag(Tags.Power))
                    jumped = false;
                else if (collision.gameObject.CompareTag(Tags.Enemy))
                {
                    if (collision.contactCount > 0)
                    {
                        if (collision.contacts[0].thisCollider.CompareTag(Tags.Sword))
                            Debug.Log("HIT!");
                        if (collision.contacts[0].thisCollider.CompareTag(Tags.Sword))
                            Debug.Log("HIT!");
                        else
                            Debug.Log("Enemy Collision!");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #region animations
        private void CheckMove()
        {
            playerPosition = GameObject.FindWithTag(Tags.Player).transform.position;
            if (progress.life > 0)
            {
                float forward = Input.GetAxis("Vertical");
                float sided = Input.GetAxis("Horizontal");
                if (forward != 0.0f || sided != 0.0f)
                    Walk(forward, sided);
                else
                    Idle();

                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey))
                    {
                        Debug.Log(vKey.ToString());
                        break;
                    }
                }
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
                    Jump(true);
                if (Input.GetKey(KeyCode.Y) /*|| Input.GetKey(KeyCode.Joystick1Button0)*/)
                    Roll();
                if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Joystick1Button3) && CheckPower(1))
                    Spin();
                if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Joystick1Button3) && CheckPower(2))
                    Fire();
                if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Joystick1Button3) && CheckPower(3))
                    Bomb();
                if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Joystick1Button3) && CheckPower(4))
                    Ice();
                if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Joystick1Button1))
                    Hit();
                if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.Joystick1Button4))
                    Defend();
            }
            else
                Die();
            //if (Input.GetKey(KeyCode.Alpha2))
            //    Recover();
        }
        /// <summary>
        /// <list>
        /// <item>1 -> spin</item>
        /// <item>2 -> fire</item>
        /// <item>3 -> bomb</item>
        /// <item>4 -> ice</item>
        /// </list>
        /// </summary>
        /// <param name="powerToCheck"></param>
        /// <returns></returns>
        private bool CheckPower(int powerToCheck) => powerCount >= powerToCheck;

        void Idle()
        {
            if (!jumped)
                ExecuteAction(Actions.Idle);
        }
        void Walk(float forward, float sided)
        {
            if (!jumped)
                ExecuteAction(Input.GetKey(KeyCode.L) ? Actions.SilentWalk : Actions.Walk);

            float speed = Input.GetKey(KeyCode.L) ? walkingSpeed * 0.5f : walkingSpeed;
            transform.Translate(Vector3.forward * forward * Time.deltaTime * speed);
            transform.Rotate(Vector3.up * sided * Time.deltaTime * rotationSpeed);
        }
        void Defend() => animator.SetBool(Actions.Defend, true);
        void Jump(bool jump)
        {
            if (!jumped)
            {
                jumped = true;
                ExecuteAction(Actions.Jump);
                rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }

        }
        void Hit() => ExecuteTrigger(Actions.Hit);
        void Spin() => ExecuteTrigger(Actions.Spin);
        private void Fire()
        {
            ExecuteTrigger(Actions.Hit);
            Instantiate(fireball, transform.position, transform.rotation);
        }
        private void Bomb()
        {
            ExecuteTrigger(Actions.Hit);
            Instantiate(bombball, transform.position, transform.rotation);
        }
        private void Ice()
        {
            ExecuteTrigger(Actions.Hit);
            Instantiate(iceball, transform.position, transform.rotation);
        }
        void Roll() => ExecuteTrigger(Actions.Roll);
        void Die() => ExecuteTrigger(Actions.Die);
        void Recover() => ExecuteTrigger(Actions.Recover);
        void ExecuteAction(string action)
        {

            currentAction = action;
            string[] actions = animator.parameters.Where(a => a.name.EndsWith("_b")).Select(x => x.name).ToArray();

            //if(action == Actions.Jump)
            //    actions = actions.Where(a => a.Equals(Actions.Jump)).ToArray();

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
        #endregion animations
        public float GetHitDamage()
        {
            float r = 25f;
            return r;
        }
    }
}