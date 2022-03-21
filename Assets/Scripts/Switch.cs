using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Tilemaps;
using static UnityEngine.Color;


namespace InverseWorld
{
    public class Switch : MonoBehaviour
    {
        [Header("Object References")]
        [SerializeField]
        private GameObject normalEnvironment;
        [SerializeField]
        private GameObject inverseEnvironment;
        [SerializeField]
        private GameObject normalBase;
        [SerializeField]
        private GameObject inverseBase;
        [SerializeField]
        private GameObject player;

        [SerializeField] private Sprite regularSprite;
        [SerializeField] private Sprite invertedSprite;
        [SerializeField] private InversionVignette iv;
        [SerializeField] private TextMeshProUGUI countText;

        private ParticleSystem playerParticle;
        private SpriteRenderer sr;
        
        public PostProcessVolume volume; //Assigned with the editor
        private Bloom bloom;
        // Extra Variables
        public static bool IsInverted = false;
        public int inversionAmount = 3;
        public static int InversionLimit = 3;

        public float inversionTime = 10f;
        private bool timerOn = false;

        [SerializeField] private GameObject peekCircle;

        private bool isPeeking = false;
        
        private void Start()
        {
            InversionLimit = inversionAmount;
            playerParticle = player.GetComponent<ParticleSystem>();
            sr = player.GetComponent<SpriteRenderer>();
            
            bloom = volume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();
        }


        void Update()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            peekCircle.transform.position = worldPos;
            
            Switcher();
            Peek();
            
            if (timerOn)
            {
                Timer();
            }
        }

        public void Switcher()
        {
            if (InversionLimit > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) && IsInverted == false)
                {
                    Invert();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && IsInverted == true)
                {
                    Revert();
                    InversionLimit--;
                }
            }
        }

        private void Timer()
        {
            inversionTime -= Time.deltaTime;
                
            if (inversionTime < 0)
            {
                Destroy(player);
                timerOn = false;
            }

            if (inversionTime < 5f)
            {
                iv.StartVignette();
            }
        }

        public void Invert()
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = white;
            countText.color = black;
            player.GetComponent<SpriteRenderer>().sprite = invertedSprite;
            player.GetComponent<Rigidbody2D>().gravityScale = -8f;
            sr.flipY = true;
            
            var playerParticleMain = playerParticle.main;
            playerParticleMain.gravityModifier = -1;
            playerParticleMain.startColor = black;
            
            var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
            colorParameter.value = Color.black;
            bloom.color.Override(colorParameter);

            var baseColliderNormal = normalBase.GetComponent<TilemapCollider2D>();
            var baseColliderInverse = inverseBase.GetComponent<TilemapCollider2D>();
            var baseTilemapNormal = normalBase.GetComponent<Tilemap>();
            var baseTilemapInverse = inverseBase.GetComponent<Tilemap>();
            
            normalEnvironment.SetActive(false);
            baseTilemapNormal.color = Color.white;
            baseColliderNormal.isTrigger = true;
            inverseEnvironment.SetActive(true);
            baseTilemapInverse.color = Color.white;
            baseColliderInverse.isTrigger = false;

            if (isPeeking == true)
            {
                normalEnvironment.SetActive(true);
            }
            
            
            IsInverted = true;
            timerOn = true;
        }

        public void Revert()
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = black;
            countText.color = white;
            player.GetComponent<SpriteRenderer>().sprite = regularSprite;
            player.GetComponent<Rigidbody2D>().gravityScale = 8f;
            sr.flipY = false;
            
            var playerParticleMain = playerParticle.main;
            playerParticleMain.gravityModifier = 1;
            playerParticleMain.startColor = white;
            
            var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
            colorParameter.value = Color.white;
            bloom.color.Override(colorParameter);

            var baseColliderNormal = normalBase.GetComponent<TilemapCollider2D>();
            var baseColliderInverse = inverseBase.GetComponent<TilemapCollider2D>();
            var baseTilemapNormal = normalBase.GetComponent<Tilemap>();
            var baseTilemapInverse = inverseBase.GetComponent<Tilemap>();
            
            normalEnvironment.SetActive(true);
            baseTilemapNormal.color = Color.black;
            baseColliderNormal.isTrigger = false;
            inverseEnvironment.SetActive(false);
            baseTilemapInverse.color = Color.black;
            baseColliderInverse.isTrigger = true;


            if (isPeeking == true)
            {
                inverseEnvironment.SetActive(true);
            }
            
            
            IsInverted = false;
            timerOn = false;
            inversionTime = 10f;
            iv.EndVignette();
        }

        private void Peek()
        {
            if (IsInverted)
            {
                var color = peekCircle.GetComponent<SpriteRenderer>();
                color.color = Color.black;
            }
            else if (IsInverted == false)
            {
                var color = peekCircle.GetComponent<SpriteRenderer>();
                color.color = Color.white;
            }
            
            
            
            if (Input.GetKeyDown(KeyCode.Q) && isPeeking == false)
            {
                isPeeking = true;
                peekCircle.SetActive(true);

                if (IsInverted == false)
                {
                    inverseEnvironment.SetActive(true);
                }
                else if (IsInverted)
                {
                    normalEnvironment.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q) && isPeeking)
            {
                isPeeking = false;
                peekCircle.SetActive(false);
                
                if (IsInverted == false)
                {
                    inverseEnvironment.SetActive(false);
                }
                else if (IsInverted)
                {
                    normalEnvironment.SetActive(false);
                }
            }
        }
    }   
}

