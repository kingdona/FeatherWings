using System;
using System.Collections.Generic;
using Meadow.Foundation.ICs.IOExpanders;
using Meadow.Hardware;


namespace Meadow.Foundation.FeatherWings
{
    public class SevenSegmentWing
    {
        readonly Ht16k33 ht16k33;
        private Dictionary<int, byte[]> _digitMasks;
        private Dictionary<byte, byte[]> _characterMasks;

        public SevenSegmentWing(II2cBus i2cBus, byte address = 0x70)
        {
            ht16k33 = new Ht16k33(i2cBus, address);
            SetDigitMasks();
            SetCharacterMasks();
        }

        public bool ColonOn
        {
            set { SetColon(value); }
        }
        
        private void SetDigitMasks()
        {
            _digitMasks = new Dictionary<int, byte[]>
            {
                { 0, new byte[] { 0, 1, 2, 3, 4, 5, 6 } },           //Digit 1
                { 1, new byte[] { 16, 17, 18, 19, 20, 21, 22 } },    //Digit 2
                { 2, new byte[] { 48, 49, 50, 51, 52, 53, 54 } },    //Digit 3
                { 3, new byte[] { 64, 65, 66, 67, 68, 69, 70 } }     //Digit 4
            };
        }

        private void SetDecimals(byte digit, bool isOn)
        {
            switch(digit)
            {
                case 1:
                    ht16k33.SetLed(7, true);
                    break;
                case 2:
                    ht16k33.SetLed(23, true);
                    break;
                case 3:
                    ht16k33.SetLed(55, true);
                    break;
                case 4:
                    ht16k33.SetLed(71, true);
                    break;
                default: break;
            }            
        }

        private void SetColon(bool isOn)
        {
            ht16k33.SetLed(33, isOn);
            ht16k33.UpdateDisplay();
        }

        private void SetCharacterMasks()
        {
            _characterMasks = new Dictionary<byte, byte[]>
            {
                { 0, new byte[] { 1, 1, 1, 1, 1, 1, 0 } },
                { 1, new byte[] { 0, 1, 1, 0, 0, 0, 0 } },
                { 2, new byte[] { 1, 1, 0, 1, 1, 0, 1 } },
                { 3, new byte[] { 1, 1, 1, 1, 0, 0, 1 } },
                { 4, new byte[] { 0, 1, 1, 0, 0, 1, 1 } },
                { 5, new byte[] { 1, 0, 1, 1, 0, 1, 1 } },
                { 6, new byte[] { 1, 0, 1, 1, 1, 1, 1 } },
                { 7, new byte[] { 1, 1, 1, 0, 0, 0, 0 } },
                { 8, new byte[] { 1, 1, 1, 1, 1, 1, 1 } },
                { 9, new byte[] { 1, 1, 1, 1, 0, 1, 1 } },
                { (byte)'A', new byte[] { 1, 1, 1, 0, 1, 1, 1 } },
                { (byte)'b', new byte[] { 0, 0, 1, 1, 1, 1, 1 } },
                { (byte)'C', new byte[] { 1, 0, 0, 1, 1, 1, 0 } },
                { (byte)'d', new byte[] { 0, 1, 1, 1, 1, 0, 1 } },
                { (byte)'E', new byte[] { 1, 0, 0, 1, 1, 1, 1 } },
                { (byte)'F', new byte[] { 1, 0, 0, 0, 1, 1, 1 } },
                { (byte)'H', new byte[] { 0, 1, 1, 0, 1, 1, 1 } },
                { (byte)'L', new byte[] { 0, 0, 0, 1, 1, 1, 0 } },
                { (byte)'P', new byte[] { 1, 1, 0, 0, 1, 1, 1 } },
                { (byte)'S', new byte[] { 1, 0, 1, 1, 0, 1, 1 } },
                { (byte)'U', new byte[] { 0, 1, 1, 1, 1, 1, 0 } },
                { (byte)' ', new byte[] { 0, 0, 0, 0, 0, 0, 0 } }
            };
        }

        public void SetDisplay(byte character, byte digit, bool useDecimal)
        {
            if (digit < 0 || digit > 3)
            {
                throw new IndexOutOfRangeException("SevenSegmentWing index must be 0, 1, 2 or 3");
            }

            try
            {
                byte[] characterMask = _characterMasks[character];
                byte[] digitMask = _digitMasks[digit];

                for (int i = 0; i < characterMask.Length; i++)
                {
                    if (characterMask[i] == 1)
                    {
                        ht16k33.SetLed(digitMask[i], true);
                    }
                }

                SetDecimals(digit, useDecimal);

            }catch (Exception ex)
            {
                throw new Exception("Error setting display.", ex);
            }                       

            ht16k33.UpdateDisplay();
        }

    }
}