using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "GameStateGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "GameState",
	    order = 120)]
	public sealed class GameStateGameEvent : GameEventBase<GameState>
	{
	}
}