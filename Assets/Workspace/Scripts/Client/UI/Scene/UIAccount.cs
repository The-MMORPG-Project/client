using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

namespace Valk.Networking 
{
    public class UIAccount : MonoBehaviour 
    {
        private const float CONNECTION_ATTEMPT_RETRY_DELAY = 0.2f;
        private const int MAX_CONNECTION_ATTEMPTS = 10;
        private const string IP = "127.0.0.1";

        private int ConnectionAttempts;

        private void Start() 
        {
            ENetClient.Connect(IP);
            StartCoroutine(CheckConnection());
        }

        private IEnumerator CheckConnection() 
        {
            while (!ENetClient.IsConnected()) 
            {
                ConnectionAttempts++;
                Debug.Log($"Connection attempts: {ConnectionAttempts}");

                if (ConnectionAttempts >= MAX_CONNECTION_ATTEMPTS) 
                {
                    Debug.Log("Failed after 10 connection attempts");
                    yield break;
                }

                yield return new WaitForSeconds(CONNECTION_ATTEMPT_RETRY_DELAY);
            }

            // Success
            ConnectionAttempts = 0; // Reset connection attempts / only fill it up again if client disconnects
            Debug.Log("Connection established");
        }

        public void Play()
        {
            if (ENetClient.IsConnected()) 
            {
                SceneManager.LoadScene("Main");
                ENetClient.InGame = true;
            }
        }
    }
}