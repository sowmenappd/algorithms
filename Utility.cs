using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SRLibs {

  public struct Coord {
    public int x, y;
    public Coord (int x, int y) {
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
    public static List<Coord> FloodFill (int[ , ] map, FillMode fill = FillMode.eight, Coord center = new Coord ()) {
      int width = map.GetLength (1);
      int height = map.GetLength (0);

      List<Coord> regionTiles = new List<Coord>();

      List<Coord> checkList = new List<Coord> ();
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
        Coord tile = checkList[0];
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
              Coord temp = new Coord (surroundTileX, surroundTileY);
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