using UnityEngine;
using Unity.Cinemachine;
public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineCamera vCam;
    private float _startTime;

    

    private void Update()
    {
        if(_startTime > 0)
        {
            _startTime -= Time.deltaTime;
            if(_startTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin _perlin = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
                _perlin.AmplitudeGain = 0f;
            }
        }
    }

    public void shake(float duration,float intencity)
    {
        _startTime = duration;
        //CinemachineBasicMultiChannelPerlin _perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //_perlin.AmplitudeGain = intencity;
       CinemachineBasicMultiChannelPerlin _perlin = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        _perlin.AmplitudeGain = intencity;
        
    }
}
