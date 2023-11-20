using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class GravityElement : MonoBehaviour
    {
        const float GRAVITY = 6.674f;
        public Rigidbody rb;
        private void FixedUpdate()
        {
            GravityElement[] elements = FindObjectsOfType<GravityElement>();
            foreach (GravityElement element in elements)
            {
                if (element != this)
                    Attract(element);
            }
        }
        void Attract(GravityElement element)
        {
            Rigidbody rbToAttract = element.rb;
            Vector3 direction = rb.position - rbToAttract.position;
            float distance = direction.magnitude;

            float forceMagnitude = GRAVITY * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
            Vector3 force = direction.normalized * forceMagnitude;

            rbToAttract.AddForce(force);
        }
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    }
}