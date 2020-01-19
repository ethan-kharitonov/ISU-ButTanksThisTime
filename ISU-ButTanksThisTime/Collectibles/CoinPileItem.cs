// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-19-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Class CoinPileItem.
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.CoinItem" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.CoinItem" />
    internal class CoinPileItem : CoinItem
    {
        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        /// <summary>
        /// Initializes static members of the <see cref="CoinPileItem"/> class.
        /// </summary>
        static CoinPileItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_B");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinPileItem"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public CoinPileItem(Vector2 position) : base(position, 25)
        {
        }

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        public override Texture2D Img => img;
    }
}