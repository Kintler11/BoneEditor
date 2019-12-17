using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sfe
{

    public partial class Form1 : Form
    {
        int sandboxS;
        static int hexadecimalToDecimal(String hexVal)
        {
            int len = hexVal.Length;

            // Initializing base1 value  
            // to 1, i.e 16^0 
            int base1 = 1;

            int dec_val = 0;

            // Extracting characters as 
            // digits from last character 
            for (int i = len - 1; i >= 0; i--)
            {
                // if character lies in '0'-'9',  
                // converting it to integral 0-9  
                // by subtracting 48 from ASCII value 
                if (hexVal[i] >= '0' &&
                    hexVal[i] <= '9')
                {
                    dec_val += (hexVal[i] - 48) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }

                // if character lies in 'A'-'F' ,  
                // converting it to integral  
                // 10 - 15 by subtracting 55  
                // from ASCII value 
                else if (hexVal[i] >= 'A' &&
                         hexVal[i] <= 'F')
                {
                    dec_val += (hexVal[i] - 55) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }
            }
            return dec_val;
        }
        public void LoadSettings()
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fl);

            //Shadow Quality
            br.BaseStream.Position = 0x3E1;
            String text = hexadecimalToDecimal(br.ReadByte().ToString("X")).ToString();
            br.Close();
            textBox3.Text = text;


            //Physics Update Rate
            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            br = new BinaryReader(fl);
            br.BaseStream.Position = 0x3A8;
            string stext = hexadecimalToDecimal(br.ReadByte().ToString("X")).ToString();
            br.Close();
            textBox4.Text = stext;

            //Read arena
            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pInfo_00.dat", FileMode.Open);
            br = new BinaryReader(fl);
            string arenaName = null;
            int c = 0;
            for(int i = 0x414; i < 0x427; i++)
            {
                br.BaseStream.Position = i;

                if (br.ReadByte().ToString("X2") == "00")
                {
                    sandboxS = i + 6;
                    break;
                }
                if (br.ReadByte().ToString("X2") != "00")
                {
                arenaName += System.Convert.ToChar(System.Convert.ToUInt32(br.ReadByte().ToString("X2"), 16)).ToString();
                }
                c++;
            }
            br.Close();
            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pInfo_00.dat", FileMode.Open);
            br = new BinaryReader(fl);
            br.BaseStream.Position = 0x415;
            textBox5.Text = System.Convert.ToChar(System.Convert.ToUInt32(br.ReadByte().ToString("X2"), 16)).ToString() + arenaName;
            br.Close();

        }
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fl);
            //Read Sandbox
            br.BaseStream.Position = 0x415;
            string jeff = br.ReadByte().ToString("X2");
                br.BaseStream.Position = sandboxS;
                if (br.ReadByte().ToString("X2") == "01")
                {
                    br.Close();
                    checkBox1.Checked = true;
                }
                else
                {
                    br.Close();
                    checkBox1.Checked = false;
                }
                br.Close();

            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            br = new BinaryReader(fl);
            //Read Sandbox
            br.BaseStream.Position = sandboxS - 1;
            if (br.ReadByte().ToString("X2") == "01")
            {
                br.Close();
                checkBox3.Checked = true;
            }
            else
            {
                br.Close();
                checkBox3.Checked = false;
            }
            br.Close();

            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            br = new BinaryReader(fl);
            //Read Sandbox
                br.BaseStream.Position = 0x40D;
                if (br.ReadByte().ToString("X2") == "01")
                {
                    br.Close();
                    checkBox2.Checked = true;
                }
                else
                {
                    br.Close();
                    checkBox2.Checked = false;
                }
                br.Close();

            fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\additional_resources1.dat", FileMode.Open);
            br = new BinaryReader(fl);
            //Read Ammo
            string ammo = null;
            int curAmmo = 0;

            int runs = 0;
            int runss = 0;

            string ammoM = null;
            int curAmmoM = 0;

            int runsT = 0;
            int runssT = 0;

            int[] lAmmoloc = { 0x760, 0x75F, 0x75E, 0x72D, 0x72C, 0x72B, 0x6FE, 0x6FD, 0x6FC, 0x6C7, 0x6C6, 0x6C5, 0x694, 0x693, 0x692, 0x65E, 0x65D, 0x65C, 0x62E, 0x62D, 0x62C, 0x5FD, 0x5FC, 0x5FB, 0x5CD, 0x5CC, 0x5CB, 0x59A, 0x599, 0x598};
            foreach(int ammoLoc in lAmmoloc)
            {
                runs++;
                runss++;
                try
                {
                    br.BaseStream.Position = ammoLoc;
                    ammo += br.ReadByte().ToString("X2");
                    if (runs == 3)
                    {
                        curAmmo += hexadecimalToDecimal(ammo);
                        ammo = null;
                        runs = 0;
                    }

                    textBox1.Text = curAmmo.ToString();
                }
                catch
                {

                }
            }

            foreach (int sammoLoc in lAmmoloc)
            {
                runsT++;
                runssT++;
                try
                {
                    br.BaseStream.Position = sammoLoc + 4;
                    ammoM += br.ReadByte().ToString("X2");
                    if (runsT == 3)
                    {
                        curAmmoM += hexadecimalToDecimal(ammoM);
                        ammoM = null;
                        runsT = 0;
                    }

                    textBox2.Text = curAmmoM.ToString();
                }
                catch
                {

                }
            }
            br.Close();
            
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fl);
            BinaryWriter bw = new BinaryWriter(fl);
            br.BaseStream.Position = 0x415;
            string arena = br.ReadByte().ToString("X");
            br.Close();
                fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
                bw = new BinaryWriter(fl);
                if (checkBox1.Checked == true)
                {
                    bw.BaseStream.Position = sandboxS;
                    bw.Write((byte)01);
                    bw.Close();
                }
                if (checkBox1.Checked == false)
                {
                    bw.BaseStream.Position = sandboxS;
                    bw.Write((byte)00);
                    bw.Close();
                }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryWriter bw = new BinaryWriter(fl);
            bw.BaseStream.Position = 0x40D;
            if (checkBox2.Checked == true)
            {
                bw.Write((byte)01);
                bw.Close();
            }

            if (checkBox2.Checked == false)
            {
                bw.Write((byte)00);
                bw.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryWriter bw = new BinaryWriter(fl);
            if((textBox3.Text != null) || (textBox3.Text != ""))
            {
                try
                {
                    int bit = int.Parse(textBox3.Text);
                    bw.BaseStream.Position = 0x3E1;
                    bw.Write((byte)bit);
                    bw.Close();
                }
                catch
                {
                    bw.Close();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryWriter bw = new BinaryWriter(fl);
            if ((textBox3.Text != null) || (textBox3.Text != ""))
            {
                try
                {
                    int bit = int.Parse(textBox4.Text);
                    bw.BaseStream.Position = 0x3A8;
                    bw.Write((byte)bit);
                    bw.Close();
                }
                catch
                {
                    bw.Close();
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            FileStream fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fl);
            BinaryWriter bw = new BinaryWriter(fl);
            br.BaseStream.Position = 0x415;
            string arena = br.ReadByte().ToString("X");
            br.Close();
                fl = new FileStream(@"C:\Users\kaitt\AppData\LocalLow\Stress Level Zero\BONEWORKS\bw1_pinfo_00.dat", FileMode.Open);
                bw = new BinaryWriter(fl);
                if (checkBox3.Checked == true)
                {
                    bw.BaseStream.Position = sandboxS - 1;
                    bw.Write((byte)01);
                    bw.Close();
                }
                if (checkBox3.Checked == false)
                {
                    bw.BaseStream.Position = sandboxS - 1;
                    bw.Write((byte)00);
                    bw.Close();
                }
        }
    }
}
