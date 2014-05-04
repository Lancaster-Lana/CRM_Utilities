using System.Collections;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The internal array manager class.
	/// </summary>
    internal class ArrayManager
    {
		#region Private Variables
		private Queue _bufferQueue;
		private Buffer _currentBuffer;
		private int _offset;
		#endregion

		#region Construction
        internal ArrayManager()
        {
        }
		#endregion

		#region Internal Stuff
        internal void Append(char[] buffer, int offset, int size)
        {
            this.BufferQueue.Enqueue(new Buffer(buffer, offset, size));
        }

        internal void CleanUp(int internalBufferOffset)
        {
            if (this._currentBuffer != null)
            {
                this._currentBuffer._offset += (internalBufferOffset - this.Offset);
                this.Offset = 0;
            }
        }

        internal void Refresh()
        {
            this.BufferQueue = new Queue();
            this._currentBuffer = null;
            this._offset = 0;
        }

		internal char[] CurrentBuffer
		{
			get
			{
				if (this._currentBuffer != null)
				{
					return this._currentBuffer._charBuffer;
				}
				return null;
			}
		}

		internal int CurrentBufferLength
		{
			get
			{
				if (this._currentBuffer != null)
				{
					return this._currentBuffer._size;
				}
				return 0;
			}
		}

		internal int CurrentBufferOffset
		{
			get
			{
				if (this._currentBuffer != null)
				{
					return this._currentBuffer._offset;
				}
				return 0;
			}
		}

		internal char this[int index]
		{
			get
			{
				char ch1 = '\0';
				if (this._currentBuffer == null)
				{
					if (this.BufferQueue.Count > 0)
					{
						this._currentBuffer = (Buffer)this.BufferQueue.Dequeue();
					}
					else
					{
						return ch1;
					}
				}
				if (((this._currentBuffer._offset + index) - this.Offset) >= this._currentBuffer._size)
				{
					this.Offset = index;
					this._currentBuffer = (this.BufferQueue.Count > 0) ? ((Buffer)this.BufferQueue.Dequeue()) : null;
				}
				if (this._currentBuffer != null)
				{
					ch1 = this._currentBuffer._charBuffer[this._currentBuffer._offset + (index - this.Offset)];
				}
				return ch1;
			}
		}

		internal int Length
		{
			get
			{
				int num1 = 0;
				if (this._currentBuffer != null)
				{
					num1 += (this._currentBuffer._size - this._currentBuffer._offset);
				}
				IEnumerator enumerator1 = this.BufferQueue.GetEnumerator();
				while (enumerator1.MoveNext())
				{
					Buffer buffer1 = (Buffer) enumerator1.Current;
					num1 += (buffer1._size - buffer1._offset);
				}
				return num1;
			}
		}
		#endregion

		#region Private Stuff
        private Queue BufferQueue
        {
            get
            {
                if (this._bufferQueue == null)
                {
                    this._bufferQueue = new Queue();
                }
                return this._bufferQueue;
            }
            set
            {
                this._bufferQueue = value;
            }
        }

        private int Offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                this._offset = value;
            }
        }
		#endregion

        // Nested Types
        internal class Buffer
        {
			public char[] _charBuffer;
			public int _offset;
			public int _size;

            public Buffer(char[] buffer, int offset, int size)
            {
                this._charBuffer = buffer;
                this._offset = offset;
                this._size = size;
            }
        }
    }
}

