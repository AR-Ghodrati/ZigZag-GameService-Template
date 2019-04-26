﻿using FiroozehCorp.ZigZagGame.scripts.game.ZigZag;
using Newtonsoft.Json;
using UnityEngine;

namespace FiroozehCorp.ZigZagGame.scripts.data {
	
	namespace ZigZag {

		/// <summary>
		/// The progress manager is the class the game may access to modify Google Player User Data..
		/// .. such as leaderboards, achievements, and events
		/// </summary>
		public class ProgressManager : MonoBehaviour {

			public static ProgressManager Instance;
			

			public int Attempts => DataStorage.GetLocalData(GPGSIds.event_attempts);
			public static int HighScore => DataStorage.GetLocalData(GPGSIds.leaderboard_high_score);
			public int Score => score;
			

			public bool AudioOn { 
				get { 
				#if UNITY_WEBGL
					return true;
				#endif
					return DataStorage.GetLocalData("Audio") == 1 ? true : false; 
				} 
			}

			int score = 0;

			/// <summary>
			/// Singleton pattern. Only one Progress Manager allowed
			/// </summary>
			void Awake() {
				if (Instance != null) {
					Destroy(gameObject);
				}
				else {
					Instance = this;
					DontDestroyOnLoad(gameObject);
				}
			}

			/// <summary>
			/// Saves audio state locally
			/// </summary>
			public void ToggleAudio()
			{
				DataStorage.SaveLocalData("Audio", AudioOn ? 0 : 1);
			}
			
			/// <summary>
			/// Reset score
			/// </summary>
			public void ResetScore() {
				
				SessionManager.GameService?.SaveGame(
					"ZigZagGame"
					,"ZigZagGameSave"
					,null
					,new Save {Attempts = Attempts, HighScore = HighScore, Score = Score}
					,success => {},error => {}
				);
				
				score = 0;
			}

			/// <summary>
			/// Add to current session's score, and report to the leaderboard if a new high score is reached
			/// Determine if an achievement should be unlocked
			/// </summary>
			public void AddScore(int value) {
				score += value;
				if (score > HighScore) {
					DataStorage.ReportLeaderboardScore(GPGSIds.leaderboard_high_score, (uint)score);
				}
						
				CheckScoreAchievementUnlock();
				
			}

			/// <summary>
			/// Add to number of session attempts and save the value
			/// </summary>
			public void IncrementAttempts() {
				DataStorage.IncrementEvent(GPGSIds.event_attempts, 1);
			}

			/// <summary>
			/// Unlock achievement
			/// </summary>
			void CheckScoreAchievementUnlock() {
				if (score >= 5000) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_feared_tapper);
				}
				if (score >= 1000) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_respected_tapper);
				}
				if (score >= 500) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_skilled_tapper);
				}
				if (score >= 250) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_experienced_tapper);
				}
				if (score >= 100) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_apprentice_tapper);
				}
				if (score >= 50) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_novice_tapper);
				}
				if (score >= 10) {
					DataStorage.UnlockAchievement(GPGSIds.achievement_starter_tapper);
				}
			}
			
		}
	}
}