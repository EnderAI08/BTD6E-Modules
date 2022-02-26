namespace GodlyTowers.Util;
internal static class GlobalTowerIndex {
    private static int m_index = 32;
    public static int Index { get { return m_index++; } }
}
