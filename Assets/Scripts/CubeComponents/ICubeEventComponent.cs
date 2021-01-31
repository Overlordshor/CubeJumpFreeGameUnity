public interface ICubeEventComponent
{
    public void Cube_OnCompressedCube();

    public void Cube_OnHitCube();

    public void Cube_OnJumped();

    public void Cube_OnFellGround();

    public void SubscribeOnEvent();
}