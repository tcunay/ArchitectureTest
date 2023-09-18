using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private FollowToHerro _follow;
        [SerializeField] private float _coolDown;
        
        private Coroutine _aggroCoroutine;
        private WaitForSeconds _coolDownWaitInstruction;
        private bool _hasAggroTarget;

        private void Start()
        {
            _coolDownWaitInstruction = new WaitForSeconds(_coolDown);
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget == false)
            {
                _hasAggroTarget = true;
                
                StopAggroCoroutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                
                _aggroCoroutine = StartCoroutine(SwitchFollowAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowAfterCooldown()
        {
            yield return _coolDownWaitInstruction;
            SwitchFollowOff();
        }

        private void SwitchFollowOn() => 
            _follow.enabled = true;

        private void SwitchFollowOff() => 
            _follow.enabled = false;
    }
}