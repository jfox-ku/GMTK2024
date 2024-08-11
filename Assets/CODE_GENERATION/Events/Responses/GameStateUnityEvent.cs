using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GameStateUnityEvent : UnityEvent<GameState>
	{
	}
}