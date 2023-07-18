using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlanB.Assets
{
    public class SwitchController : MonoBehaviour
    {
        public Usable usable;
        public bool turnedOn = false;
        public string turnOnText = "on";
        public string turnOffText = "off";
        public UnityEvent OnTurnOn;
        public UnityEvent OnTurnOff;

        // Start is called before the first frame update
        void Start()
        {
            ChangeText();
        }

        void ChangeText()
        {
            if (usable != null)
            {
                usable.ActionName = turnedOn ? turnOffText : turnOnText;
            }
        }

        void CallEvents()
        {
            if (turnedOn)
            {
                OnTurnOn.Invoke();
            }
            else
            {
                OnTurnOff.Invoke();
            }
        }

        // Update is called once per frame
        public void Switch()
        {
            turnedOn = !turnedOn;
            ChangeText();
            CallEvents();
        }
    }
}