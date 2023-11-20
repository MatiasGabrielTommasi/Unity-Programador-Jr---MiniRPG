using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class SkyManager : MonoBehaviour
    {
        [SerializeField] GameObject cloudObject1;
        [SerializeField] GameObject cloudObject2;
        bool cloud1Selector = false;
        [SerializeField] int cloudCount;
        float yCloudPosition => Random.Range(300f, 400f);
        float planeCloudPosition => Random.Range(-500, 500);

        void Start()
        {
            for (int i = 0; i < cloudCount; i++)
                SpawnClouds();
        }
        void SpawnClouds()
        {
            GameObject obj = (cloud1Selector) ? cloudObject1 : cloudObject2;
            obj = Instantiate(obj, new Vector3(planeCloudPosition, yCloudPosition, planeCloudPosition), obj.transform.rotation);
            obj.transform.parent = GameObject.Find("Sky").transform;
            cloud1Selector = !cloud1Selector;
        }
    }
}