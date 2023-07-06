using System.Collections.Generic;
using UnityEngine;

public enum FluorescentLampState
{
    TurnedOn,
    TurnedOff,
    Blinking
}

public class FluorescentLampController : MonoBehaviour
{
    MeshRenderer mesh;
    public List<GameObject> lights;
    public Material turnOnMaterial;
    public Material turnOffMaterial;
    bool turnedOn = false;


    public float minBlinkPeriod = 0.01f;
    public float maxBlinkPeriod = 1;
    public FluorescentLampState state = FluorescentLampState.TurnedOff;
    public int turnOnBlinkTimes = 3;

    bool on = false;
    bool turningOnProcess = false;
    int iteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        switch(state)
        {
            case FluorescentLampState.TurnedOn:
                StartTurningOn();
                break;
            case FluorescentLampState.TurnedOff:
                StartTurningOff();
                break;
            case FluorescentLampState.Blinking:
                StartBlinking();
                break;
        }
    }

    void TurnOn()
    {
        var materials = mesh.materials;
        materials[1] = turnOnMaterial;
        mesh.materials = materials;
        foreach (var light in lights)
        {
            light.SetActive(true);
        }
        turnedOn = true;
    }

    void TurnOff()
    {
        var materials = mesh.materials;
        materials[1] = turnOffMaterial;
        mesh.materials = materials;
        foreach (var light in lights)
        {
            light.SetActive(false);
        }
        turnedOn = false;
    }

    void Blink()
    {
        if (turnedOn)
        {
            TurnOff();
            iteration++;
        }
        else
        {
            TurnOn();
        }
        if(iteration >= turnOnBlinkTimes && turningOnProcess)
        {
            TurnOn();
            return;
        }
        Invoke("Blink", Random.Range(minBlinkPeriod, maxBlinkPeriod));
    }

    public void StartTurningOn()
    {
        turningOnProcess = true;
        iteration = 0;
        CancelInvoke();
        Blink();
        on = true;
    }

    public void StartTurningOff()
    {
        turningOnProcess = false;
        CancelInvoke();
        TurnOff();
        on = false;
    }

    public void Turn()
    {
        if (on)
        {
            StartTurningOff();
        }
        else
        {
            StartTurningOn();
        }
    }

    public void StartBlinking()
    {
        turningOnProcess = false;
        CancelInvoke();
        Blink();
        on = true;
    }

    public void TurnBlinking()
    {
        if (on)
        {
            StartTurningOff();
        }
        else
        {
            StartBlinking();
        }
    }
}
