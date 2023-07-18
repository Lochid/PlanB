using UnityEngine;

public class LightSwitcherController : MonoBehaviour
{
    Animator animationController { get; set; }
    void Start()
    {
        animationController = GetComponent<Animator>();
    }
    public void TurnOn()
    {
        animationController.SetBool("TurnedOn", true);
    }
    public void TurnOff()
    {
        animationController.SetBool("TurnedOn", false);
    }
}
