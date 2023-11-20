using UnityEngine;
namespace Assets.Scripts.Scenes
{
    public class CloudMovement : MonoBehaviour
    {
        [SerializeField] float terrainRange = 500f;
        [SerializeField] Vector3 startPosition;
        private void Start()
        {
            startPosition = new Vector3(transform.position.x, transform.position.y, -terrainRange);
        }
        void Update()
        {
            if (transform.position.z < -terrainRange || transform.position.z > terrainRange)
                transform.position = startPosition;

            transform.Translate(Vector3.forward * Random.Range(0.5f, 10.5f) * Time.deltaTime);
        }
    }
}