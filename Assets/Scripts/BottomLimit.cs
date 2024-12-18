using UnityEngine;

public class BottomLimit : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag(Const.OBSTACLE_TAG)){
         Destroy(col.gameObject);
        }
   }
}
