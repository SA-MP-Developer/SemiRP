﻿using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP
{
    public static class Constants
    {
        public const float PROXIMITY_RADIUS = 15.0f;
        public const float PROXIMITY_SHOUT_FACTOR = 1.5f;
        public const float PROXIMITY_WHISPER = 5.0f;

        public const int PASSWORD_MAX_ATTEMPTS = 3;
        public const int MAX_CHARACTERS = 2;

        public const int CHARACTER_INVENTORY_SIZE = 50;

        public static class Chat
        {
            public static Color ADMIN_TAG = Color.Red;
            public static Color INFO_TAG = Color.LemonChiffon;
            public static Color SMS_TAG = Color.YellowGreen;
            public static Color TEL_TAG = Color.DarkOrange;
            public static Color ERROR_TAG = Color.DarkRed;
            public static Color HELP_TAG = Color.LightSkyBlue;

            public static Color HIGHLIGHT = Color.LightSkyBlue;

            public static Color ME = Color.DeepPink;

            public static Color PM = Color.Yellow;
            public static Color ADMIN_PM = Color.Red;
        }
    }
}
