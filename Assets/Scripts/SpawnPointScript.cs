using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour {

    public Vector3 center;
    public Vector2 size;
    public GameObject[] shapes;

    //графическое отображение области в которой будут создаваться объекты
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    //создание рандомных объектов в случайных местах
    void SpawnShapes()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), (Random.Range(-size.y / 2, size.y / 2)));

        GameObject shape = Instantiate(shapes[Random.Range(0, shapes.Length)], pos, Quaternion.identity);
        shape.GetComponent<ShapeScripts>().ChooseShape();

        Destroy(shape, 3f);
    }

    void Start()
    {
        InvokeRepeating("SpawnShapes", 1, 0.75f);
    }
}
