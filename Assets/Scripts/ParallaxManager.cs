using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer //clase con datos de cada background
    {
        public Transform background;
        public float ParallaxFactor; //cuanto quiero mover el background
    }

    public ParallaxLayer[] layers; //array de todos los background

    public Transform camTransform;
    private Vector3 cameraLastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraLastPosition = camTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraDelta = camTransform.position - cameraLastPosition; //diferencia de posicion actual y ultima posicion de camara
        foreach (ParallaxLayer layer in layers) //mover background la diferencia de posicion de camara por el factor parallax
        {
            float moveX = cameraDelta.x * layer.ParallaxFactor;
            float moveY = cameraDelta.y * layer.ParallaxFactor;

            layer.background.position += new Vector3(moveX, moveY, 0f);
        }
        cameraLastPosition = camTransform.position; //actualizar ultima posicion
    }
}
