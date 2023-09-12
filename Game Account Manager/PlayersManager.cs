using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.EventSystems;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private string fetchPlayersEndpoint = "http://127.0.0.1:13756/fetchPlayers";
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private GameObject reloadButton, nextButton, previousButton;
    [SerializeField] private Transform playerButtonContainer;

    private int startIndex = 0;
    private int maxPlayersToShow = 6;
    private int playersCount = 0;

    public delegate void OnPlayerSelected(string id, string fullname);
    public static event OnPlayerSelected onPlayerSelected;


    void Start()
    {
        LoginManager.onLoginSuccess += OnReloadClick;
    }

    public void OnReloadClick()
    {
        StartCoroutine(TryFetchPlayers());
    }

    private void ClearPlayerButtons()
    {
        foreach (Transform child in playerButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator TryFetchPlayers()
    {

        // Create the UnityWebRequest object for the GET request
        UnityWebRequest request = UnityWebRequest.Get(fetchPlayersEndpoint);

        // Include the token in the header
        string token = PlayerPrefs.GetString("token");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        // Send the request
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching players: " + request.error);
            alertText.text = "Error fetching players";
        }
        else
        {
            // Get the JSON response
            string jsonResponse = request.downloadHandler.text;
            PlayerDataWrapper wrapper = JsonUtility.FromJson<PlayerDataWrapper>("{\"players\":" + jsonResponse + "}");
            List<PlayerData> players = wrapper.players.ToList();

            // Remove the existing player buttons
            ClearPlayerButtons();

            // Update the UI with the fetched players
            playersCount = players.Count;

            playerButtonContainer.GetComponent<GridLayoutGroup>().constraintCount = maxPlayersToShow;

            for (int i = startIndex; i < Mathf.Min(startIndex + maxPlayersToShow, players.Count); i++)
            {
                PlayerData player = players[i];

                // Create a button for each player name
                GameObject playerButtonObj = new GameObject("PlayerButton");
                playerButtonObj.transform.SetParent(playerButtonContainer, false);
                Button playerButton = playerButtonObj.AddComponent<Button>();

                // Add the HorizontalLayoutGroup component to playerButtonObj
                HorizontalLayoutGroup layoutGroup = playerButtonObj.AddComponent<HorizontalLayoutGroup>();
                layoutGroup.childAlignment = TextAnchor.MiddleLeft;
                layoutGroup.spacing = 10f;

                // Create a child object for the player name
                GameObject playerNameObj = new GameObject("PlayerName");
                playerNameObj.transform.SetParent(playerButtonObj.transform, false);
                TextMeshProUGUI playerNameTMP = playerNameObj.AddComponent<TextMeshProUGUI>();
                playerNameTMP.text = $"{i + 1} - {player.fullName}";

                // Create a child object for the player points
                GameObject playerPointsObj = new GameObject("PlayerPoints");
                playerPointsObj.transform.SetParent(playerButtonObj.transform, false);
                TextMeshProUGUI playerPointsTMP = playerPointsObj.AddComponent<TextMeshProUGUI>();
                playerPointsTMP.text = player.points.ToString() + " pt";
                playerPointsTMP.alignment = TextAlignmentOptions.Right;

                // Add an onClick listener to the button
                playerButton.onClick.AddListener(() =>
              {
                  // Handle the button click
                  onPlayerSelected?.Invoke(player._id, player.fullName);
              });


            }


            // Activate the next and previous buttons based on the number of players
            bool showNextButton = players.Count > startIndex + maxPlayersToShow;
            bool showPreviousButton = startIndex > 0;

            ActivateButtons(showNextButton, showPreviousButton);


            alertText.text = "Select a player";
        }


    }

    private void ActivateButtons(bool showNextButton, bool showPreviousButton)
    {
        reloadButton.SetActive(true);
        nextButton.SetActive(showNextButton);
        previousButton.SetActive(showPreviousButton);
    }

    public void OnNextButtonClick()
    {
        if (startIndex + maxPlayersToShow < playersCount)
        {
            startIndex += maxPlayersToShow;
            StartCoroutine(TryFetchPlayers());
        }
    }

    public void OnPreviousButtonClick()
    {
        if (startIndex - maxPlayersToShow >= 0)
        {
            startIndex -= maxPlayersToShow;
            StartCoroutine(TryFetchPlayers());
        }
    }
}
