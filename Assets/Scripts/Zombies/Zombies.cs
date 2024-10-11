using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    protected float speed = 10f;
    protected float damage = 10f;
    protected float health = 10f;

    protected bool isShooter = false;
    protected bool isExplosive = false;
}

//skapa subklasser av zombier. Varje klass ska inneh�lla zombiens actions. Som Invader skripten.
/* Liten snabb
 * L�ngsam skjutare
 * Explosiv
 * Boss
 */