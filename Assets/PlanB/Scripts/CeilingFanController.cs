using System.Collections.Generic;
using UnityEngine;

namespace PlanB.Assets
{
    public class CeilingFanController : MonoBehaviour
    {
        public Animator fanAnimator;
        public bool turnedOn { get; private set; } = false;

        public List<AudioSource> turnOnSounds;
        public List<AudioSource> turnOffSounds;
        public List<AudioSource> workingSounds;
        public void Turn()
        {
            SetPower(!turnedOn);
        }
        public void SetPower(bool power)
        {
            turnedOn = power;
            fanAnimator?.SetBool("Powered", power);
            if (turnedOn)
            {
                foreach (var sound in turnOnSounds)
                {
                    sound.Play();
                }
                foreach (var sound in workingSounds)
                {
                    sound.PlayDelayed(2.5f);
                }
            }
            else
            {
                foreach (var sound in workingSounds)
                {
                    sound.Stop();
                }
                foreach (var sound in turnOffSounds)
                {
                    sound.Play();
                }
            }
        }
    }
}