namespace GodlyTowers.Util;
// WIP
[RegisterTypeInIl2Cpp]
public class ChromaBackground : MonoBehaviour {
    private Image image;
    private int tick;

    public ChromaBackground(IntPtr obj0) : base(obj0) {
        ClassInjector.DerivedConstructorBody(this);
    }

    public ChromaBackground() : base(ClassInjector.DerivedConstructorPointer<ChromaBackground>()) { }

    public void Start() {
        if (image == null)
            foreach (var potIm in gameObject.GetComponents<Image>()) {
                if (potIm.overrideSprite != null && potIm.overrideSprite.texture.width == 213 && potIm.overrideSprite.texture.height == 174) {
                    image = potIm;
                    break;
                }
            }
    }

    public void Update() {
        if (image == null)
            foreach (var potIm in gameObject.GetComponents<Image>()) {
                if (potIm.overrideSprite != null && potIm.overrideSprite.texture.width == 213 && potIm.overrideSprite.texture.height == 174) {
                    image = potIm;
                    break;
                }
            }
        var text = image.overrideSprite.texture;
        var pixels = text.GetPixels32(); 

        for (int x = 0; x < 213; x++) {
            for (int y = 0; y < 174; y++) {
                long dif = (213 - x * 10) - (174 - y * 10);
                var col = HSL2RGB(tick - dif, 0.8, 0.5);
                pixels[x + (y * 213)] = Color32.Lerp(col, pixels[x + (y * 213)], 0.5f);
            }
        }

        text.SetAllPixels32(pixels, 0);
        image.overrideSprite = GodTier.GodTier.LoadSprite(text);

        tick = (tick + 1) % 500;
    }

    public static Color32 HSL2RGB(double h, double sl, double l) {
        double v;
        double r, g, b;

        r = l;   // default to gray
        g = l;
        b = l;
        v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

        if (v > 0) {
            double m;
            double sv;
            int sextant;
            double fract, vsf, mid1, mid2;

            m = l + l - v;
            sv = (v - m) / v;
            h *= 6.0;
            sextant = (int)h;
            fract = h - sextant;
            vsf = v * sv * fract;
            mid1 = m + vsf;
            mid2 = v - vsf;

            switch (sextant) {
                case 0:
                    r = v;
                    g = mid1;
                    b = m;
                    break;
                case 1:
                    r = mid2;
                    g = v;
                    b = m;
                    break;
                case 2:
                    r = m;
                    g = v;
                    b = mid1;
                    break;
                case 3:
                    r = m;
                    g = mid2;
                    b = v;
                    break;
                case 4:
                    r = mid1;
                    g = m;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = m;
                    b = mid2;
                    break;
            }
        }

        return new Color32(Convert.ToByte(r * 255.0f), Convert.ToByte(g * 255.0f), Convert.ToByte(b * 255.0f), 255);
    }
}
