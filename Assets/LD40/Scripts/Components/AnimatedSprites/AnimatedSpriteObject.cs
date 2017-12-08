using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Kittea Party/Animated Sprite")]
public class AnimatedSpriteObject : ScriptableObject {
    public float framesPerSecond;
    public Sprite[] frames;
}
