using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerCandle : MonoBehaviour
{
    [SerializeField] private float fuelСonsumption;
    [SerializeField] private float originalitensity;
    [SerializeField] private float sparkleFrequency;
    [SerializeField] private Light2D _light;
    private GameSession session;
    private float sparklePhase;
    private void Start()
    {
        session = FindObjectOfType<GameSession>();
    }


    private void Update()
    {
        if (session.data.LightFuel.Value>30)
        {
            _light.intensity = originalitensity;
            session.data.LightFuel.Value -= Mathf.Min(fuelСonsumption*Time.deltaTime, session.data.LightFuel.Value);
        }else
        if (session.data.LightFuel.Value > 0)
        {
            sparklePhase += Time.deltaTime;
            _light.intensity = 0.1f + (originalitensity - 0.1f) *  Mathf.Abs(Mathf.Cos(sparklePhase* sparkleFrequency));
            session.data.LightFuel.Value -= Mathf.Min(fuelСonsumption * Time.deltaTime, session.data.LightFuel.Value);
        }
        else
        {
            this.gameObject.SetActive(false);
            
        }

    }
}
