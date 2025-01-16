

using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
#if UNITY_IPHONE
using UnityEngine.SocialPlatforms.GameCenter;
#elif UNITY_ANDROID
using GooglePlayGames;
#endif

namespace TinyStudio
{
	public class GameCenter : MonoBehaviour
	{

		//Values
		#if UNITY_IPHONE
		static ILeaderboard leaderboard_IOS;
		#elif UNITY_ANDROID
		static ILeaderboard leaderboard_Android;
		#endif
		private int highScoreInt = 1000;

		[Header("iOS")]
		public string leaderboardName_IOS = "leaderboard01";
		public string leaderboardID_IOS = "com.company.game.leaderboardname";
		[Header("Android")]
		public string leaderboardName_Android = "leaderboard01";
		public string leaderboardID_Android = "com.company.game.leaderboardname";

		[Header("Values")]
		public LeaderboardPlayer[] data;
		public bool dataIsReady = false;
		public bool isTestMode = true;

		bool gameOver = false;

		#region Standart system methods

		//Use this for initialization
		void Start()
		{
			if (!isTestMode)
			{
				#if UNITY_IPHONE
				Social.localUser.Authenticate(ProcessAuthentication);

				DoLeaderboard();
				#elif UNITY_ANDROID

				AccessLeaderboardData();
				#endif
			}
			else
			{
				int lenght = 10;
				data = new LeaderboardPlayer[lenght];

				for (int i = 0; i < lenght; i++)
				{
					LeaderboardPlayer player = new LeaderboardPlayer();
					player.position = i + 1;
					player.name = "Player " + i.ToString();
					player.score = (234 - i * 10).ToString();

					data[i] = player;
				}

				dataIsReady = true;
			}
		}

		#endregion


		#if UNITY_IPHONE
		void ProcessAuthentication(bool success)
		{
			if (success)
			{
				Debug.Log("Authenticated, checking achievements");

				// MAKE REQUEST TO GET LOADED ACHIEVEMENTS AND REGISTER A CALLBACK FOR PROCESSING THEM
				Social.LoadScores(leaderboardName_IOS, scores =>
				{
					if (scores.Length > 0)
					{
						// SHOW THE SCORES RECEIVED
						Debug.Log("Received " + scores.Length + " scores");
						string myScores = "Leaderboard: \n";

						data = new LeaderboardPlayer[scores.Length];

						for (int i = 0; i < scores.Length; i++)
						{
							IScore score = scores[i];
							myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";

							LeaderboardPlayer player = new LeaderboardPlayer();
							player.position = i + 1;
							player.name = score.userID;
							player.score = score.formattedValue;

							data[i] = player;
						}

						dataIsReady = true;

						Debug.Log(myScores);
					}
					else
						Debug.Log("No scores have been loaded.");
				});
			}
			else
				Debug.Log("Failed to authenticate with Game Center.");
		}

		#elif UNITY_ANDROID
		void AccessLeaderboardData()
		{
			leaderboard_Android = PlayGamesPlatform.Instance.CreateLeaderboard();
			leaderboard_Android.id = leaderboardID_Android;
			leaderboard_Android.LoadScores(success =>
			{
				if (success)
				{
					data = new LeaderboardPlayer[leaderboard_Android.scores.Length];
					List<string> userIds = new List<string>();
					foreach (IScore score in leaderboard_Android.scores)
					{
						userIds.Add(score.userID);
					}

					//Get data
					for (int i = 0; i < data.Length; i++)
                    {
						LeaderboardPlayer player = new LeaderboardPlayer();
						player.position = i + 1;
                        player.name = userIds[i];
						player.score = leaderboard_Android.scores[i].formattedValue;

						data[i] = player;
					}

					dataIsReady = true;
				}
				else
				{
					Debug.Log("Error retrieving leaderboard android");
				}
			});
		}

		#endif

		#region Game Center Integration

		#if UNITY_IPHONE
		/// <summary>
		/// Reports the score to the leaderboards.
		/// </summary>
		/// <param name="score">Score.</param>
		/// <param name="leaderboardID">Leaderboard I.</param>
		void ReportScore(long score, string leaderboardID)
		{
			Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
			Social.ReportScore(score, leaderboardID, success =>
			{
				Debug.Log(success ? "Reported score to leaderboard successfully" : "Failed to report score");
			});
		}

		/// <summary>
		/// Get the leaderboard.
		/// </summary>
		void DoLeaderboard()
		{
			leaderboard_IOS = Social.CreateLeaderboard();
			leaderboard_IOS.id = leaderboardID_IOS;  // YOUR CUSTOM LEADERBOARD NAME
			leaderboard_IOS.LoadScores(result => DidLoadLeaderboard(result));
		}

		/// <summary>
		/// RETURNS THE NUMBER OF LEADERBOARD SCORES THAT WERE RECEIVED BY THE APP
		/// </summary>
		/// <param name="result">If set to <c>true</c> result.</param>
		void DidLoadLeaderboard(bool result)
		{
			Debug.Log("Received " + leaderboard_IOS.scores.Length + " scores");
			foreach (IScore score in leaderboard_IOS.scores)
			{
				Debug.Log(score);
			}
			//Social.ShowLeaderboardUI();
		}
		#else
		#endif

		public void ShowLeaderboard()
		{
			#if UNITY_IPHONE && UNITY_ANDROID
			Social.ShowLeaderboardUI();
			#endif
		}

		public void ReportScore()
		{
			int score = CustomPlayerPrefs.GetInt("highScore", 0);

			#if UNITY_IPHONE
			ReportScore(score, leaderboardID_IOS);
			#elif UNITY_ANDROID
			Social.ReportScore(score, leaderboardID_Android, (bool success) => {
				// handle success or failure
			});
			#endif
		}

		#endregion
	}

	//#endif
}