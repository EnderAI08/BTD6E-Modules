namespace BTD6E_Module_Helper {
    internal static class NumberExtensions {
        public static float Map(this float value, float fromLow, float fromHigh, float toLow, float toHigh) {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static int Map(this int value, int fromLow, int fromHigh, int toLow, int toHigh) {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }
    }
}
