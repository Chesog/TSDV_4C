using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

public class EnemiesManager : MonoBehaviour
{
    #region EVENTS

    public static event Action OnNoEnemies;
    public static event Action OnGamePause;
    public static event Action OnFinalVideo;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private GameObject _winningPanel;
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private int _kills = 0;
    [SerializeField] private VideoHandeler _videoHandeler;

    #endregion

    #region PRIVATE_FIELDS

    private int _currentRoom = 1;

    #endregion
    
    #region PUBLIC_FIELDS

    public int _room1NecessaryKills = 10;
    public int _room2NecessaryKills = 0;
    public int _room3NecessaryKills = 7;
    public int _room4NecessaryKills = 0;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        EnemyInputManager.OnEnemyDestroy += IncreaseKill;
        DistanceEnemy.OnDestroyed += IncreaseKill;
        Boss.OnDestroyed += StartShowVictoryPanel;
        RoomManager.OnNewRoom += ResetKill;
        RoomManager.OnNewRoom += IncreaseCurrentRoom;
    }


    private void Start()
    {
        _winningPanel.SetActive(false);
    }

    private void Update()
    {
        if (_currentRoom == 1 && _kills >= _room1NecessaryKills)
        {
            OnNoEnemies?.Invoke();
        }
        else if (_currentRoom == 2 && _kills >= _room2NecessaryKills)
        {
            OnNoEnemies?.Invoke();
        }
        else if (_currentRoom == 3 && _kills >= _room3NecessaryKills)
        {
            OnNoEnemies?.Invoke();
        }
        else if (_currentRoom == 4 && _kills >= _room4NecessaryKills)
        {
            OnNoEnemies?.Invoke();
        }
        
    }

    private void OnDestroy()
    {
        EnemyInputManager.OnEnemyDestroy -= IncreaseKill;
        DistanceEnemy.OnDestroyed -= IncreaseKill;
        Boss.OnDestroyed -= IncreaseKill;
        RoomManager.OnNewRoom -= ResetKill;
        RoomManager.OnNewRoom -= IncreaseCurrentRoom;
    }

    #endregion

    #region PRIVATE_METHODS

    private void IncreaseKill()
    {
        _kills++;
    }
    private void IncreaseCurrentRoom()
    {
        _currentRoom++;
    }

    private void StartShowVictoryPanel()
    {
        StartCoroutine(ShowVictoryPanel());    }
    private IEnumerator ShowVictoryPanel()
    {
        if (_winningPanel != null)
        { 
            OnFinalVideo?.Invoke();
            OnGamePause?.Invoke();
            _videoHandeler.PlayVideo();
            
            while (!_videoHandeler.IsPaused())
            {
                yield return null;
            }

            _winningPanel.SetActive(true);
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public void ResetKill()
    {
        _kills = 0;
    }

    #endregion
}