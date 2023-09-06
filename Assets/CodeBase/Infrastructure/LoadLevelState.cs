using System;
using CodeBase.CameraLogic;
using CodeBase.Logic;
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
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject hero = Instantiate(HeroPrefabPath, at: initialPoint.transform.position);
            Instantiate(HudPrefabPath);
            CameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
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