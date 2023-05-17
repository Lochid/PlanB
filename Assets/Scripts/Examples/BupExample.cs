using UnityEngine;

namespace PlanB
{
    [RequireComponent(typeof(AudioSource))]
    public class BupExample : MonoBehaviour
    {
        AudioSource audioData;
        void Start()
        {
            audioData = GetComponent<AudioSource>();
        }

        public void  Play()
        {
            audioData.Play();
        }
    }
}