using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI pountCountText;

    private int _coinCount;
    private int _pointCount;
    private bool _questionBlockHit;
    
    private DateTime startTime;
    private TimeSpan levelDuration = TimeSpan.FromSeconds(400);
    
    public GameObject playerHeh;

    private void Start()
    {
        updateCointText(0);
        levelDuration = TimeSpan.FromSeconds(400);
        startTime = DateTime.Now;

    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan timeLeft = levelDuration - (DateTime.Now - startTime);
        string timeString = $"Time \n {timeLeft.TotalSeconds:F0}";
        timerText.text = timeString;
        

        Debug.DrawRay(playerHeh.transform.position, Vector3.up * 2.5f, Color.red);

        // Cast a ray from the player's position upwards
        RaycastHit hit;
        if (Physics.Raycast(playerHeh.transform.position, Vector3.up, out hit, 2.5f))
        {
            GameObject hitObject = hit.collider.gameObject;
            
                Debug.Log("Hit something");
                
                if (hitObject.CompareTag("Brick"))
                {
                    Destroy(hitObject);
                    updateCointText(10);

                }
                
                if (hitObject.CompareTag("Question") && !_questionBlockHit)
                {
                    _questionBlockHit = true;
                    addCoin();
                    updateCointText(100);
                }
                else
                {
                    _questionBlockHit = false;
                }
            
        }
    }

    public void addCoin()
    {
        _coinCount++;
        coinCountText.text = $"X{_coinCount:D2}";
    }

    public void updateCointText(int points)
    {
        _pointCount += points;
        string pointString = $"Mario\n{_pointCount:D6}";
        pountCountText.text = pointString;
    }
}
