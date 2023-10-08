using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        protected IPersistantProgressService ProgressService { get; private set; }
        protected PlayerProgress Progress => ProgressService.Progress;

        public void Construct(IPersistantProgressService progressService) => 
            ProgressService = progressService;

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            CleanUp();

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(()=> Destroy(gameObject));

        protected virtual void Initialize() { }

        protected virtual void SubscribeUpdates() { }

        protected virtual void CleanUp() { }
    }
}