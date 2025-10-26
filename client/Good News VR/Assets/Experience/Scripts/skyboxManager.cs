using UnityEngine;

public class skyboxManager : MonoBehaviour
{
    public Material skybox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // skybox.SetFloat("_Time", Time.time);
        RenderSettings.skybox = skybox;
    }
}
