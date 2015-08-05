using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GOD2FAT
{
    public class GameFileReader
    {
        private StreamReader sr;

        public GameFileReader(string filePath)
        {
            this.sr = new StreamReader(filePath);
        }

        public void readSkip(int len)
        {
            // skip len characters
        }

        public string readId()
        {
            // read the next len characters as integer
            // int id = (int)this.readChar(4);
            return "";
            // 
        }

        public char[] readChar(int len)
        {
            char[] readCharacters = new char[len];
            // read the next len characters as char
            this.sr.Read(readCharacters, 0, len);
            return readCharacters;
        }

        public string readString(int len)
        {
            return "";
        }

        public int readInt(int len)
        {
            return 1;
        }

    }
}
