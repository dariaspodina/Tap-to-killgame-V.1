using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScripts : MonoBehaviour {

    public bool isEnemy;

    // создание объектов красного цвета (enemy)
    public void MakeEnemyShape()
    {
        isEnemy = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // создание объектов зеленого цвета
    public void MakeFriendlyShape()
    {
        isEnemy = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    //рандомное присваивание цветов формам
    public void ChooseShape()
    {
        int c = Random.Range(0, 4);
        if (c == 0)
        {
            MakeEnemyShape();
        }
        else
        {
            MakeFriendlyShape();
        }
    }

}
