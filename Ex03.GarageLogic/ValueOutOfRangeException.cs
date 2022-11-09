using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;
        private string m_Message;
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
            this.m_Message = $"The chosen value is out of bounds. (not between {m_MinValue} and {m_MaxValue})";
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
            this.m_Message = i_Message;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, bool i_AddWordValueForDefaultMessage, string i_WordToAddForValue) : this(i_MinValue, i_MaxValue)
        {
            if (i_AddWordValueForDefaultMessage)
            {
                this.m_Message = $"The chosen value of {i_WordToAddForValue} is out of bounds. (not between {m_MinValue} and {m_MaxValue})";
            }
        }

        public override string ToString()
        {
            return m_Message;
        }
    }
}
