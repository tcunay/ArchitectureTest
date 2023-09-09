using UnityEngine;

namespace CodeBase.Enemy
{
    public class AgentRotateToHero : FollowToHerro
    {
        [SerializeField] private float _speed;
        
        private Vector3 _positionToLook;

        private void Update()
        {
            if (HeroIsInit()) 
                RotateTowardsHerro();
        }

        private void RotateTowardsHerro()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => 
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private float SpeedFactor() => 
            _speed * Time.deltaTime;

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private void UpdatePositionToLookAt()
        {
            Vector3 position = transform.position;
            Vector3 positionDiff = Herro.position - position;
            _positionToLook = new Vector3(positionDiff.x, position.y, positionDiff.z);
        }
    }
}