using UnityEngine;

namespace UI.PositionSetters
{
    public static class ScreenSizeCalculator
    {
        public static float GetSize(ScreenSizeCalculateMode screenSizeCalculateMode)
        {
            float size = 0;

            if (screenSizeCalculateMode == ScreenSizeCalculateMode.heightMinusWidth) size = Screen.height - Screen.width;
            if (screenSizeCalculateMode == ScreenSizeCalculateMode.widthMinusHeight) size = Screen.width - Screen.height;
            if (screenSizeCalculateMode == ScreenSizeCalculateMode.heightPlusWidth) size = Screen.height + Screen.width;
            if (screenSizeCalculateMode == ScreenSizeCalculateMode.widthPlusHeight) size = Screen.width + Screen.height;

            return size;
        }
    }
}