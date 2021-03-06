﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EXS
{
    public class Scene : GameObjectGroup
    {
        private bool _inputEnabled = true;

        protected FadingLayer fadeInLayer;
        protected FadingLayer fadeOutLayer;
        
        private TimeNotifier _hangNotifier = new TimeNotifier();
        public double HangTime { get; set; }

        private Scene _nextScene = null;

        public bool IsTransitioning
        {
            get
            {
                return !fadeInLayer.HasCompleted || (fadeOutLayer.HasStarted && !fadeOutLayer.HasCompleted)
                        || _hangNotifier.Initialized;
            }
        }

        public void GoToNextScene(Scene nextScene)
        {
            _nextScene = nextScene;
            _hangNotifier.NotifyMe(HangTime, true);
        }

        public virtual void InitScene()
        {
            fadeInLayer.Start();
        }

        public Scene(double fadeInTime = 0, double fadeOutTime = 0, double hangTime = 0) : base()
        {
            HangTime = hangTime;

            fadeInLayer = new FadingLayer(true, fadeInTime);
            fadeInLayer.Initialize(Core.game.Content);

            fadeOutLayer = new FadingLayer(false, fadeOutTime);
            fadeOutLayer.Initialize(Core.game.Content);
        }

        public override void Update()
        {
            if (IsTransitioning)
            {
                _inputEnabled = false;
            }
            else
            {
                _inputEnabled = true;
            }

            if (_hangNotifier.Notify)
            {
                fadeOutLayer.Start();
            }

            if (fadeOutLayer.HasCompleted)
            {
                Core.game.SetScene(_nextScene);
            }

            if (_inputEnabled)
            {
                //only now update everything according to base class
                base.Update();
            }

            UpdateAnimation();
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            fadeInLayer.UpdateAnimation();
            fadeOutLayer.UpdateAnimation();
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            base.Draw(batch, false);
            base.Draw(batch, true);

            fadeInLayer.Draw(batch, false);
            fadeOutLayer.Draw(batch, false);
        }
    }
}