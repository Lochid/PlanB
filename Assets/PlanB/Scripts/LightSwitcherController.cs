using UnityEngine;

public class LightSwitcherController : MonoBehaviour
{
    public bool turnedOn;
    Animator animationController { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        animationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationController.SetBool("TurnedOn", turnedOn);
    }
}
