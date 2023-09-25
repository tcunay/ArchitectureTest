using UnityEngine;

namespace CodeBase.Logic
{
    public class UniqueId : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public void SetId(string id)
        {
            _id = id;
        }
    }
}