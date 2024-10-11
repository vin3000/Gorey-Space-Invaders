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

//skapa subklasser av zombier. Varje klass ska innehålla zombiens actions. Som Invader skripten.
/* Liten snabb
 * Långsam skjutare
 * Explosiv
 * Boss
 */