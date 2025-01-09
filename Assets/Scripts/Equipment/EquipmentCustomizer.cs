using Unity.Hierarchy;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class EquipmentCustomizer : MonoBehaviour
{
    public static EquipmentCustomizer Instance;

    public EquipmentAssets equipmentAssets;

    // Head/Helmets/Hair/Hats
    public SkinnedMeshRenderer head;
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer closedHelmet;
    public SkinnedMeshRenderer openHelmet;
    public SkinnedMeshRenderer helmetAttachmentHeadRootBone;
    public SkinnedMeshRenderer helmetAttachmentSpineRootBone;
    public SkinnedMeshRenderer hat;

    // Shoulder
    public SkinnedMeshRenderer shoulderRight;
    public SkinnedMeshRenderer shoulderLeft;

    // Chest Armor
    public SkinnedMeshRenderer chestArmor;
    public SkinnedMeshRenderer upperRightArm;
    public SkinnedMeshRenderer upperLeftArm;

    // Cape/Cloak
    public SkinnedMeshRenderer capeAttachment;
    public SkinnedMeshRenderer backpackAttachment;

    // Gloves
    public SkinnedMeshRenderer lowerRightArm;
    public SkinnedMeshRenderer lowerLeftArm;
    public SkinnedMeshRenderer rightHand;
    public SkinnedMeshRenderer leftHand;

    // Pants
    public SkinnedMeshRenderer hips;
    public SkinnedMeshRenderer kneeAttachmentRight;
    public SkinnedMeshRenderer kneeAttachmentLeft;

    // Boots
    public SkinnedMeshRenderer leftLeg;
    public SkinnedMeshRenderer rightLeg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        DisableHairAndHeadAndHelmet();
    }

    public void Randomize()
    {
        //closedHelmet.gameObject.SetActive(true);
        //helmetAttachment.gameObject.SetActive(true);

        //closedHelmet.sharedMesh = equipmentAssets.closedHelmets[Random.Range(0, equipmentAssets.closedHelmets.Length)];
        //helmetAttachment.sharedMesh = equipmentAssets.helmetAttachments[Random.Range(0, equipmentAssets.closedHelmets.Length)];

        EquipOpenHelmet(Random.Range(0, equipmentAssets.openHelmets.Length));
        EquipShoulderArmor(Random.Range(0, equipmentAssets.shoulderRights.Length));
        EquipChestArmor(Random.Range(0, equipmentAssets.chestArmors.Length), Random.Range(0, equipmentAssets.upperRightArms.Length));
        EquipCape(Random.Range(0, equipmentAssets.capeAttachments.Length));
        EquipGloves(Random.Range(0, equipmentAssets.rightHands.Length));
        EquipLegArmor(Random.Range(0, equipmentAssets.hips.Length));
        EquipBoots(Random.Range(0, equipmentAssets.leftLegs.Length));
    }

    // Equip flow moves from Head to Boots, following the same equipment slots as WoW and other commmon RPGs
    // With Helmet/Shoulders/Chest/Cape/Gloves/Legs/Boots
    public void EquipClosedHelmet(int helmet, int attachment)
    {
        UnequipOpenHelmet();
        closedHelmet.gameObject.SetActive(true);
        //helmetAttachment.gameObject.SetActive(true);

        closedHelmet.sharedMesh = equipmentAssets.closedHelmets[helmet];
        //helmetAttachment.sharedMesh = equipmentAssets.helmetAttachments[attachment];
    }

    public void UnequipClosedHelmet()
    {
        closedHelmet.gameObject.SetActive(false);
    }

    public void EquipOpenHelmet(int helmet)
    {
        UnequipClosedHelmet();

        openHelmet.gameObject.SetActive(true);
        //helmetAttachment .gameObject.SetActive(true);
        head.gameObject.SetActive(true);

        openHelmet.sharedMesh = equipmentAssets.openHelmets[helmet];
        //helmetAttachment.sharedMesh = equipmentAssets.helmetAttachments[attachment];
    }

    public void UnequipOpenHelmet()
    {
        openHelmet.gameObject.SetActive(false);
    }

    public void EnableHead()
    {
        if (!closedHelmet.gameObject.activeSelf)
        {
            head.gameObject.SetActive(true);
            hair.gameObject.SetActive(true);
            hair.sharedMesh = equipmentAssets.hairs[0];
        }
    }

    public void DisableHairAndHeadAndHelmet()
    {
        if (closedHelmet.gameObject.activeSelf)
        {
            head.gameObject.SetActive(false);
            hair.gameObject.SetActive(false);
            openHelmet .gameObject.SetActive(false);
            hat.gameObject.SetActive(false);
        }

        if (openHelmet.gameObject.activeSelf)
        {
            hair.gameObject.SetActive(false);
            closedHelmet.gameObject.SetActive(false);
            hat.gameObject.SetActive(false);
        }
    }

    public void EquipShoulderArmor(int armor)
    {
        shoulderLeft.gameObject.SetActive(true);
        shoulderRight.gameObject.SetActive(true);

        shoulderLeft.sharedMesh = equipmentAssets.shoulderLefts[armor];
        shoulderRight.sharedMesh = equipmentAssets.shoulderRights[armor];
    }

    public void UnequipShoulderArmor()
    {
        shoulderRight.gameObject.SetActive(false);
        shoulderLeft.gameObject.SetActive(false);
    }

    public void EquipChestArmor(int chest, int upperArms)
    {
        chestArmor.sharedMesh = equipmentAssets.chestArmors[chest];
        EquipUpperArmsArmor(upperArms);
    }

    public void UnequipChestArmor()
    {
        chestArmor.sharedMesh = equipmentAssets.chestArmors[0];

        UnequipUpperArmsArmor();
    }

    private void EquipUpperArmsArmor(int armor)
    {
        upperRightArm.sharedMesh = equipmentAssets.upperRightArms[armor];
        upperLeftArm.sharedMesh = equipmentAssets.upperLeftArms[armor];
    }

    private void UnequipUpperArmsArmor()
    {
        upperRightArm.sharedMesh = equipmentAssets.upperRightArms[0];
        upperLeftArm.sharedMesh = equipmentAssets.upperLeftArms[0];
    }

    public void EquipCape(int armor)
    {
        UnequipBackpack();
        capeAttachment.gameObject.SetActive(true);
        capeAttachment.sharedMesh = equipmentAssets.capeAttachments[armor]; 
    }

    public void UnequipCape()
    {
        capeAttachment.gameObject.SetActive(false);
    }

    public void EquipBackpack(int armor)
    {
        UnequipCape();
        backpackAttachment.gameObject.SetActive(true);
        backpackAttachment.sharedMesh = equipmentAssets.backpackAttachments[armor];
    }

    public void UnequipBackpack()
    {
        backpackAttachment.gameObject.SetActive(false);
    }

    public void EquipGloves(int armor)
    {
        lowerRightArm.sharedMesh = equipmentAssets.lowerRightArms[armor];
        lowerLeftArm.sharedMesh = equipmentAssets.lowerLeftArms[armor];
        rightHand.sharedMesh = equipmentAssets.rightHands[armor];
        leftHand.sharedMesh = equipmentAssets.leftHands[armor];
    }

    public void UnequipGloves()
    {
        rightHand.sharedMesh = equipmentAssets.rightHands[0];
        leftHand.sharedMesh = equipmentAssets.leftHands[0];
        lowerLeftArm.sharedMesh = equipmentAssets.lowerLeftArms[0];
        lowerRightArm.sharedMesh = equipmentAssets.lowerRightArms[0];
    }

    public void EquipLegArmor(int armor)
    {
        hips.sharedMesh = equipmentAssets.hips[armor];
    }

    public void UnequipLegArmor()
    {
        hips.sharedMesh = equipmentAssets.hips[0];
    }

    // Legs shared mesh and equipment assets are BACKWARDS
    public void EquipBoots(int armor)
    {
        rightLeg.sharedMesh = equipmentAssets.leftLegs[armor];
        leftLeg.sharedMesh = equipmentAssets.rightLegs[armor];
    }

    public void UnequipBoots()
    {
        rightLeg.sharedMesh = equipmentAssets.leftLegs[0];
        leftLeg.sharedMesh = equipmentAssets.rightLegs[0];
    }
}

[CustomEditor(typeof(EquipmentCustomizer))]
public class EquipmentCustomizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var customizer = (EquipmentCustomizer)target;

        if (GUILayout.Button(text: "Randomize"))
        {
            customizer.Randomize();
        }
    }
}
