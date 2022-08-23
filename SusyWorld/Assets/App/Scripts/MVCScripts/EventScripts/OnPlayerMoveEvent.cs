using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DynamicBox.EventManagement.Speed
{
    public class OnPlayerMoveEvent : GameEvent
    {
        public Text Speed;

        public OnPlayerMoveEvent(Text speed)
        {
            Speed = speed;
        }
    }
}
