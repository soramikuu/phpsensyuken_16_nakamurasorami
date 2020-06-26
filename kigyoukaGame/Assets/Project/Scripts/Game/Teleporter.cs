using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter exitTeleporter;
    //テレポート間の無限ループを回避するための値
    public float exitOffset = 2f;

    void OnTriggerEnter(Collider otherCollider)
    {
        //if (otherCollider.GetComponent<Player>() != null) {
        //    if (exitTeleporter != null)
        //    {
        //        Player player = otherCollider.GetComponent<Player>();
        //        //player.Teleport(exitTeleporter.transform.position + exitTeleporter.transform.forward * exitOffset);
        //        player.transform.position = exitTeleporter.transform.position
        //            + exitTeleporter.transform.forward * exitOffset;
        //    }
        //}

        if (otherCollider.GetComponent<Player>() != null)
        {
            if (exitTeleporter != null)
            {
                Player player = otherCollider.GetComponent<Player>();
                player.Teleport(exitTeleporter.transform.position + exitTeleporter.transform.forward * exitOffset);
            }
        }



    }
}
