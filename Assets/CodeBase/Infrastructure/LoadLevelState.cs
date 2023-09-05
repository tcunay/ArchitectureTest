using System;
using CodeBase.CameraLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string HeroPrefabPath = "Hero/hero";
        private const string HudPrefabPath = "Hud/Hud";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
            
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject hero = Instantiate(HeroPrefabPath, at: initialPoint.transform.position);
            Instantiate(HudPrefabPath);
            CameraFollow(hero);
        }
        
        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        private void CameraFollow(GameObject following) => 
            Camera.main.GetComponent<CameraFollow>().Follow(following);
    }
}