using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void TouchItem(PlayerControl player);
    void GetItem(PlayerControl player);
}
public interface INextLevel
{
    void Next(PlayerControl player);
}
public interface ICameraPos
{
    void PosChange(PlayerControl player);
}
