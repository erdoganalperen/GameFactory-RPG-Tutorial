using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEffect : MonoBehaviour
{
    [Header("Skill Effects")]
    public GameObject HammerSkill;
    public GameObject KickSkill;
    public GameObject SpellCastSkill;
    public GameObject HealSkill;
    public GameObject ShieldSkill;
    public GameObject ComboSkill;
    [Header("Skill Transforms")] 
    public Transform KickTransform;
    public Transform SpellCastSkillTransform;
    public Transform HammerSkillTransform;
    public Transform ComboSkillTransform;

    private void HammerSkillCast()
    {
        Instantiate(HammerSkill, HammerSkillTransform.position, Quaternion.identity);
    }
    private void KickSpellCast()
    {
        Instantiate(KickSkill, KickTransform.position, Quaternion.identity);
    }
    private void SpellCast()
    {
        Instantiate(SpellCastSkill, SpellCastSkillTransform.position, Quaternion.identity);
    }
    private void SlashComboCast()
    {
        Instantiate(ComboSkill, ComboSkillTransform.position, Quaternion.identity);
    }
    private void ShieldCast()
    {
        Vector3 pos = transform.position;
        GameObject shield=Instantiate(ShieldSkill, pos, Quaternion.identity);
        shield.transform.SetParent(transform);
    }
    private void HealCast()
    {        
        Vector3 pos = transform.position;
        GameObject healClone = Instantiate(HealSkill, pos, Quaternion.identity);
        healClone.transform.SetParent(transform);
    }
}
