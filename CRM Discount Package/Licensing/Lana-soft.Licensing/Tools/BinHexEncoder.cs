using System;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The class used for binary to hex encoding.
	/// </summary>
    public sealed class BinHexEncoder
    {
		#region Private Constructor
		private BinHexEncoder()
        {
        }
		#endregion

		#region Public Stuff
        public static string EncodeToBinHex(byte[] inArray, int offsetIn, int count)
        {
            if (null == inArray)
                throw new ArgumentNullException("inArray");
            
			if (0 > offsetIn)
                throw new ArgumentOutOfRangeException("offsetIn");
            
			if (0 > count)
                throw new ArgumentOutOfRangeException("count");
            
			if (count > (inArray.Length - offsetIn))
                throw new ArgumentException("Count > inArray.Length - offsetIn");
            
			char[] hexArray = new char[2 * count];
            int length = BinHexEncoder.EncodeToBinHex(inArray, offsetIn, count, hexArray);
            
			return new string(hexArray, 0, length);
        }
		#endregion

		#region Private Stuff
        private static int EncodeToBinHex(byte[] inArray, int offsetIn, int count, char[] outArray)
        {
            int num1 = 0;
            int num2 = 0;
            int num4 = outArray.Length;
            
			for (int index = 0; index < count; index++)
            {
                byte num3 = inArray[offsetIn++];
                outArray[num1++] = "0123456789ABCDEF"[num3 >> 4];
                if (num1 == num4)
                {
                    break;
                }
                outArray[num1++] = "0123456789ABCDEF"[num3 & 15];
                if (num1 == num4)
                {
                    break;
                }
            }
            
			return (num1 - num2);
        }
		#endregion
    }
}

