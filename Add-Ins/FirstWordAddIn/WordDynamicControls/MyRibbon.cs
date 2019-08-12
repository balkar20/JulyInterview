﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace WordDynamicControls
{
    public partial class MyRibbon
    {
        private void MyRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void addButtonCheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ToggleButtonOnDocument();
        }

        private void addRichTextCheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ToggleRichTextControlOnDocument();
        }
    }
}
