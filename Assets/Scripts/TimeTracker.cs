using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public delegate void TimeUpHandler();
    private TimeUpHandler handler_;
    private float secondsLeft_ = 0.0f;

    void Update() {
        if(handler_ == null) return;

        if(secondsLeft_ < 0)
            handler_();

        if(secondsLeft_ > 0) {
            secondsLeft_ -= Time.deltaTime;
        }
    }

    public void StartTimer(float seconds, TimeUpHandler handler) {
        secondsLeft_ = seconds;
        handler_ = handler;
    }

    public void StopTimer() {
        secondsLeft_ = 0.0f;
        handler_ = null;
    }
}
