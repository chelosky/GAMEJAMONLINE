using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scRandomGenerator
{
    public static int GetRandomLinearCongruential(int maxValue, int minIteration = 25, int maxIteration = 100)
    {
        long seed = Random.Range(0, maxValue);

        //Needs the following parameters, which can be found on Wikipedia for different implementations
        //https://en.wikipedia.org/wiki/Linear_congruential_generator
        //multiplier
        long a = 5;
        //increment
        long c = 1;
        //modulus m (which is also the maximum possible random value)
        long m = (long)maxValue;
        for (int i = 0; i < Random.Range(minIteration, maxIteration); i++)
        {
            seed = (a * seed + c) % m;
        }
        return (int)seed;
    }

    /*
     * 
     *  (minX,maxY)
     *                       
     *                       
     *                       
     *                       
     *                                  (maxX,minY)
     * 
     */
    public static Vector3 GenerateARandomVector3(int minX,int minY, int maxX, int maxY)
    {
        int newMaxX = maxX + Mathf.Abs(minX);
        int newMaxY = maxY + Mathf.Abs(minY);
        int posX = GetRandomLinearCongruential(newMaxX) - Mathf.Abs(minX);
        int posY = GetRandomLinearCongruential(newMaxY) - Mathf.Abs(minY);
        return new Vector3(posX, posY, 0f);
    }
}
