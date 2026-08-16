using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace WPR.XnaCompability
{
    public class Effect2 : Effect
    {
        #region Effect Parameters

        EffectParameter matrixParam;

        #endregion

        #region Methods


        /// <summary>
        /// Creates a new SpriteEffect.
        /// </summary>
        public Effect2(GraphicsDevice device, byte[] effectCode)
            : base(device, Effect2.GetResource())
        {
            CacheEffectParameters();
        }


        /// <summary>
        /// Creates a new SpriteEffect by cloning parameter settings from an existing instance.
        /// </summary>
        protected Effect2(Effect2 cloneSource)
            : base(cloneSource)
        {
            CacheEffectParameters();
        }

        private static byte[] GetResource()
        {
            Stream stream = typeof(Effect2).Assembly.GetManifestResourceStream(
                "WPR.XnaCompability.SpriteEffect.fxb"
            );
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }


        /// <summary>
        /// Creates a clone of the current SpriteEffect instance.
        /// </summary>
        public override Effect Clone()
        {
            return new Effect2(this);
        }


        /// <summary>
        /// Looks up shortcut references to our effect parameters.
        /// </summary>
        void CacheEffectParameters()
        {
            matrixParam = Parameters["MatrixTransform"];
        }


        /// <summary>
        /// Lazily computes derived parameter values immediately before applying the effect.
        /// </summary>
        protected override void OnApply()
        {
            Viewport viewport = GraphicsDevice.Viewport;

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            matrixParam.SetValue(halfPixelOffset * projection);
        }


        #endregion
    }
}