namespace GodlyTowers.Util;

[RegisterTypeInIl2Cpp]
public class ChromaColor : MonoBehaviour {
    private UnityDisplayNode udn;
    private int tick;

    public ChromaColor(IntPtr obj0) : base(obj0) {
        ClassInjector.DerivedConstructorBody(this);
    }

    public ChromaColor() : base(ClassInjector.DerivedConstructorPointer<ChromaColor>()) {}

    public void Start() {
        if (udn == null)
            udn = gameObject.GetComponent<UnityDisplayNode>();
    }

    public void Update() {
        if (udn == null)
            udn = gameObject.GetComponent<UnityDisplayNode>();
        if (udn.genericRenderers != null) 
            foreach (var renderer in udn.genericRenderers)
                foreach (var material in renderer.materials)
                    if (material.name.Contains("T5Mat"))
                        material.SetColor("_TOutlineColor", HSL2RGB(tick / 500f, 1, 0.5));
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
