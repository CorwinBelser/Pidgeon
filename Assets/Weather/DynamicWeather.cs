using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWeather : MonoBehaviour {

    private Transform player;
    private Transform weather;
    public float weatherHeight = 50f; //height from ground 

    public float switchWeatherTimer = 0;
    public float resetWeatherTimer = 20f; //defines what you set the timer to

    public WeatherStates weatherStates; //naming convention of weather states
    private int switchWeather; //name conv of switch range

    public ParticleSystem sunCloudParticleSystem; //creates inspector slot
    public ParticleSystem rainParticleSystem;
    public ParticleSystem fogParticleSystem;
    public ParticleSystem snowParticleSystem;
    public ParticleSystem pollenParticleSystem;

    public float audioFadeTime = .25f;
    public AudioClip sunAudio;
    public AudioClip rainAudio;
    public AudioClip fogAudio;
    public AudioClip snowAudio;

    public float lightDimTime = .1f;
    public float rainIntensity = 0f;
    public float sunIntensity = 1f;
    public float fogIntensity = .5f;
    public float snowIntensity = .25f;

    public Color sunFog;
    public Color rainFog;
    public Color fogFog;
    public Color snowFog;
    public float fogChangeSpeed = .1f;
    public float fogDensity;

    public Light mainLight;

    public enum WeatherStates //defines states of the weather
    {
        
        PickWeather, 
        SunnyWeather,
        RainWeather, 
        FoggyWeather, 
        SnowWeather
    }

	// Use this for initialization
	void Start () {
        //sunCloudParticleSystem = GetComponent<ParticleSystem>();
       // mainLight = GetComponent<Light>();
        StartCoroutine(WeatherSM());
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = fogDensity;
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.transform;
        GameObject weatherGameObject = GameObject.FindGameObjectWithTag("Weather");
        weather = weatherGameObject.transform;


    }
	
	// Update is called once per frame
	void Update () {
        SwitchWeatherTimer();
        weather.transform.position = new Vector3(player.position.x, player.position.y + weatherHeight, player.position.z);
    } 

    IEnumerator WeatherSM()
    {
        while (true) //while the switch is active
        {
            switch (weatherStates) // switch the weather states
            {
                case WeatherStates.PickWeather:
                    PickWeather();
                    break;
                case WeatherStates.SunnyWeather:
                    SunnyWeather();
                    break;
                case WeatherStates.RainWeather:
                    RainWeather();
                    break;
                case WeatherStates.FoggyWeather:
                    FoggyWeather();
                    break;
               /* case WeatherStates.SnowWeather:
                    SnowWeather();
                    break;*/
            }
            yield return null;
        }
    }

    void PickWeather()
    {
        switchWeather = Random.Range(0, 3);

      //  ParticleSystem.EmissionModule sunEmission = sunCloudParticleSystem.emission;
       // sunEmission.enabled = false;
        sunCloudParticleSystem.Stop();
        pollenParticleSystem.Stop();
        rainParticleSystem.Stop();
        fogParticleSystem.Stop();
       // pollenParticleSystem.Stop();
       // snowParticleSystem.Stop();
        //rainParticleSystem.enableEmission = false;
        //  fogParticleSystem.enableEmission = false;
        //snowParticleSystem.enableEmission = false;


        switch (switchWeather)
        {
            case 0:
                weatherStates = DynamicWeather.WeatherStates.SunnyWeather;
                break;
            case 1:
                weatherStates = DynamicWeather.WeatherStates.RainWeather;
                break;
            case 2:
                weatherStates = DynamicWeather.WeatherStates.FoggyWeather;
                break;
          /*  case 3:
                weatherStates = DynamicWeather.WeatherStates.SnowWeather;
                break;*/
            
        }
    }

    void SunnyWeather()
    {
        Debug.Log("Sunny");
        sunCloudParticleSystem.Play();
        pollenParticleSystem.Play();
        if(mainLight.intensity > sunIntensity)
        {
            mainLight.intensity -= Time.deltaTime * lightDimTime;
        }
        if (mainLight.intensity < sunIntensity)
        {
            mainLight.intensity += Time.deltaTime * lightDimTime;
        }

        Color currentFogCol = RenderSettings.fogColor;
        RenderSettings.fogColor = Color.Lerp(currentFogCol, sunFog, fogChangeSpeed * Time.deltaTime);
    }

    void RainWeather()
    {
        Debug.Log("Rain");
        rainParticleSystem.Play();
        if (mainLight.intensity > rainIntensity)
        {
            mainLight.intensity -= Time.deltaTime * lightDimTime;
        }
        if (mainLight.intensity < rainIntensity)
        {
            mainLight.intensity += Time.deltaTime * lightDimTime;
        }

        Color currentFogCol = RenderSettings.fogColor;
        RenderSettings.fogColor = Color.Lerp(currentFogCol, rainFog, fogChangeSpeed * Time.deltaTime);
    }

    void FoggyWeather()
    {
        Debug.Log("Fog");
        fogParticleSystem.Play();
        if (mainLight.intensity > fogIntensity)
        {
            mainLight.intensity -= Time.deltaTime * lightDimTime;
        }
        if (mainLight.intensity < fogIntensity)
        {
            mainLight.intensity += Time.deltaTime * lightDimTime;
        }

        Color currentFogCol = RenderSettings.fogColor;
        RenderSettings.fogColor = Color.Lerp(currentFogCol, fogFog, fogChangeSpeed * Time.deltaTime);
    }

    void SnowWeather()
    {
        Debug.Log("Snow");
        snowParticleSystem.Play();
        if (mainLight.intensity > snowIntensity)
        {
            mainLight.intensity -= Time.deltaTime * lightDimTime;
        }
        if (mainLight.intensity < snowIntensity)
        {
            mainLight.intensity += Time.deltaTime * lightDimTime;
        }

        Color currentFogCol = RenderSettings.fogColor;
        RenderSettings.fogColor = Color.Lerp(currentFogCol, snowFog, fogChangeSpeed * Time.deltaTime);
    }

    void SwitchWeatherTimer()
    {
        Debug.Log("SwitchWeatherTimer");

        switchWeatherTimer -= Time.deltaTime; //decrease time 

        if (switchWeatherTimer < 0)
        {
            switchWeatherTimer = 0;
        }

        if(switchWeatherTimer > 0)
        {
            return;
        }

        if(switchWeatherTimer == 0)
        {
            weatherStates = DynamicWeather.WeatherStates.PickWeather;
            switchWeatherTimer = resetWeatherTimer;

        }
    }
}
