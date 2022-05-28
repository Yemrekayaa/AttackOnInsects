using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private Tilemap colMap;
    [SerializeField]
    private List<Variation> packs;
    private List<Variation> packsWithFreq;




    void Start()
    {
        GoThroughMap();
    }

    [ContextMenu("Make Map  ")]
    private void GoThroughMap()
    {
        CreatePacksWithFreq();

        map.CompressBounds();
        colMap.ClearAllTiles();
        Vector3Int bottomLeft = map.origin;
        Vector3Int topRight = map.origin + map.size;

        for (int x = bottomLeft.x; x < topRight.x; x++)
        {
            for (int y = bottomLeft.y; y < topRight.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, map.origin.z);
                TileBase tile = map.GetTile(tilePosition);
                int packID = GetPackID(tile);
                if (packID > -1)
                {
                    int randomTileNumber = Random.Range(0, packsWithFreq[packID].tiles.Count);
                    TileBase newTile = packsWithFreq[packID].tiles[randomTileNumber];
                    map.SetTile(tilePosition, newTile);
                    if (randomTileNumber > 156)
                        colMap.SetTile(tilePosition, packsWithFreq[packID].tiles[0]);
                }
            }


            //map.SetTile(tilePosition,TestTile);
        }
    }

    [ContextMenu("Clear Col")]
    private void ClearCol()
    {
        colMap.ClearAllTiles();
    }

    private int GetPackID(TileBase tile)
    {
        int id = -1;

        for (int i = 0; i < packs.Count; i++)
        {
            foreach (var packedTile in packs[i].tiles)
            {
                if (tile == packedTile)
                    return i;
            }
        }

        return id;
    }


    private void CreatePacksWithFreq()
    {
        packsWithFreq = new List<Variation>();

        for (int i = 0; i < packs.Count; i++)
        {
            Variation newPack = new Variation();
            newPack.tiles = new List<TileBase>();

            for (int j = 0; j < packs[i].tiles.Count; j++)
            {
                for (int k = 0; k < packs[i].freq[j]; k++)
                {
                    newPack.tiles.Add(packs[i].tiles[j]);
                }
            }

            packsWithFreq.Add(newPack);
        }
    }
}
