using System.Drawing;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private Transform[] Points;
    [SerializeField] private float _speed = 3f;
    private int _pointIndex = 0; 


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.eulerAngles += new Vector3(0,0,3f);
        transform.position = Vector2.MoveTowards(transform.position, Points[_pointIndex].position, _speed * Time.deltaTime);  
        if(Vector2.Distance(transform.position, Points[_pointIndex].position) <=0.1f)
        {
            _pointIndex++;
        }
        if (_pointIndex == Points.Length) { 
            _pointIndex = 0;
        }
    }

}
