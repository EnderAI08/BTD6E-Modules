namespace GodlyTowers.Util;

[RegisterTypeInIl2Cpp]
public class Rotate : MonoBehaviour {
    public Rotate(IntPtr obj0) : base(obj0) {
        ClassInjector.DerivedConstructorBody(this);
    }

    public Rotate() : base(ClassInjector.DerivedConstructorPointer<Rotate>()) { }


    public void Update() {
        transform.Rotate(0, 1f, 0);
    }
}