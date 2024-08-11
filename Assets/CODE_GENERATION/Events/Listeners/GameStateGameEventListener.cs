using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "GameState")]
	public sealed class GameStateGameEventListener : BaseGameEventListener<GameState, GameStateGameEvent, GameStateUnityEvent>
	{
	}
}