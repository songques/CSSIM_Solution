namespace CSS.IM.UI.Control.Graphics.FileTransfersControl
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FileTansfersContainer : Panel
    {
        private IFileTransfersItemText _fileTransfersItemText;

        public FileTansfersContainer()
        {
            this.AutoScroll = true;
        }

        public FileTransfersItem AddItem(string text, string fileName, Image image, long fileSize, FileTransfersItemStyle style)
        {
            FileTransfersItem item = new FileTransfersItem();
            item.Text = text;
            item.FileName = fileName;
            item.Image = image;
            item.FileSize = fileSize;
            item.Style = style;
            item.FileTransfersText = this.FileTransfersItemText;
            item.Dock = DockStyle.Top;
            base.SuspendLayout();
            base.Controls.Add(item);
            item.BringToFront();
            base.ResumeLayout(true);
            return item;
        }

        public FileTransfersItem AddItem(string name, string text, string fileName, Image image, long fileSize, FileTransfersItemStyle style)
        {
            FileTransfersItem item = new FileTransfersItem();
            item.Name = name;
            item.Text = text;
            item.FileName = fileName;
            item.Image = image;
            item.FileSize = fileSize;
            item.Style = style;
            item.FileTransfersText = this.FileTransfersItemText;
            item.Dock = DockStyle.Top;
            base.SuspendLayout();
            base.Controls.Add(item);
            item.BringToFront();
            base.ResumeLayout(true);
            return item;
        }

        public void RemoveItem(FileTransfersItem item)
        {
            base.Controls.Remove(item);
        }

        public void RemoveItem(Predicate<FileTransfersItem> match)
        {
            FileTransfersItem item = null;
            foreach (FileTransfersItem item2 in base.Controls)
            {
                if (match(item2))
                {
                    item = item2;
                }
            }
            base.Controls.Remove(item);
        }

        public void RemoveItem(string name)
        {
            base.Controls.RemoveByKey(name);
        }

        public FileTransfersItem Search(Predicate<FileTransfersItem> match)
        {
            foreach (FileTransfersItem item in base.Controls)
            {
                if (match(item))
                {
                    return item;
                }
            }
            return null;
        }

        public FileTransfersItem Search(string name)
        {
            return (base.Controls[name] as FileTransfersItem);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IFileTransfersItemText FileTransfersItemText
        {
            get
            {
                if (this._fileTransfersItemText == null)
                {
                    this._fileTransfersItemText = new FileTransfersItemText();
                }
                return this._fileTransfersItemText;
            }
            set
            {
                this._fileTransfersItemText = value;
                foreach (FileTransfersItem item in base.Controls)
                {
                    item.FileTransfersText = this._fileTransfersItemText;
                }
            }
        }
    }
}

