using UnityEngine;
using UnityEngine.Events;

namespace PlanB.Assets
{
    public class Usable : MonoBehaviour, IUsable
    {
        public bool Active { get; set; } = true;
        public bool Changable => false;
        public string ActionName { get; set; } = "Use";
        public UnityEvent OnUse;

        public void Use()
        {
            OnUse?.Invoke();
        }
        public void Next()
        {
        }
    }
}