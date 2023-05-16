using UnityEngine;

namespace PlanB
{
    public class LogExample : MonoBehaviour
    {
        public string Text;
        public void Log()
        {
            Debug.Log(Text);
        }
    }
}