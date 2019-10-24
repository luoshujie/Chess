namespace Script.FSM
{
    /// <summary>
    /// 转换状态
    /// </summary>
    public enum Transition
    {
        NullTransition = 0,
        MoveToTarget,
        Attack,
        SeekTarget
    }
}