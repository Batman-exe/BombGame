using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRecieverSpawner : MonoBehaviour
{
    //Lista de los conectores para los cables
    [SerializeField] private List<GameObject> wiresR;

    private void Start()
    {

        Vector3 position = new Vector3(-6.46f, 2.7f, 0);

        //Para no repetir cables se eliminan los que se van usando y se le agrega espaciado entre los cables en Y
        while (wiresR.Count-1 >= 0)
        {
            int pos = Random.Range(0, wiresR.Count);

            var wire = Instantiate(wiresR[pos], position,wiresR[pos].transform.rotation);
            wire.transform.parent =  gameObject.transform;
            position -= new Vector3(0f, 2.2f, 0f);
            wiresR.RemoveAt(pos);

        }

        //Para que queden en la posicion opuesta a el inicio del cable
        transform.localScale = new Vector3(-1, 1, 1);


    }




}
