using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace GOD2FAT
{
    public class GameInterpreter
    {
        private FileStream data;

        public bool isValid;
        public bool selected = false;

        public int fileType;
        public long fileSize;
        public string titleId;
        public string mediaId;
        public int discNumber;
        public int discTotal;
        public string title_en;
        public string description_en;
        public Image thumbnail;
        public string type;

        public GameInterpreter(string filePath)
        {
            this.data = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            this.fileSize = this.data.Length;
            this.fileType = this.getFileType(this.data);
            this.isValid = (this.fileType == 1279874629);
            this.type = (this.fileSize > 50000) ? "XBLA" : "GOD";

            if (this.isValid) this.parse();
        }

        private void parse()
        {
            this.title_en = this.getTitle(this.data, "en");
            this.titleId = this.getTitleId(this.data);
            this.mediaId = this.getMediaId(this.data);
            this.discNumber = this.getDiscNumber(this.data);
            this.discTotal = this.getDiscTotal(this.data);
            this.description_en = this.getDescription(this.data, "en");
            this.thumbnail = this.getThumbnail(this.data);
        }

        private int getFileType(FileStream data)
        {
            data.Position = 0;
            byte[] fileType = new byte[4];
            data.Read(fileType, 0, 4);
            return int.Parse(BitConverter.ToString(fileType).Replace("-", string.Empty), NumberStyles.HexNumber);
        }

        private string getTitle(FileStream data, string language)
        {
            switch(language)
            {
                case "en":
                    data.Position = 1042;
                    break;
            }
            byte[] title = new byte[256];

            data.Read(title, 0, 256);
            return unicodeToStr(title);
        }

        private string getMediaId(FileStream data)
        {
            data.Position = 852;
            byte[] mediaId = new byte[4];

            data.Read(mediaId, 0, 4);
            return BitConverter.ToString(mediaId).Replace("-", string.Empty);
        }

        private string getTitleId(FileStream data)
        {
            data.Position = 864;
            byte[] titleId = new byte[4];

            data.Read(titleId, 0, 4);
            return BitConverter.ToString(titleId).Replace("-", string.Empty);
        }

        private int getDiscNumber(FileStream data)
        {
            data.Position = 870;
            byte[] discNumber = new byte[1];
            data.Read(discNumber, 0, 1);
            return int.Parse( BitConverter.ToString(discNumber));
        }

        private int getDiscTotal(FileStream data)
        {
            data.Position = 871;
            byte[] discTotal = new byte[1];
            data.Read(discTotal, 0, 1);
            return int.Parse(BitConverter.ToString(discTotal));
        }

        private string getDescription(FileStream data, string language)
        {
            switch (language)
            {
                case "en":
                    data.Position = 3346;
                    break;
            }
            byte[] description = new byte[238];

            data.Read(description, 0, 238);
            return unicodeToStr(description);
        }

        private Image getThumbnail(FileStream data)
        {
            data.Position = 5914;
            byte[] thumbnail = new byte[15590];

            data.Read(thumbnail, 0, 15590);
            return Image.FromStream(new MemoryStream(thumbnail));
        }

        public string unicodeToStr(byte[] data)
        {
            var sb = new StringBuilder();
            int num = 0;

            while (num < data.Length)
            {
                string hexString = data[num].ToString("X2");
                if (hexString != "00")
                {
                    byte[] byteString = new byte[1];
                    byteString[0] = Convert.ToByte(hexString, 16);
                    sb.Append(Encoding.UTF8.GetString(byteString));
                }
                num++;
            }
            return sb.ToString();
        }

    }
}
