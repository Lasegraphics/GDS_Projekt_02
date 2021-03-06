﻿using System.Collections;
using GridPack.Cells;
using GridPack.Units;
using GridPack.Grid; 
using UnityEngine;
using UnityEngine.UI;

namespace GridPack.SceneScripts
{
    public class Entity : Unit
    {
        Coroutine PulseCoroutine;
        Color myColor;
        private void Awake()
        {
        }
        private TurnChanger turnChanger; 

        public override void Initialize()
        {
            base.Initialize();
            transform.localPosition += new Vector3(0, 0, -1);
            turnChanger = GameObject.Find("GameController").GetComponent<TurnChanger>();  
        }

        public override bool IsCellMovableTo(Cell cell)
        {
            return base.IsCellMovableTo(cell) && (cell as MyOtherHexagon).GroundType != GroundType.Forest;
            //Zabrania poruszania się do komórek typu woda 
        }
        public override bool IsCellTraversable(Cell cell)
        {
            return base.IsCellTraversable(cell) && (cell as MyOtherHexagon).GroundType != GroundType.Forest;
           //Zabrania poruszania się przez komórki typu woda
        }

        public override void OnUnitDeselected()
        {
            //Zatrzymuje pulsacje jednostki
            base.OnUnitDeselected();
            StopCoroutine(PulseCoroutine);
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }

        public override void MarkAsAttacking(Unit other)
        {
            myColor = gameObject.GetComponent<SpriteRenderer>().color;            
            StartCoroutine(Jerk(other, 0.25f));
        }
        public override void MarkAsDefending(Unit other)
        {
            StartCoroutine(Glow(new Color(1, 0.5f, 0.5f), 1));
        }
        public override void MarkAsDestroyed()
        {
        }

        private IEnumerator Jerk(Unit other, float movementTime)
        {
            var heading = other.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            float startTime = Time.time;

            while (true)
            {
                var currentTime = Time.time;
                if (startTime + movementTime < currentTime)
                    break;
                transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + movementTime) - currentTime));
                yield return 0;
            }
            startTime = Time.time;
            while (true)
            {
                var currentTime = Time.time;
                if (startTime + movementTime < currentTime)
                    break;
                transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + movementTime) - currentTime));
                yield return 0;
            }
            transform.position = Cell.transform.position + new Vector3(0, 0, -1);
        }
        private IEnumerator Glow(Color color, float cooloutTime)
        {
            var _renderer = GetComponent<SpriteRenderer>();
            float startTime = Time.time;

            while (true)
            {
                var currentTime = Time.time;
                if (startTime + cooloutTime < currentTime)
                    break;
               // _renderer.color = Color.Lerp(Color.white, color, (startTime + cooloutTime) - currentTime);
                yield return 0;
            }
        }
        private IEnumerator Pulse(float breakTime, float delay, float scaleFactor)
        {
            var baseScale = transform.localScale;
            while (true)
            {
                float time1 = Time.time;
                while (time1 + delay > Time.time)
                {
                    transform.localScale = Vector3.Lerp(baseScale * scaleFactor, baseScale, (time1 + delay) - Time.time);
                    yield return 0;
                }
                float time2 = Time.time;
                while (time2 + delay > Time.time)
                {
                    transform.localScale = Vector3.Lerp(baseScale, baseScale * scaleFactor, (time2 + delay) - Time.time);
                    yield return 0;
                }
                yield return new WaitForSeconds(breakTime);
            }
        }

        public override void MarkAsFriendly()
        {
            SetHighlighterColor(new Color(0.75f, 0.75f, 0.75f, 0.5f));
        }
        public override void MarkAsReachableEnemy()
        {
            SetHighlighterColor(new Color(1, 0, 0, 0.5f));
        }
        public override void MarkAsSelected()
        {
            PulseCoroutine = StartCoroutine(Pulse(1.0f, 0.5f, 1.25f));
            SetHighlighterColor(new Color(0, 1, 0, 0.5f));
        }
        public override void MarkAsFinished()
        {
            SetHighlighterColor(new Color(0.75f, 0.75f, 0.75f, 0.5f));  
        }
        public override void UnMark()
        {
            SetHighlighterColor(Color.clear);
        }
      
        private void SetHighlighterColor(Color color)
        {
            var highlighter = transform.Find("WhiteTile").GetComponent<SpriteRenderer>();
            if (highlighter != null)
            {
                highlighter.color = color;
            }
        }
    }
}