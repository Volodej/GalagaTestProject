using System;
using UnityEngine;

namespace Units
{
    public class Damageable : MonoBehaviour
    {
        public event Action GotHit = () => { };

        public void Hit()
        {
            GotHit();
        }
    }
}