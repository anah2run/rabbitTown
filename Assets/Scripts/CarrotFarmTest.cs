using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotFarmTest : MonoBehaviour
{
    public float _carrotCreateTime = 1f;
    private float _t = 0;
    public int _addCount = 1;

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        if( _t > _carrotCreateTime )
        {
            _t = Mathf.Repeat(_t, _carrotCreateTime);
            var obj = ObjectPooler.Instance.GetPooledObject();
            var effect = obj.GetComponent<ResourceGainEffect>();
            effect.spawnPoint = transform.position;
            effect.ShowResourceGain(_addCount);
            GameManager.Instance.AddCarrot(_addCount);
        }
    }
}
