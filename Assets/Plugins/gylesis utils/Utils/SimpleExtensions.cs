using UnityEngine;
using static UnityEngine.Screen;

namespace Warlords.Utils
{
    public static class SimpleExtensions
    {
        public static float DistanceToUpScreenBorder(this Vector2 pos)
        {
            return (pos - new Vector2(pos.x, height)).magnitude;
        }
        
        public static float DistanceToDownScreenBorder(this Vector2 pos)
        {
            return (pos - new Vector2(pos.x, 0)).magnitude;
        }
        
        public static float DistanceToRightScreenBorder(this Vector2 pos)
        {
            return (pos - new Vector2(width, pos.y)).magnitude;
        }
        
        public static float DistanceToLeftScreenBorder(this Vector2 pos)
        {
            return (pos - new Vector2(0, pos.y)).magnitude;
        }

        public static void SetPos(this RectTransform rectTransform, Vector2 pos)
        {
            float distanceToRightScreenBorder = pos.DistanceToRightScreenBorder();
            float distanceToLeftScreenBorder = pos.DistanceToLeftScreenBorder();
            float distanceToUpScreenBorder = pos.DistanceToUpScreenBorder();
            float distanceToDownScreenBorder = pos.DistanceToDownScreenBorder();

            float offsetX = 5;
            float offsetY;

            float rectWidth = rectTransform.rect.width;
            float rectHeight = rectTransform.rect.height;

            bool rightSided = pos.x > width / 2;
            bool upperSided = pos.y > height / 2;

            Vector2 pivot = Vector2.zero;

            Vector2 downScreenBorder = new Vector2(pos.x, 0);
            Vector2 upperScreenBorder = new Vector2(pos.x, height);

            if (rightSided)
            {
                if (rectWidth > distanceToRightScreenBorder)
                {
                    pivot = Vector2.right;
                    offsetX *= -1;
                }
            }
            else
            {
                if (rectWidth > distanceToLeftScreenBorder)
                {
                    pivot = Vector2.zero;
                    offsetX *= 1;
                }
            }

            if (upperSided)
            {
                if (rectHeight > distanceToUpScreenBorder)
                {
                    pivot.y += 1;
                }

                offsetY = (upperScreenBorder - rectTransform.rect.center).magnitude;
            }
            else
            {
                offsetY = (downScreenBorder - rectTransform.rect.center).magnitude;
            }
            
            
            var movePos = pos;
            movePos.x += offsetX;
            //    movePos.y += offsetY;

            rectTransform.pivot = pivot;
            rectTransform.position = movePos;
        }
        
        
    }
}