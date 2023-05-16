using UnityEngine;
using UnityEngine.Events;

namespace PlanB
{
    public class Usable : MonoBehaviour
    {
        public bool Active = true;
        public string ActionName = "Use";
        public UnityEvent OnUse;

        public void Use()
        {
            OnUse?.Invoke();
        }
    }
}