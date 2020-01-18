using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Inventory
    {
        private readonly Dictionary<ItemType, Icon> icons = new Dictionary<ItemType, Icon>();
        public Inventory()
        {
            //Add re-locate icon
            Icon reLocateIcon = new Icon(new Vector2(Tools.Screen.Width - 10 - Icon.Frame.Width, 10), RelocateItem.VoidObject);
            icons.Add(ItemType.Relocate, reLocateIcon);

            //Add speed boost icon
            Icon speedBoostIcon = new Icon(new Vector2(Tools.Screen.Width - 2 * (10 + Icon.Frame.Width), 10), SpeedBoostItem.VoidObject);
            icons.Add(ItemType.SpeedBoost, speedBoostIcon);

            Icon coinIcon = new Icon(new Vector2(Tools.Screen.Width - 3 * (15 + Icon.Frame.Width), 10), CoinItem.VoidObject);
            icons.Add(ItemType.Coin, coinIcon);

        }

        public void Update()
        {
            foreach (var icon in icons.Values)
            {
                icon.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var icon in icons.Values)
            {
                icon.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void AddItem(Item item)
        {
            icons[item.ItemType].Add(item.Amount);
        }


    }
}
