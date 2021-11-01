using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameRules : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private Text inputField;

    public UnityEvent loseGameEvent;

    private Coroutine correntCor;
    public void StartGame()
    {
        timer.CurrentTime = 0;
        timer.StartTimer();
        StartLoseGameMachine();
    }




    public void StopGame()
    {
        timer.StopTimer();
        StopLoseGameMachine();
        CompareResults();
    }

    private void StartLoseGameMachine()
    {
        float randomTime = Random.Range(0, timer.MaxTime);
        correntCor = StartCoroutine(LoseGameCor(randomTime));
    }
    private void StopLoseGameMachine()
    {
        StopCoroutine(correntCor);
    }

    private void CompareResults()
    {
        scoreCounter.takeAway(timer.CurrentTime / 2);
    }

    private IEnumerator LoseGameCor(float time)
    {
        yield return new WaitForSeconds(time);
        loseGameEvent?.Invoke();
        timer.StopTimer();
        yield break;
    }

    private void OnEnable()
    {
        timer.stopTimeEvent.AddListener(CompareResults);
    }
    private void OnDisable()
    {
        timer.stopTimeEvent.RemoveListener(CompareResults);
    }
}
