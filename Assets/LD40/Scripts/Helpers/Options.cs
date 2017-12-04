using UnityEngine;

partial class Helpers
{
    public enum MovementType
    {
        WASD,
        TowardsMouse
    }

    const string KEY_MOVEMENTTYPE = "MovementType";
    public static MovementType GetOptionsMovementType()
    {
        if (!PlayerPrefs.HasKey(KEY_MOVEMENTTYPE))
            return MovementType.WASD;

        return PlayerPrefs.GetInt(KEY_MOVEMENTTYPE) == 1 ? MovementType.TowardsMouse : MovementType.WASD;
    }

    public static void SetOptionsMovementType(MovementType movementType)
    {
        PlayerPrefs.SetInt(KEY_MOVEMENTTYPE, movementType == MovementType.TowardsMouse ? 1 : 0);
    }
}
