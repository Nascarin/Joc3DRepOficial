using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tile; // Tile normal
    public GameObject tileRight; // Tile de giro a la derecha
    public GameObject tileLeft; // Tile de giro a la izquierda
    public GameObject[] obstacleTiles; // Array de tiles con obstáculos
    public GameObject referenceObject; // Objeto de referencia para la posición de los tiles
    public float timeOffset = 0.4f; // Tiempo de espera entre la generación de tiles
    public float distanceBetweenTiles = 5.0f; // Distancia entre tiles
    public float randomValue = 0.8f; // Valor aleatorio para determinar si se generará un tile de giro
    public float obstacleSpawnProbability = 0.2f; // Probabilidad de que un tile tenga obstáculos
    public Vector3 previousTilePosition; // Posición del tile anterior
    private float startTime; // Tiempo de inicio
    private Vector3 mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0); // Direcciones de los tiles
    public float tileAngle = 0.0f; // Ángulo de rotación del tile
    private bool turnNext = false; // Determina si el siguiente tile será un giro
    public bool obstaclePassed;
    public int tileId;
    public int ordTile;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la posición del tile anterior y el tiempo de inicio
        //previousTilePosition = referenceObject.transform.position;
        previousTilePosition = new Vector3(189.71f, 5.46f, 0.3f);
        startTime = Time.time;
        obstaclePassed = false;
        ordTile = 0;
    }

    // Método para actualizar la dirección del tile
    public void UpdateDirection(Vector3 newMainDirection, Vector3 newOtherDirection)
    {
        // Actualizar las direcciones
        mainDirection = newMainDirection;
        otherDirection = newOtherDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // Generar un nuevo tile si ha pasado el tiempo de espera
        if (Time.time - startTime > timeOffset)
        {

            GameObject spawnedTile;

            // Determina si se generará un tile normal o uno con obstáculos segun si hay un giro o no y la probabilidad
            if (!turnNext && Random.value < obstacleSpawnProbability)
            {
                // Instancia un tile con obstáculos usando un switch
                int randomIndex = Random.Range(0, obstacleTiles.Length);
                obstaclePassed = true;
                tileId = 2;
                switch (randomIndex)
                {
                    // Para cada caso, instanciar un tile con obstáculos y rotarlo
                    case 0:
                        spawnedTile = Instantiate(obstacleTiles[0], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 1:
                        spawnedTile = Instantiate(obstacleTiles[1], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 2:
                        spawnedTile = Instantiate(obstacleTiles[2], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 3:
                        spawnedTile = Instantiate(obstacleTiles[3], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 4:
                        spawnedTile = Instantiate(obstacleTiles[4], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 5:
                        spawnedTile = Instantiate(obstacleTiles[5], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 6:
                        spawnedTile = Instantiate(obstacleTiles[6], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    case 7:
                        spawnedTile = Instantiate(obstacleTiles[7], previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                    default:
                        // En caso de que el índice esté fuera del rango del array
                        spawnedTile = Instantiate(tile, previousTilePosition, Quaternion.identity);
                        spawnedTile.transform.Rotate(0, tileAngle, 0);

                        break;
                }
            }
            else
            {
                // Instancia un tile normal
                if (!turnNext)
                {
                    obstaclePassed = false;
                    GameObject spawnedTil = Instantiate(tile, previousTilePosition, Quaternion.identity);
                    spawnedTil.transform.Rotate(0, tileAngle, 0);
                    tileId = 0;
                    ++ordTile;
                }
                else
                {
                    obstaclePassed = true;
                    // Instancia un tile de giro
                    GameObject tileTSpawn = turnNext ? (mainDirection == Vector3.right ? tileRight : tileLeft) : tile;
                    GameObject spawnedTil = Instantiate(tileTSpawn, previousTilePosition, Quaternion.identity);
                    //spawnedTile.transform.Rotate(0, tileAngle, 0);
                    tileId = 1;
                }
            }
            // Calcular la siguiente posición del tile
            Vector3 nextPosition = previousTilePosition + distanceBetweenTiles * mainDirection;
            // Determinar si el siguiente tile será un giro
            turnNext = Random.value > randomValue;
            // Determinar el tile a instanciar
            GameObject tileToSpawn = turnNext ? (mainDirection == Vector3.right ? tileRight : tileLeft) : tile;
            // Rotar el tile según la dirección
            if (turnNext && tileToSpawn == tileRight)
            {
                tileAngle += +90.0f;
            }
            else if (turnNext && tileToSpawn == tileLeft)
            {
                tileAngle += -90.0f;
            }
            // Rotar el tile según la dirección
            if (turnNext)
            {
                Vector3 temp = mainDirection;
                mainDirection = otherDirection;
                otherDirection = temp;
            }

            // Actualizar la posición del tile anterior
            previousTilePosition = nextPosition;
            // Actualizar el tiempo de inicio
            startTime = Time.time;
        }
    }
}
