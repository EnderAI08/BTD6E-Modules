using Assets.Scripts.Models.Powers;

namespace Ultra_Powers.PowerAdapters;
internal abstract class IPowerAdapter {
    internal abstract void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets);
    internal abstract void ModifyPower(ref PowerModel power);
}
