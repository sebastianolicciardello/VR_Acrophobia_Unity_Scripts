using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private string fetchPlayersEndpoint = "http://127.0.0.1:13756/fetchPlayers";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private GameObject reloadButton;

    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerPointsText;


    void Start()
    {
        LoginManager.onLoginSuccess += OnReloadClick;
    }

    public void OnReloadClick()
    {

        alertText.text = "Reloading...";
        ActivateButtons(false);

        StartCoroutine(TryFetchPlayers());
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

            // Update the UI with the fetched players
            playerNameText.text = "";
            playerPointsText.text = "";

            for (int i = 0; i < players.Count; i++)
            {
                PlayerData player = players[i];

                // Create a button for each player name
                GameObject playerButtonObj = Instantiate(playerNameText.gameObject, playerNameText.transform.parent);
                Button playerButton = playerButtonObj.GetComponent<Button>();

                // Set the player name as button text
                TextMeshProUGUI playerNameTMP = playerButtonObj.GetComponent<TextMeshProUGUI>();
                playerNameTMP.text = $"{i + 1}. {player.fullName}";

                // Add an onClick listener to the button
                playerButton.onClick.AddListener(() =>
                {
                    // Handle the button click
                    Debug.Log($"Button clicked for player: {player.fullName}");
                });

                // Append the player points to the points text
                playerPointsText.text += player.points.ToString() + "\n";
            }

            alertText.text = "Players fetched successfully";
        }

        // Riattiva il pulsante di ricarica
        ActivateButtons(true);
    }

    private void ActivateButtons(bool toggle)
    {
        reloadButton.SetActive(toggle);
    }
}
