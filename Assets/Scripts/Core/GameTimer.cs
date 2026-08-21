using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startingTime = 30f;

    private float currentTime;
    private bool running;

    public float CurrentTime => currentTime;

    public void StartTimer()
    {
        currentTime = startingTime;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    private void Update()
    {
        if (!running)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            running = false;

            GameManager.Instance.TimeUp();
        }

        GameManager.Instance.UpdateTimerUI(currentTime);
    }
}