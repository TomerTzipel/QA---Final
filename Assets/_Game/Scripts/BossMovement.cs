using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float spawnDuration;
    [SerializeField] private float maneuverDuration;
    private int _maneuverPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss.canAttack = false;
        StartCoroutine(Spawn()); 
    }
    private IEnumerator Spawn()
    {
        float timer = 0;
        while (timer < spawnDuration)
        {
            timer += Time.deltaTime;
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
            yield return null;
        }
        StartCoroutine(ManeuverCoroutine(ChooseManeuverDirection()));
        boss.canAttack = true;
    }

    private IEnumerator ManeuverCoroutine(Vector3 direction)
    {
        _maneuverPosition = direction == Vector3.left ? _maneuverPosition - 1 : _maneuverPosition + 1;

        float timer = 0;
        while (timer < maneuverDuration)
        {
            timer += Time.deltaTime;
            transform.Translate(moveSpeed * Time.deltaTime * direction);
            yield return null;
        }

        StartCoroutine(ManeuverCoroutine(ChooseManeuverDirection()));
    }

    private Vector3 ChooseManeuverDirection()
    {
        if(_maneuverPosition > 0) { return Vector3.left; }
        if (_maneuverPosition < 0) { return Vector3.right; }

        return Random.Range(0,99) % 2 == 0 ? Vector3.left : Vector3.right;
    }
}
