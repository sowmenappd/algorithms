using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SRLibs {

  public struct Coord2D {
    public int x, y;
    public Coord2D (int x, int y) {
      this.x = x;
      this.y = y;
    }

  }

  public enum FillMode { four, eight }

  public static class Utility {

    ///<summary>
    ///performs a floodfill algorithm on the given map
    ///returns a list of tile coordinates available in the same region as the starting coordinate
    ///value 0 = empty, value 1 = obstacle 
    ///</summary>
    public static List<Coord2D> FloodFill (int[ , ] map, FillMode fill = FillMode.eight, Coord2D center = new Coord2D ()) {
      int width = map.GetLength (1);
      int height = map.GetLength (0);

      List<Coord2D> regionTiles = new List<Coord2D>();

      List<Coord2D> checkList = new List<Coord2D> ();
      bool[, ] marked = new bool[height,width];
      for (int i = 0; i < height; i++) {
        for (int j = 0; j < width; j++) {
          marked[i,j] = false;
        }
      }

      if (map[center.y, center.x] == 1){
        //regionTiles.Add(new Coord(center.x, center.y));
        return regionTiles;
      }
      else {
        marked[center.y, center.x] = true;
      }

      checkList.Add (center);

      while (checkList.Count > 0) {
        Coord2D tile = checkList[0];
        checkList.RemoveAt (0);
        regionTiles.Add(tile);

        int neighbourX, neighbourY;

        for (neighbourX = -1; neighbourX <= 1; neighbourX++) {
          for (neighbourY = -1; neighbourY <= 1; neighbourY++) {
            int surroundTileX = tile.x + neighbourX;
            int surroundTileY = tile.y + neighbourY;

            if (surroundTileX < 0 || surroundTileX >= width || surroundTileY < 0 || surroundTileY >= height ||
              (surroundTileX == tile.x && surroundTileY == tile.y)) {
              continue;
            }

            if (fill == FillMode.four) {
              if ((neighbourX == -1 || neighbourX == 1) && (neighbourY == -1 || neighbourY == 1)) {
                continue;
              }
            }
            if (map[surroundTileY, surroundTileX] == 0 && !marked[surroundTileY, surroundTileX]) {
              Coord2D temp = new Coord2D (surroundTileX, surroundTileY);
              checkList.Add (temp);
              marked[surroundTileY, surroundTileX] = true;
            }
          }
        }

      }

      return regionTiles;
    }

  }
}