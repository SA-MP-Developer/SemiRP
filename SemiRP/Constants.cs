using SampSharp.GameMode.SAMP;
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

        public const int CHARACTER_INVENTORY_SIZE = 10;

        public const int SAVE_TIMER = 2000;

        public const float PLAYER_LABEL_DIST = 12.5f;

        public static class Vehicle
        {
            public static float SPEED_MAGIC = 181.5f;
            public static float LOCK_RANGE = 3.0f;
            public static float HOOD_RANGE = 4.0f;
            public static float STOPPED_CONSUMPTION_FACTOR = 0.00001f;

            public static int MS500_TIMER = 500;
        }

        public static class Chat
        {
            public static float CHAT_BUBBLE_ME_RANGE = 10.0f;
            public static float CHAT_BUBBLE_TALK_RANGE = 15.0f;

            public static float RANGED_CHAT_FINAL_COLOR_PERC = 0.5f;

            public static float CHAT_BUBBLE_TIME_FACTOR = 2.0f;

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
        public static class Item
        {
            public static float PROXIMITY_RADIUS = 3.0f;
            public static float PHONE_LABEL_DISTANCE = 15f;
            public static float PHONE_LABEL_DISTANCE_FROM_PHONE = 0.2f;
        }
    }
}
