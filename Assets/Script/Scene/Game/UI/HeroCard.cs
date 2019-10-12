using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Scene.Game.UI
{
    public class HeroCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public Image headImg;
        public Text levelText;
        public Slider energySlider;
        public Slider lifeSlider;

        private PersonData personData = null;

        public Image drayImg;
        private Transform parentTransf;
        private Transform targetTransf;
        private Vector3 startPos;
        private int layerNum;

        public void UpdateData(PersonBase personBase)
        {
            personData = personBase.personData;
            if (personData == null) return;

            headImg.sprite = TextureMgr.instance.GetHeroHeadImgAtId(personData.Id);
            drayImg.sprite = headImg.sprite;
            levelText.text = personData.Level.ToString();
            energySlider.value = personData.CurrEnergy * 1.0f / personData.MaxEnergy;
            lifeSlider.value = personData.CurrLife * 1.0f / personData.MaxLife;
        }

        private void Start()
        {
            targetTransf = GameObject.Find("Canvas").transform;
            parentTransf = drayImg.transform.parent;
            startPos = drayImg.transform.localPosition;
            layerNum = drayImg.transform.GetSiblingIndex();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("拖拽中...");
            drayImg.rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("PlayerPos"))
                {
                    personData=new PersonData();
                    personData.Id = 1;
                    hit.collider.GetComponent<CharacterPos>().InitChessPos(personData);
                }
            }

            drayImg.transform.SetParent(parentTransf);
            drayImg.transform.localPosition = startPos;
            drayImg.transform.SetSiblingIndex(layerNum);
            Debug.Log("结束拖拽");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            drayImg.transform.SetParent(targetTransf);
            Debug.Log("开始拖拽");
        }
    }
}