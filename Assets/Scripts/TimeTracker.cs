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
            Debug.Log(secondsLeft_);
        }
    }

    public void StartTimer(float seconds, TimeUpHandler handler) {
        secondsLeft_ = seconds;
        handler_ = handler;
    }

    public void StopTimer() {
        Debug.Log("Stopping timer");
        secondsLeft_ = 0.0f;
        handler_ = null;
    }
}
