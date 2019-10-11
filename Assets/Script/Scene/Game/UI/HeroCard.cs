using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scene.Game.UI
{
    public class HeroCard : MonoBehaviour
    {
        public Image headImg;
        public Text levelText;
        public Slider energySlider;
        public Slider lifeSlider;

        public void UpdateData(PersonBase personBase)
        {
            headImg.sprite = TextureMgr.instance.GetHeroHeadImgAtId(personBase.personData.Id);
            levelText.text = personBase.personData.Level.ToString();
            energySlider.value = personBase.personData.CurrEnergy * 1.0f / personBase.personData.MaxEnergy;
            lifeSlider.value = personBase.personData.CurrLife * 1.0f / personBase.personData.MaxLife;
        }
    }
}