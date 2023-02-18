using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	//-- set start time 00:00
    public int minutes = 0;
    public int hour = 0;
	public int seconds = 0;
	public bool realTime=true;
    public bool manualSet = false;

    public int targetMinutes;
    public int targetHour;
    public int targetMinutesRange = 5; // -= 5 minutes

	public GameObject pointerSeconds;
    public GameObject pointerMinutes;
    public GameObject pointerHours;
    
    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    float msecs=0;

void Start() 
{
	//-- set real time
	if (realTime)
	{
		hour=System.DateTime.Now.Hour;
		minutes=System.DateTime.Now.Minute;
		seconds=System.DateTime.Now.Second;
	}
}

    void Update()
    {
        if (!manualSet)
        {
            //-- calculate time
            msecs += Time.deltaTime * clockSpeed;
            if (msecs >= 1.0f)
            {
                msecs -= 1.0f;
                seconds++;
                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                    if (minutes > 60)
                    {
                        minutes = 0;
                        hour++;
                        if (hour >= 24)
                            hour = 0;
                    }
                }
            }
            SetAnglesFromTime();
        }
        else
        {
            SetTimeFromAngles();
        }
        
    }

        

        private void SetAnglesFromTime()
        {
            //-- calculate pointer angles
            float rotationSeconds = (360.0f / 60.0f) * seconds;
            float rotationMinutes = (360.0f / 60.0f) * minutes;
            float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

            //-- draw pointers
            pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
            pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
            pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
        }
        
        private void SetTimeFromAngles()
        {
            float minuteAngle = pointerMinutes.transform.localEulerAngles.z;
            float hourAngle = pointerHours.transform.localEulerAngles.z;

            minuteAngle = 6 * Mathf.Floor(minuteAngle / 6);
            hourAngle = 30 * Mathf.Floor(hourAngle / 30);

            minutes = (int)minuteAngle / 6;
            hour = (int)hourAngle / 30;
        }

        public int minuteIncrement = 10;
        public int hourIncrement = 1;
        public void HandClicked(GameObject clickedHand)
        {
            Debug.Log($"Hand Clicked {clickedHand.name}");
            if(clickedHand == pointerMinutes)
            {
                minutes += minuteIncrement;
                SetAnglesFromTime();
            }
            else if(clickedHand == pointerHours)
            {
                hour += hourIncrement;
                SetAnglesFromTime();
            }
        }
    }


