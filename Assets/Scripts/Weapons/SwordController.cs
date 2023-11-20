using Assets.Library;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class SwordController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("HIT!");
            if (collision.gameObject.CompareTag(Tags.Sword))
            {
            }
        }
    }
}