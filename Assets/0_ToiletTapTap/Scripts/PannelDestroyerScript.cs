using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    class PannelDestroyerScript : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Pannel" || col.tag == "pannelBlank")
                Destroy(col.gameObject.transform.parent.gameObject); //free up some memory
        }
    }

