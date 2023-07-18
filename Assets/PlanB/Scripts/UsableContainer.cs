using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanB.Assets
{
    public class UsableContainer : MonoBehaviour, IUsable
    {
        public List<Usable> usableList;
        int usableIndex = 0;
        public Usable usable
        {
            get
            {
                if(usableIndex >= usableList.Count)
                {
                    return null;
                }
                return usableList[usableIndex];
            }
        }
        public bool Active
        {
            get
            {
                if (usable != null)
                {
                    return usable.Active;
                }
                return false;
            }
        }


        public string ActionName
        {
            get
            {
                if(usable!=null)
                {
                    return usable.ActionName; 
                }
                return "";
            }
        }
        public bool Changable => true;

        public void Use()
        {
            usable.Use();
        }

        public void Next()
        {
            usableIndex = (usableIndex + 1) % usableList.Count;
        }
    }
}