using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class GameStateEvent : UnityEvent<GameState> { }

	[CreateAssetMenu(
	    fileName = "GameStateVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GameState",
	    order = 120)]
	public class GameStateVariable : BaseVariable<GameState, GameStateEvent>
	{
	}
}