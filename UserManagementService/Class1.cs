namespace a;
interface I1{}
interface I2 : I1 {}
class A :I2 { }
class B
{
    B()
    {
        I1 i2 = new A();
        I2 i3 = (I2)i2;
    }
}
