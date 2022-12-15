
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    public float tiempo;//cambiar esto para vincularlo con la vida 


    private void FixedUpdate()
    {
        tiempo = Time.timeScale;
        Time.timeScale += (1f / slowdownLength)*Time.deltaTime;
        if (Time.timeScale>1)
        {
            Time.timeScale = 1;
        }
    }
    public void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
