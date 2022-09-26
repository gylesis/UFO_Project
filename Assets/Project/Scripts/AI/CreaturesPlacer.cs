﻿using UnityEngine;

namespace Project.AI
{
    public class CreaturesPlacer
    {
        public CreaturesPlacer(Transform transform, PolarVector pos, CoordinatesService coordinatesService)
        {
            coordinatesService.SetPolarCoords(transform, pos);
            coordinatesService.SetRadius(transform, pos.Radius);
        }
    }
}