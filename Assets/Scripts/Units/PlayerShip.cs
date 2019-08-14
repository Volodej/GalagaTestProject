using UnityEngine;
using UserInput;
using Zenject;

namespace Units
{
    public class PlayerShip : MonoBehaviour
    {
        [Inject]
        public void Init(IUserInput userInput, PlayerShipSettings settings)
        {
            
        }
    }
}