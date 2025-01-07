using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Assets")]
public class EquipmentAssets : ScriptableObject
{
    // Head and Helmets
    public Mesh[] heads;
    public Mesh[] hairs;
    public Mesh[] closedHelmets;
    public Mesh[] openHelmets;
    public Mesh[] helmetAttachmentsHeadRootBones;
    public Mesh[] helmetAttachmentsSpineRootBones;
    public Mesh[] hats;

    // Shoulder
    public Mesh[] shoulderLefts;
    public Mesh[] shoulderRights;

    // Chest Armor
    public Mesh[] chestArmors;
    public Mesh[] upperRightArms;
    public Mesh[] upperLeftArms;
    public Mesh[] elbowRightAttachments;
    public Mesh[] elbowLeftAttachments;

    // Cape/Cloak
    public Mesh[] capeAttachments;
    public Mesh[] backpackAttachments;

    // Gloves
    public Mesh[] lowerRightArms;
    public Mesh[] lowerLeftArms;
    public Mesh[] rightHands;
    public Mesh[] leftHands;
    
    // Pants
    // Broken Hips Meshes: 26, 4, 9, 6, 12
    public Mesh[] hips;
    public Mesh[] kneeAttachmentRights;
    public Mesh[] kneeAttachmentLefts;

    // Boots
    public Mesh[] leftLegs;
    public Mesh[] rightLegs;
}
