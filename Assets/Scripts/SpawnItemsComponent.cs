using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemsComponent : MonoBehaviour
{
    [SerializeField] private float sectorAngle=90f;
    [SerializeField] private float rotateAngle;
    [SerializeField] private float dropDelay = 1f;
    [SerializeField] private float force = 5;
    [SerializeField] private int itemPerBurst = 1;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip DropSound;
    private Coroutine coroutine;
    public void StartDrop(GameObject[] items)
    {
        StopCurrentCoroutine();
        StartCoroutine(BurstDrop(items));
    }
    private void StopCurrentCoroutine()
    {
        if (coroutine != null) StopCoroutine(coroutine);
    }
    private IEnumerator BurstDrop(GameObject[] drop)
    {
        for (int i = 0; i < drop.Length; i++)
        {
            for (int j = 0; j < itemPerBurst && i < drop.Length; j++)
            {
                if (j > 0) i++;
                Spawn(drop[i]);
                if (DropSound != null)
                {
                    source.PlayOneShot(DropSound);
                }
            }
            yield return new WaitForSeconds(dropDelay);
        }
    }
    private void Spawn(GameObject item)
    {
        var instanse = Instantiate(item, transform.position, Quaternion.identity);
        var rigidbody = instanse.GetComponent<Rigidbody2D>();
        var randomAngle = Random.Range(0, sectorAngle);
        var forceVector = AngleToVector(randomAngle);
        rigidbody.AddForce(forceVector * force, ForceMode2D.Impulse);

    }
    private Vector2 AngleToVector(float angle)
    {
        var angleMiddleDelta = (180 - sectorAngle - rotateAngle) / 2;
        return CalculateVector(angleMiddleDelta+ angle);
    }
    private Vector2 CalculateVector(float angle)
    {
        var angleInRadian = angle * Mathf.PI / 180;
        var x = Mathf.Cos(angleInRadian);
        var y = Mathf.Sin(angleInRadian);
        return new Vector2(x, y);
    }
}
