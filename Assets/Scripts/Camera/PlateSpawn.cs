using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlateSpawn : MonoBehaviour
{
    public GameObject plate;
    public SpawnTile spawnTile;
    private bool tileChange;
    private int ordAnTile;
    // Start is called before the first frame update
    void Start()
    {
        tileChange = true;
        ordAnTile = -1;
    }

    // Update is called once per frame
    void Update()
    {
        float probVal = Random.Range(0f, 100f);

        // We will put in plates in the 40% of cases.
        if (probVal > 59f) {


            if (ordAnTile < spawnTile.ordTile)
            {
                if (spawnTile.tileId == 0 || spawnTile.tileId == 1) // Normal Tile or Lateral Holes
                {
                    int probSide = Random.Range(0,2);
                    float side = 0;
                    if (probSide == 0) side = 0;
                    else if (probSide == 1) side = 5;
                    else if (probSide == 2) side = -5;
                    GameObject pCoin1 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin1.transform.Translate(new Vector3(side, 3, 0), Space.Self);

                    GameObject pCoin2 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin2.transform.Translate(new Vector3(side, 3, -4), Space.Self);

                    GameObject pCoin3 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin3.transform.Translate(new Vector3(side, 3, 4), Space.Self);

                    GameObject pCoin4 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin4.transform.Translate(new Vector3(side, 3, -8), Space.Self);

                    GameObject pCoin5 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin5.transform.Translate(new Vector3(side, 3, 8), Space.Self);
                }
                else if (spawnTile.tileId == 4) { // Panzer On Right
                    GameObject pCoin1 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin1.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin1.transform.Translate(new Vector3(-4, 3, 0), Space.Self);

                    GameObject pCoin2 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin2.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin2.transform.Translate(new Vector3(-4, 3, -4), Space.Self);

                    GameObject pCoin3 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin3.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin3.transform.Translate(new Vector3(-4, 3, 4), Space.Self);

                    GameObject pCoin4 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin4.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin4.transform.Translate(new Vector3(-4, 3, -8), Space.Self);

                    GameObject pCoin5 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin5.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin5.transform.Translate(new Vector3(-4, 3, 8), Space.Self);
                }
                else if (spawnTile.tileId == 5) // T34 On Left
                {
                    GameObject pCoin1 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin1.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin1.transform.Translate(new Vector3(-4, 3, 0), Space.Self);

                    GameObject pCoin2 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin2.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin2.transform.Translate(new Vector3(-4, 3, -4), Space.Self);

                    GameObject pCoin3 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin3.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin3.transform.Translate(new Vector3(-4, 3, 4), Space.Self);

                    GameObject pCoin4 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin4.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin4.transform.Translate(new Vector3(-4, 3, -8), Space.Self);

                    GameObject pCoin5 = Instantiate(plate, spawnTile.previousTilePosition, Quaternion.identity);
                    pCoin5.transform.Rotate(0, spawnTile.tileAngle, 0);
                    pCoin5.transform.Translate(new Vector3(-4, 3, 8), Space.Self);
                }
            }
        }
        ordAnTile = spawnTile.ordTile;
    }
}
