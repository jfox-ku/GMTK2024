using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GameStateReference : BaseReference<GameState, GameStateVariable>
	{
	    public GameStateReference() : base() { }
	    public GameStateReference(GameState value) : base(value) { }
	}
}