using System;
using System.Xml;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The internal class used for hex to binary decoding.
	/// </summary>
    internal class BinHexDecoder
    {
		#region Private Variables
		private ArrayManager _charBuffer;
		private byte _highHalfByte;
		private bool _highNibblePresent;
		#endregion

		#region Construction
        public BinHexDecoder()
        {
            _charBuffer = new ArrayManager();
        }
		#endregion

		#region Internal Stuff
        internal byte[] DecodeBinHex(char[] inArray, int offset, bool flush)
        {
            int num1 = inArray.Length;
            byte[] buffer1 = new byte[((num1 - offset) + 1) / 2];
            int num2 = this.DecodeBinHex(inArray, offset, inArray.Length, buffer1, 0, buffer1.Length, flush);
            if (num2 != buffer1.Length)
            {
                byte[] buffer2 = new byte[num2];
            	Array.Copy(buffer1, buffer2, num2);
                buffer1 = buffer2;
            }
            return buffer1;
        }

        internal int DecodeBinHex(char[] inArray, int offset, int inLength, byte[] outArray, int offsetOut, int countOut, bool flush)
        {
            byte num4;
            char ch1;
            if (0 > offset)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (0 > offsetOut)
            {
                throw new ArgumentOutOfRangeException("offsetOut");
            }
            int num1 = (inArray == null) ? 0 : inArray.Length;
            if (num1 < inLength)
            {
                throw new ArgumentOutOfRangeException("inLength");
            }
            int num2 = outArray.Length;
            if (num2 < (countOut + offsetOut))
            {
                throw new ArgumentOutOfRangeException("offsetOut");
            }
            int num3 = inLength - offset;
            if (flush)
            {
                this._charBuffer.Refresh();
            }
            if (num3 > 0)
            {
                this._charBuffer.Append(inArray, offset, inLength);
            }
            if ((this._charBuffer.Length == 0) || (countOut == 0))
            {
                return 0;
            }
            countOut += offsetOut;
            int num5 = this._charBuffer.Length;
            int num6 = offsetOut;
            int num7 = 0;
        Label_00A3:
            ch1 = this._charBuffer[num7++];
            if ((ch1 >= 'a') && (ch1 <= 'f'))
            {
                num4 = (byte) (('\n' + ch1) - 'a');
            }
            else if ((ch1 >= 'A') && (ch1 <= 'F'))
            {
                num4 = (byte) (('\n' + ch1) - 'A');
            }
            else if ((ch1 >= '0') && (ch1 <= '9'))
            {
                num4 = (byte) (ch1 - '0');
            }
            else
            {
                if (XmlCharType.IsWhiteSpace(ch1))
                {
                    goto Label_0199;
                }
                string text1 = new string(this._charBuffer.CurrentBuffer, this._charBuffer.CurrentBufferOffset, (this._charBuffer.CurrentBuffer == null) ? 0 : (this._charBuffer.CurrentBufferLength - this._charBuffer.CurrentBufferOffset));
                throw new XmlException(text1);
            }
            if (this._highNibblePresent)
            {
                outArray[num6++] = (byte) ((this._highHalfByte << 4) + num4);
                this._highNibblePresent = false;
                if (num6 != countOut)
                {
                    goto Label_0199;
                }
                goto Label_01A2;
            }
            this._highHalfByte = num4;
            this._highNibblePresent = true;
        Label_0199:
            if (num7 < num5)
            {
                goto Label_00A3;
            }
        Label_01A2:
            this._charBuffer.CleanUp(num7);
            return (num6 - offsetOut);
        }

        internal void Flush()
        {
            if (this._charBuffer != null)
            {
                this._charBuffer.Refresh();
            }
            this._highNibblePresent = false;
        }

        internal int BitsFilled
        {
            get
            {
                if (!this._highNibblePresent)
                {
                    return 0;
                }
                return 4;
            }
        }
		#endregion
    }
}

