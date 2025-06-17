using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PopulateLeaderboard : MonoBehaviour
{

    public GameObject rowPrefab;
    public Transform rowsParent;

    public void LeaderboardButton()
    {
        GetLeaderBoard();
    }

    public static void SendLeaderBoard(int money)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="MoneyGrabbed",
                    Value=money
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnLeaderBoardError);
    }

    static void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Data sent to leaderboard!");
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MoneyGrabbed",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnLeaderBoardError);
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newRow.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }
    }

    static void OnLeaderBoardError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
