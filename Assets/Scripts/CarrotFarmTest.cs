using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotFarmTest : MonoBehaviour
{
    public float _carrotCreateTime = 1f;
    private float _t = 0;

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        if( _t > _carrotCreateTime )
        {
            _t = Mathf.Repeat(_t, _carrotCreateTime);
            GameManager.Instance.AddCarrot(1);
        }
    }
}
