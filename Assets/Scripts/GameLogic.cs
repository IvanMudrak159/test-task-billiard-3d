using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private int ballCount = 10;
    [SerializeField] private List<GameObject> switchingObjects;

    private void OnEnable()
    {
        Cue.onBallHit += TurnOffSwitchingObjects;
        WhiteBall.onBallStoped += TurnOnSwitchingObjects;
        BallTriggerDetection.onPocket += UpdateBallCount;
    }

    private void OnDisable()
    {
        Cue.onBallHit -= TurnOffSwitchingObjects;
        WhiteBall.onBallStoped -= TurnOnSwitchingObjects;
    }

    public void UpdateBallCount(Transform ball)
    {
        ball.gameObject.SetActive(false);
        if (ball.CompareTag("White Ball"))
        {
            ball.position = new Vector3(-4, 0, 0);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.gameObject.SetActive(true);
        }
        else
        {
            ballCount -= 1;
            if (ballCount == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void TurnOffSwitchingObjects()
    {
        foreach (var objectToSwitch in switchingObjects)
        {
            objectToSwitch.SetActive(false);
        }
    }
    private void TurnOnSwitchingObjects()
    {
        foreach (var objectToSwitch in switchingObjects)
        {
            objectToSwitch.SetActive(true);
        }
    }
}
