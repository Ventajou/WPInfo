﻿using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//
// Credit for this code belongs with Andrew Vos, who published this code on his website.
// In the absence of a copyright statement, it is assumed that he retains copyright on this
// code. A formal request has been sent to Andrew requesting permission to use this code.
//
// Original Web Page URL [retrieved 9 April 2015]
//     http://www.andrewvos.com/2008/05/23/draw-rtf-text-on-a-graphics-object-in-c/
//
// Andrew Vos' Official site 
//     http://www.andrewvos.com
//
// Andrew Vos' GitHub site 
//     http://www.github.com/AndrewVos
//

namespace Ventajou.WPInfo
{
    public static class Graphics_DrawRtfText
    {
        private static RichTextBoxDrawer rtfDrawer;
        public static void DrawRtfText(this Graphics graphics, string rtf, Rectangle layoutArea)
        {
            if (Graphics_DrawRtfText.rtfDrawer == null)
            {
                Graphics_DrawRtfText.rtfDrawer = new RichTextBoxDrawer();
            }
            Graphics_DrawRtfText.rtfDrawer.Rtf = rtf;
            Graphics_DrawRtfText.rtfDrawer.Draw(graphics, layoutArea);
        }

        private class RichTextBoxDrawer : RichTextBox
        {
            // Code converted from code found here: http://support.microsoft.com/kb/812425/en-us

            // Convert the unit used by the .NET framework (1/100 inch) 
            // and the unit used by Win32 API calls (twips 1/1440 inch)

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams createParams = base.CreateParams;
                    if (SafeNativeMethods.LoadLibrary("msftedit.dll") != IntPtr.Zero)
                    {
                        createParams.ExStyle |= SafeNativeMethods.WS_EX_TRANSPARENT; // transparent
                        createParams.ClassName = "RICHEDIT50W";
                    }
                    return createParams;
                }
            }

            public void Draw(Graphics graphics, Rectangle layoutArea)
            {
                double anInchX = 1440 / graphics.DpiX;
                double anInchY = 1440 / graphics.DpiY;

                //Calculate the area to render.
                SafeNativeMethods.RECT rectLayoutArea;
                rectLayoutArea.Top = (int)(layoutArea.Top * anInchY);
                rectLayoutArea.Bottom = (int)(layoutArea.Bottom * anInchY);
                rectLayoutArea.Left = (int)(layoutArea.Left * anInchX);
                rectLayoutArea.Right = (int)(layoutArea.Right * anInchX);

                IntPtr hdc = graphics.GetHdc();

                SafeNativeMethods.FORMATRANGE fmtRange;
                fmtRange.chrg.cpMax = -1;                    //Indicate character from to character to 
                fmtRange.chrg.cpMin = 0;
                fmtRange.hdc = hdc;                                //Use the same DC for measuring and rendering
                fmtRange.hdcTarget = hdc;                    //Point at printer hDC
                fmtRange.rc = rectLayoutArea;            //Indicate the area on page to print
                fmtRange.rcPage = rectLayoutArea;    //Indicate size of page

                IntPtr wParam = IntPtr.Zero;
                wParam = new IntPtr(1);

                //Get the pointer to the FORMATRANGE structure in memory
                IntPtr lParam = IntPtr.Zero;
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
                Marshal.StructureToPtr(fmtRange, lParam, false);

                SafeNativeMethods.SendMessage(this.Handle, SafeNativeMethods.EM_FORMATRANGE, wParam, lParam);

                //Free the block of memory allocated
                Marshal.FreeCoTaskMem(lParam);

                //Release the device context handle obtained by a previous call
                graphics.ReleaseHdc(hdc);
            }

            #region SafeNativeMethods
            private static class SafeNativeMethods
            {
                [DllImport("USER32.dll")]
                public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

                [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
                public static extern IntPtr LoadLibrary(string lpFileName);

                [StructLayout(LayoutKind.Sequential)]
                public struct RECT
                {
                    public int Left;
                    public int Top;
                    public int Right;
                    public int Bottom;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct CHARRANGE
                {
                    public int cpMin;        //First character of range (0 for start of doc)
                    public int cpMax;        //Last character of range (-1 for end of doc)
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct FORMATRANGE
                {
                    public IntPtr hdc;                //Actual DC to draw on
                    public IntPtr hdcTarget;    //Target DC for determining text formatting
                    public RECT rc;                        //Region of the DC to draw to (in twips)
                    public RECT rcPage;                //Region of the whole DC (page size) (in twips)
                    public CHARRANGE chrg;        //Range of text to draw (see earlier declaration)
                }

                public const int WM_USER = 0x0400;
                public const int EM_FORMATRANGE = WM_USER + 57;
                public const int WS_EX_TRANSPARENT = 0x20;

            }
            #endregion
        }
    }
}
