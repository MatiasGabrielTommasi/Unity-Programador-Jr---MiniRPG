using Assets.Library;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes
{
    public static class SceneSelectorManager
    {
        [SerializeField] static GameObject playerObject;
        [SerializeField] static GameObject cameraObject;
        public static void TravelScene(GameScenes gameSceneIndex)
        {
            try
            {
                Vector3 playerPosition = new Vector3(9.5f, 21.138f, -6.2f);
                switch (gameSceneIndex)
                {
                    case GameScenes.ToysScene: playerPosition = new Vector3(-633.1819f, 98.42968f, -195.2521f); break;
                    case GameScenes.SeaScene: playerPosition = new Vector3(-39.32863f, 14.3f, -23.9f); break;
                    case GameScenes.RailwayScene: playerPosition = new Vector3(-13.92534f, 15.16171f, -12.36134f); break;
                    case GameScenes.VulcanScene: playerPosition = new Vector3(-12.88416f, 14.08464f, -81.09182f); break;
                    case GameScenes.SpaceScene: playerPosition = new Vector3(109f, 1033.04f, 261.51f); break;
                    case GameScenes.DreamScene: break;
                }
                //GameObject player = GetPlayer();
                //GameObject camera = GetCamera();
                PlayerController.Instance.gameObject.transform.position = playerPosition;
                Scene scene = SceneManager.GetSceneByBuildIndex((int)gameSceneIndex);
                SceneManager.LoadScene((int)gameSceneIndex);
                //SceneManager.MoveGameObjectToScene(PlayerController.Instance.gameObject, scene);
                //SceneManager.MoveGameObjectToScene(CameraController.Instance.gameObject, scene);
            }
            catch (System.Exception ex)
            {
            }
        }
        //private static GameObject GetPlayer()
        //{
        //    GameObject player = GameObject.Find("player");
        //    if (player == null)
        //        player = Object.Instantiate(playerObject, GameObject.FindGameObjectsWithTag transform);
        //    return player;
        //}
        //private static GameObject GetCamera()
        //{
        //    GameObject camera = GameObject.Find("MainCamera");
        //    if (camera == null)
        //        camera = Object.Instantiate(cameraObject, transform);
        //    return camera;
        //}
    }
}