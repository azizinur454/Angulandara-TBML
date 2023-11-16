using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent
{
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, string> characterHealed;
    public static UnityAction<GameObject, int> scores;
}
