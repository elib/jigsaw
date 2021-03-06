﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EXS
{
    public class FrameSequence : Updatable
    {
        private List<int> sequence;
        private double frameRate;
        private double nextFrameChange = 0;

        private void SetNextFrameChange(double currentTime)
        {
            nextFrameChange = currentTime + (1 / frameRate);
        }

        public FrameSequence()
        {
            sequence = new List<int>();
            frameRate = 1;
            SetNextFrameChange(0);
        }

        public FrameSequence(IEnumerable<int> newSequence, double newFrameRate) : this()
        {
            sequence.AddRange(newSequence);
            frameRate = newFrameRate;
            SetNextFrameChange(0);
        }

        public void ResetFrames(bool isRandom = false)
        {
            if (isRandom)
            {
                _currentFrameIndex = (int)(Core.rand.NextDouble() * sequence.Count);
            }
            else
            {
                _currentFrameIndex = 0;
            }
        }

        private int _currentFrameIndex = 0;

        public override void Update()
        {
            double totalElapsedSeconds = Core.TotalTime;
            if (totalElapsedSeconds >= nextFrameChange)
            {
                SetNextFrameChange(totalElapsedSeconds);
                _currentFrameIndex = (_currentFrameIndex + 1) % sequence.Count;
            }
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            throw new NotImplementedException("You cannot render an frame sequence. You must use a sprite.");
        }

        public int CurrentFrameNumber
        {
            get
            {
                return this.sequence[_currentFrameIndex];
            }
        }
    }

    public class AnimationInfo : Updatable
    {
        private Dictionary<string, FrameSequence> _animationList;
        public FrameSequence CurrentAnimation { get; private set; }

        public AnimationInfo()
        {
            _animationList = new Dictionary<string, FrameSequence>();
        }

        public void SetCurrentAnimation(string animation)
        {
            SetCurrentAnimation(animation, false);
        }

        public void SetCurrentAnimation(string animation, bool reset)
        {
            if (_animationList.ContainsKey(animation))
            {
                CurrentAnimation = _animationList[animation];
                if (reset)
                {
                    CurrentAnimation.ResetFrames();
                }
            }
        }

        public void Add(string name, IEnumerable<int> newSequence, float frameRate)
        {
            FrameSequence seq = new FrameSequence(newSequence, frameRate);
            _animationList[name] = seq;
        }

        public override void Update()
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update();
            }
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            throw new NotImplementedException("You cannot render an animation. You must use a sprite.");
        }

        public void RandomFrame()
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.ResetFrames(true);
            }
        }
    }
}