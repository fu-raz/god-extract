using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GOD2FAT
{
    class GameListView
    {
        private ListView lv;
        private ImageList il;
        public List<GameInterpreter> Games;

        public GameListView(ListView lv, ImageList il)
        {
            this.lv = lv;
            this.il = il;
            this.Games = new List<GameInterpreter>();

            this.il.ImageSize = new Size(64, 64);
            this.lv.CheckBoxes = true;
            this.lv.LargeImageList = this.il;
            this.lv.View = View.LargeIcon;

            this.lv.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.handleSelection);
        }

        public void addGame(GameInterpreter gi)
        {
            this.il.Images.Add(gi.titleId, gi.thumbnail);
            this.lv.Items.Add(new ListViewItem(gi.title_en, this.il.Images.Count -1));
            this.Games.Add(gi);
        }

        public void toggleSelectGame(int index)
        {
            this.Games[index].selected = (!this.Games[index].selected);
        }

        public void handleSelection(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked)
            {
                this.Games[e.Index].selected = false;
            }
            else
            {
                this.Games[e.Index].selected = true;
            }
        }
    }
}
