using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer //background data
    {
        public Transform background;
        public float ParallaxFactor; 
    }

    public ParallaxLayer[] layers; 

    public Transform camTransform;
    private Vector3 cameraLastPosition;
    void Start()
    {
        cameraLastPosition = camTransform.position;
    }

    void LateUpdate() //move background to follow player based on parallaxFactor
    {
        Vector3 cameraDelta = camTransform.position - cameraLastPosition; 
        foreach (ParallaxLayer layer in layers) 
        {
            float moveX = cameraDelta.x * layer.ParallaxFactor;
            float moveY = cameraDelta.y * layer.ParallaxFactor;

            layer.background.position += new Vector3(moveX, moveY, 0f);
        }
        cameraLastPosition = camTransform.position; 
    }
}
