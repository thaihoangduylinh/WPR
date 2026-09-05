using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace WPR.XnaCompability
{
    public class SpriteBatch2 : SpriteBatch
    {

        private delegate void PrepRenderStateDelegate();
        private PrepRenderStateDelegate PrepRenderStateMethod;

        FieldInfo beginCalledField;
        FieldInfo sortModeField;
        FieldInfo blendStateField;
        FieldInfo samplerStateField;
        FieldInfo depthStencilStateField;
        FieldInfo rasterizerStateField;
        FieldInfo customEffectField;
        FieldInfo transformMatrixField;
        public SpriteBatch2(GraphicsDevice graphicsDevice) : base(graphicsDevice) {
            if (PrepRenderStateMethod == null)
            {
                MethodInfo method = typeof(SpriteBatch).GetMethod("PrepRenderState", BindingFlags.NonPublic | BindingFlags.Instance);
                PrepRenderStateMethod = (PrepRenderStateDelegate)Delegate.CreateDelegate(typeof(PrepRenderStateDelegate), this, method);
            }

            beginCalledField = typeof(SpriteBatch).GetField("beginCalled", BindingFlags.NonPublic | BindingFlags.Instance);
            sortModeField = typeof(SpriteBatch).GetField("sortMode", BindingFlags.NonPublic | BindingFlags.Instance);
            blendStateField = typeof(SpriteBatch).GetField("blendState", BindingFlags.NonPublic | BindingFlags.Instance);
            samplerStateField = typeof(SpriteBatch).GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            depthStencilStateField = typeof(SpriteBatch).GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            rasterizerStateField = typeof(SpriteBatch).GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            customEffectField = typeof(SpriteBatch).GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            transformMatrixField = typeof(SpriteBatch).GetField("transformMatrix", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void Begin(
            SpriteSortMode sortMode,
            BlendState blendState,
            SamplerState samplerState,
            DepthStencilState depthStencilState,
            RasterizerState rasterizerState,
            Effect2 effect
        )
        {
            Begin(
                sortMode,
                blendState,
                samplerState,
                depthStencilState,
                rasterizerState,
                effect,
                Matrix.Identity
            );
        }

        public void Begin(
            SpriteSortMode sortMode,
            BlendState blendState,
            SamplerState samplerState,
            DepthStencilState depthStencilState,
            RasterizerState rasterizerState,
            Effect2 effect,
            Matrix transformationMatrix
        )
        {
            bool beginCalled = (bool)beginCalledField.GetValue(this);
            if (beginCalled)
            {
                throw new InvalidOperationException(
                    "Begin has been called before calling End" +
                    " after the last call to Begin." +
                    " Begin cannot be called again until" +
                    " End has been successfully called."
                );
            }
            beginCalled = true;
            beginCalledField.SetValue(this, beginCalled);
            sortModeField.SetValue(this, sortMode);
            blendStateField.SetValue(this, blendState ?? BlendState.AlphaBlend);
            samplerStateField.SetValue(this, samplerState ?? SamplerState.LinearClamp);
            depthStencilStateField.SetValue(this, depthStencilState ?? DepthStencilState.None);
            rasterizerStateField.SetValue(this, rasterizerState ?? RasterizerState.CullCounterClockwise);
            customEffectField.SetValue(this, effect);
            transformMatrixField.SetValue(this, transformationMatrix);

            if (sortMode == SpriteSortMode.Immediate)
            {
                PrepRenderStateMethod();
            }
        }
    }
}
